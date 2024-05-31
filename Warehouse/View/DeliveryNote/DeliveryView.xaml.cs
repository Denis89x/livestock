using System.Data.SqlClient;
using System;
using System.Windows;
using Warehouse.Entity;
using Warehouse.Storage;
using System.Data;

namespace Warehouse.View.DeliveryNote
{
    public partial class DeliveryView : Window
    {
        private Database database;
        private long deliveryId;

        private CrudRepo<DeliveryCompositionEntity> deliveryRepo;

        public DeliveryView(long deliveryId, string userRole)
        {
            InitializeComponent();

            database = new Database();
            deliveryRepo = new DeliveryCompositionRepoImpl();

            this.deliveryId = deliveryId;

            if (userRole.Equals("ROLE_ACCOUNTANT"))
            {
                AddContractor.Visibility = Visibility.Collapsed;
                EditContractor.Visibility = Visibility.Collapsed;
                DeleteContractor.Visibility = Visibility.Collapsed;
            }

            fetchDelivery();
            fetchDeliveryCompositions();
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void fetchDelivery()
        {
            string query = $"SELECT division.division_type as division, date, assignment, broker FROM delivery_note, division WHERE delivery_note.division_id = division.division_id AND delivery_note_id = '{deliveryId}'";

            SqlCommand command = new SqlCommand(query, database.getSqlConnection());

            database.checkConnection();

            WaybillCompositionEntity waybillComposition = new WaybillCompositionEntity();

            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    DivisionComboBox.Items.Add(reader["division"].ToString());
                    DivisionComboBox.SelectedIndex = 0;
                    DatePicker.Text = Convert.ToDateTime(reader["date"]).ToString("dd.MM.yyyy");
                    AssignmentBox.Text = reader["assignment"].ToString();
                    BrokerBox.Text = reader["broker"].ToString();
                }
            }

            database.checkConnection();
        }

        private void fetchDeliveryCompositions()
        {
            string query = $"SELECT delivery_note_composition_id as delivery_note_composition_id, delivery_note_composition.product_id, finished_product.title as name, requested, released, price, amount FROM delivery_note_composition, finished_product WHERE delivery_note_composition.product_id = finished_product.product_id AND delivery_note_composition.delivery_note_id = '{deliveryId}'";

            database.selectQuery(query, ContractorGrid);
        }

        private void AddContractor_Click(object sender, RoutedEventArgs e)
        {
            AddDeliveryCom deliveryCom = new AddDeliveryCom(deliveryId, ContractorGrid);
            deliveryCom.ShowDialog();
        }

        private void EditContractor_Click(object sender, RoutedEventArgs e)
        {
            var selectedRow = ContractorGrid.SelectedItem as DataRowView;

            if (selectedRow != null)
            {
                EditDeliveryComp editDelivery = new EditDeliveryComp(Convert.ToInt64(selectedRow.Row.ItemArray[0]), deliveryId, ContractorGrid);
                editDelivery.ShowDialog();
            }
            else
            {
                MessageBox.Show("Не выбрана строка для редактирования", "Ошибка", MessageBoxButton.OK);
            }
        }

        private void DeleteContractor_Click(object sender, RoutedEventArgs e)
        {
            var selectedRow = ContractorGrid.SelectedItem as DataRowView;

            if (selectedRow != null)
            {
                long id = Convert.ToInt64(selectedRow.Row.ItemArray[0]);

                deliveryRepo.delete(id);
                fetchDeliveryCompositions();
            }
            else
            {
                MessageBox.Show("Выберите поле для удаления!");
            }
        }
    }
}