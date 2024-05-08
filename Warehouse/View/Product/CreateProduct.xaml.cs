using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Warehouse.Entity;
using Warehouse.storage1;

namespace Warehouse.View.Product
{
    public partial class CreateProduct : Window
    {
        ProductRepo productRepo;
        DataGrid dataGrid;

        public CreateProduct(DataGrid dataGrid)
        {
            InitializeComponent();
            this.dataGrid = dataGrid;
            this.productRepo = new ProductRepoImpl();
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            string title = TitleBox.Text;
            string sort = SortBox.Text;
            string unit = UnitBox.Text;

            ProductEntity product = new ProductEntity(title, sort, unit);

            productRepo.createProduct(product);
            productRepo.fetchProductToGrid(dataGrid);

            TitleBox.Text = "";
            SortBox.Text = "";
            UnitBox.Text = "";
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
