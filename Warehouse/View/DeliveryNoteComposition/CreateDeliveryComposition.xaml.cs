using System.Windows;
using System.Windows.Controls;
using Warehouse.Entity;
using Warehouse.Storage;
using Warehouse.Validation;

namespace Warehouse.View.DeliveryNoteComposition
{
    public partial class CreateDeliveryComposition : Window
    {
        DataGrid dataGrid;
        ComboBoxRepo comboBoxRepo;
        CrudRepo<DeliveryCompositionEntity> deliveryCrud;
        CommonValidation commonValidation;

        public CreateDeliveryComposition(DataGrid dataGrid)
        {
            InitializeComponent();

            this.dataGrid = dataGrid;

            comboBoxRepo = new ComboBoxRepoImpl();
            deliveryCrud = new DeliveryCompositionRepoImpl();
            commonValidation = new CommonValidation();

            comboBoxRepo.insertProductsIntoComboBox(ProductCombo);
            comboBoxRepo.insertDeliveryIntoComboBox(DeliveryCombo);
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxEntity product = (ComboBoxEntity)ProductCombo.SelectedItem;
            ComboBoxEntity delivery = (ComboBoxEntity)DeliveryCombo.SelectedItem;

            string requested = RequestedBox.Text;
            string released = ReleasedBox.Text;
            string price = PriceBox.Text;

            if (product != null && delivery != null && (ComboBoxItem)UnitCombo.SelectedItem != null && price != null && released != null && requested != null && commonValidation.isNumberInRange(requested, 2000) && commonValidation.isNumberInRange(released, 2000) && commonValidation.isNumberInRange(price, 2000))
            {
                string unit = ((ComboBoxItem)UnitCombo.SelectedItem).Content.ToString();
                DeliveryCompositionEntity deliveryComposition = new DeliveryCompositionEntity(delivery.id, product.id, unit, requested, released, price);

                deliveryCrud.create(deliveryComposition);
                deliveryCrud.fetchToGrid(dataGrid);

                this.Close();
            }
            else
            {
                MessageBox.Show("Введите корректные значения!");
            }
        }

        private void ReturnFirst_Click(object sender, RoutedEventArgs e)
        {
            DeliveryCombo.Visibility = Visibility.Visible;
            ProductCombo.Visibility = Visibility.Visible;
            UnitCombo.Visibility = Visibility.Visible;
            RequestedBox.Visibility = Visibility.Visible;
            NextFirst.Visibility = Visibility.Visible;
            Close.Visibility = Visibility.Visible;

            ReleasedBox.Visibility = Visibility.Collapsed;
            PriceBox.Visibility = Visibility.Collapsed;
            Confirm.Visibility = Visibility.Collapsed;
            ReturnFirst.Visibility = Visibility.Collapsed;
        }

        private void NextFirst_Click(object sender, RoutedEventArgs e)
        {
            ReleasedBox.Visibility = Visibility.Visible;
            PriceBox.Visibility = Visibility.Visible;
            Confirm.Visibility = Visibility.Visible;
            ReturnFirst.Visibility = Visibility.Visible;

            DeliveryCombo.Visibility = Visibility.Collapsed;
            ProductCombo.Visibility = Visibility.Collapsed;
            UnitCombo.Visibility = Visibility.Collapsed;
            RequestedBox.Visibility = Visibility.Collapsed;
            NextFirst.Visibility = Visibility.Collapsed;
            Close.Visibility = Visibility.Collapsed;
        }
    }
}