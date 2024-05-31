using System.Data.SqlClient;
using System;
using System.Windows;
using System.Windows.Controls;
using Warehouse.Entity;
using Warehouse.Storage;
using Warehouse.Validation;

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
            string broker = BrokerBox.Text;

            if (division != null)
            {
                DeliveryNoteEntity deliveryNote = new DeliveryNoteEntity(deliveryId, division.id, date, assignment, broker);

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

        private void fetchDelivery()
        {
            string query = $"SELECT division.division_type as division, date, assignment, broker FROM delivery_note, division WHERE delivery_note.division_id = division.division_id AND delivery_note_id = '{deliveryId}'";

            SqlCommand command = new SqlCommand(query, database.getSqlConnection());

            database.checkConnection();

            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    DivisionComboBox.Text = reader["division"].ToString();
                    DatePicker.Text = Convert.ToDateTime(reader["date"]).ToString("dd.MM.yyyy");
                    AssignmentBox.Text = reader["assignment"].ToString();
                    BrokerBox.Text = reader["broker"].ToString();
                }
            }

            database.checkConnection();
        }
    }
}