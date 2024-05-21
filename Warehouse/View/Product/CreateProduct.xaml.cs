using System.Windows;
using System.Windows.Controls;
using Warehouse.Entity;
using Warehouse.Storage;
using Warehouse.Validation;

namespace Warehouse.View.Product
{
    public partial class CreateProduct : Window
    {
        private CrudRepo<ProductEntity> productCrud;
        private DataGrid dataGrid;

        private ProductValidation validation;

        public CreateProduct(DataGrid dataGrid)
        {
            InitializeComponent();

            this.dataGrid = dataGrid;

            productCrud = new ProductRepoImpl();
            validation = new ProductValidation();
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            string title = TitleBox.Text;
            string sort = SortBox.Text;
            
            if ((ComboBoxItem)UnitCombo.SelectedItem != null)
            {
                string unit = ((ComboBoxItem)UnitCombo.SelectedItem).Content.ToString();

                ProductEntity product = new ProductEntity(title, sort, unit);

                if (validation.isProductValid(product))
                {
                    productCrud.create(product);
                    productCrud.fetchToGrid(dataGrid);

                    this.Close();
                }
            } else
            {
                MessageBox.Show("Выберите единицу измерения!");
            }
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}