using System.Windows;
using System.Windows.Controls;
using Warehouse.Entity;
using Warehouse.storage1;

namespace Warehouse.View.DeliveryNoteComposition
{
    public partial class EditDeliveryComposition : Window
    {
        long deliveryCompositionId;
        DataGrid dataGrid;
        ComboBoxRepo comboBoxRepo;
        CrudRepo<DeliveryCompositionEntity> crudRepo;

        public EditDeliveryComposition(long deliveryCompositionId, string delivery, string product, string unit, string requested, string released, string price, DataGrid dataGrid)
        {
            InitializeComponent();

            this.dataGrid = dataGrid;
            this.deliveryCompositionId = deliveryCompositionId;

            comboBoxRepo = new ComboBoxRepoImpl();
            crudRepo = new DeliveryCompositionRepoImpl();

            comboBoxRepo.insertProductsIntoComboBox(ProductCombo);
            comboBoxRepo.insertDeliveryIntoComboBox(DeliveryCombo);

            UnitBox.Text = unit;
            RequestedBox.Text = requested;
            ReleasedBox.Text = released;
            PriceBox.Text = price;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxEntity product = (ComboBoxEntity)ProductCombo.SelectedItem;
            ComboBoxEntity delivery = (ComboBoxEntity)DeliveryCombo.SelectedItem;

            string unit = UnitBox.Text;
            string requested = RequestedBox.Text;
            string released = ReleasedBox.Text;
            string price = PriceBox.Text;

            DeliveryCompositionEntity deliveryComposition = new DeliveryCompositionEntity(deliveryCompositionId, delivery.id, product.id, unit, requested, released, price);

            crudRepo.update(deliveryComposition);
            crudRepo.fetchToGrid(dataGrid);

            this.Close();
        }

        private void ReturnFirst_Click(object sender, RoutedEventArgs e)
        {
            DeliveryCombo.Visibility = Visibility.Visible;
            ProductCombo.Visibility = Visibility.Visible;
            UnitBox.Visibility = Visibility.Visible;
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
            UnitBox.Visibility = Visibility.Collapsed;
            RequestedBox.Visibility = Visibility.Collapsed;
            NextFirst.Visibility = Visibility.Collapsed;
            Close.Visibility = Visibility.Collapsed;
        }
    }
}
