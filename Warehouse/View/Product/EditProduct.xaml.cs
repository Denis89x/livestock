using System.Windows;
using System.Windows.Controls;
using Warehouse.Entity;
using Warehouse.storage1;
using Warehouse.Validation;

namespace Warehouse.View.Product
{
    public partial class EditProduct : Window
    {
        long productId;
        DataGrid dataGrid;
        ProductRepo productRepo;

        ProductValidation validation;

        public EditProduct(long productId, string title, string sort, string unit, DataGrid dataGrid)
        {
            InitializeComponent();

            this.productId = productId;
            this.dataGrid = dataGrid;

            productRepo = new ProductRepoImpl();
            validation = new ProductValidation();

            TitleBox.Text = title;
            SortBox.Text = sort;
            UnitCombo.Text = unit;
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            string title = TitleBox.Text;
            string sort = SortBox.Text;
            string unit = ((ComboBoxItem)UnitCombo.SelectedItem).Content.ToString();

            if (unit != null)
            {

                ProductEntity product = new ProductEntity(productId, title, sort, unit);

                if (validation.isProductValid(product))
                {

                    productRepo.updateProduct(product);
                    productRepo.fetchProductToGrid(dataGrid);

                    this.Close();
                }
            }
            else
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