using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Warehouse.Entity;
using Warehouse.Storage;
using Warehouse.Validation;

namespace Warehouse.View.DeliveryNoteComposition
{
    public partial class CreateDeliveryComposition : Window
    {
        private DataGrid dataGrid;
        private ComboBoxRepo comboBoxRepo;
        private CommonValidation commonValidation;

        public CreateDeliveryComposition(DataGrid dataGrid)
        {
            InitializeComponent();

            this.dataGrid = dataGrid;

            comboBoxRepo = new ComboBoxRepoImpl();
            commonValidation = new CommonValidation();

            comboBoxRepo.insertProductsIntoComboBox(ProductCombo);
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxEntity product = (ComboBoxEntity)ProductCombo.SelectedItem;

            string requested = RequestedBox.Text;
            string released = ReleasedBox.Text;
            string price = PriceBox.Text;

            if (product != null && commonValidation.isNumberInRange(requested, 2000) && commonValidation.isNumberInRange(released, 2000) && commonValidation.isNumberInRange(price, 2000))
            {
                DeliveryCompositionEntity deliveryCompositionEntity = new DeliveryCompositionEntity(product.id, product.name, requested, released, price);

                AddNewRowToDataGrid(deliveryCompositionEntity);

                this.Close();
            }
            else
            {
                MessageBox.Show("Введите корректные значения!");
            }
        }

        private void AddNewRowToDataGrid(DeliveryCompositionEntity newDeliveryCompositionEntity)
        {
            var dataGridItems = (ObservableCollection<DeliveryCompositionEntity>)dataGrid.ItemsSource;
            dataGridItems.Add(newDeliveryCompositionEntity);
        }
    }
}