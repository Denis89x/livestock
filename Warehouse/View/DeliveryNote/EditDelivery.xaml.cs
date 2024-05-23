using System.Data.SqlClient;
using System;
using System.Windows;
using System.Windows.Controls;
using Warehouse.Entity;
using Warehouse.Storage;
using Warehouse.Validation;
using System.Collections.ObjectModel;
using Warehouse.View.DeliveryNoteComposition;

namespace Warehouse.View.DeliveryNote
{
    public partial class EditDelivery : Window
    {
        private long deliveryId;
        private Database database;
        private DataGrid dataGrid;
        private ComboBoxRepo comboBoxRepo;
        private CrudRepo<DeliveryNoteEntity> deliveryNoteCrud;
        private DeliveryNoteValidation validation;

        public EditDelivery(long deliveryId, DataGrid dataGrid)
        {
            InitializeComponent();

            this.dataGrid = dataGrid;
            this.deliveryId = deliveryId;

            validation = new DeliveryNoteValidation();
            comboBoxRepo = new ComboBoxRepoImpl();
            deliveryNoteCrud = new DeliveryNoteRepoImpl();

            database = new Database();

            comboBoxRepo.insertDivisionsIntoComboBox(DivisionComboBox);

            fetchDelivery();
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxEntity division = (ComboBoxEntity)DivisionComboBox.SelectedItem;

            string date = DatePicker.SelectedDate.Value.ToString("yyyy-MM-dd");
            string assignment = AssignmentBox.Text;

            if (division != null)
            {
                DeliveryNoteEntity deliveryNote = new DeliveryNoteEntity(deliveryId, division.id, date, assignment);

                if (validation.isDeliveryNoteValid(deliveryNote))
                {
                    deliveryNoteCrud.update(deliveryNote);
                    deliveryNoteCrud.fetchToGrid(dataGrid);

                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Выберите подразделение!");
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

        private void fetchDelivery()
        {
            string query = $"SELECT division.division_type as division, date, assignment FROM delivery_note, division WHERE delivery_note.division_id = division.division_id AND delivery_note_id = '{deliveryId}'";

            SqlCommand command = new SqlCommand(query, database.getSqlConnection());

            database.checkConnection();

            WaybillCompositionEntity waybillComposition = new WaybillCompositionEntity();

            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    DatePicker.Text = Convert.ToDateTime(reader["date"]).ToString("dd.MM.yyyy");
                    AssignmentBox.Text = reader["assignment"].ToString();
                }
            }

            database.checkConnection();
        }

        private void fetchDeliveryCompositions()
        {
            string query = $"SELECT delivery_note_composition.product_id, finished_product.title as name, requested, released, price, amount FROM delivery_note_composition, finished_product WHERE delivery_note_composition.product_id = finished_product.product_id AND delivery_note_composition.delivery_note_id = '{deliveryId}'";

            database.selectQuery(query, ContractorGrid);
        }
    }
}