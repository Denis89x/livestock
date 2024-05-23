using System.Windows;
using System.Windows.Controls;
using Warehouse.Entity;
using Warehouse.Storage;
using Warehouse.Validation;

namespace Warehouse.View.DeliveryNote
{
    public partial class AddDeliveryCom : Window
    {
        private long deliveryId;

        private DataGrid dataGrid;
        private ComboBoxRepo combo;
        private CrudRepo<DeliveryCompositionEntity> deliveryCompositionRepo;
        private CommonValidation commonValidation;

        public AddDeliveryCom(long deliveryId, DataGrid dataGrid)
        {
            InitializeComponent();

            deliveryCompositionRepo = new DeliveryCompositionRepoImpl();
            combo = new ComboBoxRepoImpl();
            commonValidation = new CommonValidation();

            combo.insertProductsIntoComboBox(ProductCombo);

            this.deliveryId = deliveryId;
            this.dataGrid = dataGrid;
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxEntity product = (ComboBoxEntity)ProductCombo.SelectedItem;

            string requested = RequestedBox.Text;
            string released = ReleasedBox.Text;
            string price = PriceBox.Text;

            if (product != null && commonValidation.isNumberInRange(requested, 2000) && commonValidation.isNumberInRange(released, 2000) && commonValidation.isNumberInRange(price, 2000))
            {
                DeliveryCompositionEntity deliveryCompositionEntity = new DeliveryCompositionEntity(deliveryId, product.id, requested, released, price);

                deliveryCompositionRepo.create(deliveryCompositionEntity);
                deliveryCompositionRepo.fetchToGrid(dataGrid);

                this.Close();
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
