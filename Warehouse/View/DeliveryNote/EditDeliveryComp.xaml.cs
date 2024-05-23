using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using Warehouse.Entity;
using Warehouse.Storage;
using Warehouse.Validation;

namespace Warehouse.View.DeliveryNote
{
    public partial class EditDeliveryComp : Window
    {
        private long deliveryId;
        private long compositionId;
        private DataGrid dataGrid;
        private Database database;
        private CommonValidation commonValidation;

        private CrudRepo<DeliveryCompositionEntity> crudRepo;

        public EditDeliveryComp(long compositionId, long deliveryId, DataGrid dataGrid)
        {
            InitializeComponent();

            this.compositionId = compositionId;
            this.dataGrid = dataGrid;
            this.deliveryId = deliveryId;

            crudRepo = new DeliveryCompositionRepoImpl();
            database = new Database();
            commonValidation = new CommonValidation();

            fetchDelivery();
        }

        private void fetchDelivery()
        {
            string query = $"SELECT finished_product.title, requested, released, price, amount FROM delivery_note_composition, finished_product WHERE finished_product.product_id = delivery_note_composition.product_id AND delivery_note_composition_id = '{compositionId}'";

            SqlCommand command = new SqlCommand(query, database.getSqlConnection());

            database.checkConnection();

            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    ProductCombo.Items.Add(reader["title"].ToString());
                    ProductCombo.SelectedIndex = 0;

                    RequestedBox.Text = reader["requested"].ToString();
                    ReleasedBox.Text = reader["released"].ToString();
                    PriceBox.Text = reader["price"].ToString();
                }
            }

            database.checkConnection();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            string requested = RequestedBox.Text;
            string released = ReleasedBox.Text;
            string price = PriceBox.Text;

            if (commonValidation.isNumberInRange(requested, 2000) && commonValidation.isNumberInRange(released, 2000) && commonValidation.isNumberInRange(price, 2000))
            {
                DeliveryCompositionEntity deliveryCompositionEntity = new DeliveryCompositionEntity(compositionId, requested, released, price);
                crudRepo.update(deliveryCompositionEntity);
                fetchDeliveryCompositions();

                this.Close();
            }
        }

        private void fetchDeliveryCompositions()
        {
            string query = $"SELECT delivery_note_composition_id as delivery_note_composition_id, delivery_note_composition.product_id, finished_product.title as name, requested, released, price, amount FROM delivery_note_composition, finished_product WHERE delivery_note_composition.product_id = finished_product.product_id AND delivery_note_composition.delivery_note_id = '{deliveryId}'";

            database.selectQuery(query, dataGrid);
        }
    }
}