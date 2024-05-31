using System;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using Warehouse.Entity;
using Warehouse.Storage;
using Warehouse.Validation;
using Warehouse.View.DeliveryNoteComposition;

namespace Warehouse.View.DeliveryNote
{
    public partial class CreateDelivery : Window
    {
        private Database database;

        private DataGrid dataGrid;
        private ComboBoxRepo comboBoxRepo;
        private DeliveryNoteValidation validation;
        private CrudRepo<DeliveryNoteEntity> crudRepo;

        public CreateDelivery(DataGrid dataGrid)
        {
            InitializeComponent();

            this.dataGrid = dataGrid;

            validation = new DeliveryNoteValidation();
            comboBoxRepo = new ComboBoxRepoImpl();
            database = new Database();
            crudRepo = new DeliveryNoteRepoImpl();

            DatePicker.Text = DateTime.Today.ToString("yyyy-MM-dd");

            comboBoxRepo.insertDivisionsIntoComboBox(DivisionComboBox);

            ContractorGrid.ItemsSource = new ObservableCollection<DeliveryCompositionEntity>();
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxEntity division = (ComboBoxEntity)DivisionComboBox.SelectedItem;
            try
            {
                string date = DatePicker.SelectedDate.Value.ToString("yyyy-MM-dd");
                string assignment = AssignmentBox.Text;
                string broker = BrokerBox.Text;

                if (division != null)
                {
                    DeliveryNoteEntity deliveryNote = new DeliveryNoteEntity(division.id, date, assignment, broker);

                    if (validation.isDeliveryNoteValid(deliveryNote))
                    {
                        database = new Database();
                        using (SqlConnection connection = database.getSqlConnection())
                        {
                            database.checkConnection();
                            SqlTransaction transaction = connection.BeginTransaction();

                            try
                            {
                                long deliveryId = createDeliveryAndReturnId(deliveryNote, transaction);
                                saveDataGridItems(deliveryId, transaction);
                                transaction.Commit();

                                crudRepo.fetchToGrid(dataGrid);

                                database.checkConnection();

                                this.Close();

                            }
                            catch (Exception ex)
                            {
                                transaction.Rollback();
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Выберите подразделение!");
                }
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show("Выберите дату!" + ex.Message);
            }
        }

        private void AddContractor_Click(object sender, RoutedEventArgs e)
        {
            CreateDeliveryComposition createDelivery = new CreateDeliveryComposition(ContractorGrid);
            createDelivery.ShowDialog();
        }

        private void EditContractor_Click(object sender, RoutedEventArgs e)
        {
            var selectedRow = ContractorGrid.SelectedItem as DeliveryCompositionEntity;

            if (selectedRow != null)
            {
                EditDeliveryComposition deliveryComposition = new EditDeliveryComposition(selectedRow, ContractorGrid);
                deliveryComposition.ShowDialog();
            }
            else
            {
                MessageBox.Show("Не выбрана строка для редактирования", "Ошибка", MessageBoxButton.OK);
            }
        }

        private void DeleteContractor_Click(object sender, RoutedEventArgs e)
        {
            var selectedRow = ContractorGrid.SelectedItem as DeliveryCompositionEntity;

            if (selectedRow != null)
            {
                var dataGridItems = (ObservableCollection<DeliveryCompositionEntity>)ContractorGrid.ItemsSource;

                dataGridItems.Remove(selectedRow);
            }
            else
            {
                MessageBox.Show("Не выбрана строка для редактирования", "Ошибка", MessageBoxButton.OK);
            }
        }

        private long createDeliveryAndReturnId(DeliveryNoteEntity entity, SqlTransaction transaction)
        {
            string query = $"INSERT INTO delivery_note(division_id, date, assignment, broker) OUTPUT INSERTED.delivery_note_id VALUES('{entity.divisionId}', '{entity.date}', N'{entity.assignment}', N'{entity.broker}')";

            SqlCommand command = new SqlCommand(query, transaction.Connection, transaction);

            long id = (long)command.ExecuteScalar();

            return id;
        }

        private void saveDataGridItems(long deliveryId, SqlTransaction transaction)
        {
            var dataGridItems = (ObservableCollection<DeliveryCompositionEntity>)ContractorGrid.ItemsSource;
            
            foreach (var item in dataGridItems)
            {
                try
                {
                    string query = $"INSERT INTO delivery_note_composition(delivery_note_id, product_id, requested, released, price, amount) VALUES('{deliveryId}', '{item.productId}', '{item.requested}', '{item.released}', '{item.price}', '{item.amount}')";

                    SqlCommand command = new SqlCommand(query, transaction.Connection, transaction);

                    command.ExecuteNonQuery();
                } catch (SqlException ex)
                {
                    MessageBox.Show("EX: " + ex);
                } 
            }
        }
    }
}