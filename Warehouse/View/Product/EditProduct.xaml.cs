using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Warehouse.Entity;
using Warehouse.storage1;

namespace Warehouse.View.Product
{
    public partial class EditProduct : Window
    {
        long productId;
        DataGrid dataGrid;
        ProductRepo productRepo;

        public EditProduct(long productId, string title, string sort, string unit, DataGrid dataGrid)
        {
            InitializeComponent();

            this.productId = productId;
            this.dataGrid = dataGrid;
            this.productRepo = new ProductRepoImpl();

            TitleBox.Text = title;
            SortBox.Text = sort;
            UnitBox.Text = unit;
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            string title = TitleBox.Text;
            string sort = SortBox.Text;
            string unit = UnitBox.Text;

            ProductEntity product = new ProductEntity(productId, title, sort, unit);

            productRepo.updateProduct(product);
            productRepo.fetchProductToGrid(dataGrid);

            this.Close();
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
