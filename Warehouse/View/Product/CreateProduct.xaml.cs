using System.Windows;
using System.Windows.Controls;
using Warehouse.Entity;
using Warehouse.storage1;
using Warehouse.Validation;

namespace Warehouse.View.Product
{
    public partial class CreateProduct : Window
    {
        ProductRepo productRepo;
        DataGrid dataGrid;

        ProductValidation validation;

        public CreateProduct(DataGrid dataGrid)
        {
            InitializeComponent();

            this.dataGrid = dataGrid;

            productRepo = new ProductRepoImpl();
            validation = new ProductValidation();
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            string title = TitleBox.Text;
            string sort = SortBox.Text;
            string unit = ((ComboBoxItem)UnitCombo.SelectedItem).Content.ToString();

            if (unit != null)
            {
                ProductEntity product = new ProductEntity(title, sort, unit);

                if (validation.isProductValid(product))
                {
                    productRepo.createProduct(product);
                    productRepo.fetchProductToGrid(dataGrid);

                    TitleBox.Text = "";
                    SortBox.Text = "";
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
