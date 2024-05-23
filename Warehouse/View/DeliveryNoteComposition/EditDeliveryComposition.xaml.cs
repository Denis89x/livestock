using System;
using System.Windows;
using System.Windows.Controls;
using Warehouse.Entity;
using Warehouse.Validation;

namespace Warehouse.View.DeliveryNoteComposition
{
    public partial class EditDeliveryComposition : Window
    {
        private DataGrid dataGrid;
        private CommonValidation commonValidation;
        private DeliveryCompositionEntity selectedRow;

        internal EditDeliveryComposition(DeliveryCompositionEntity selectedRow, DataGrid dataGrid)
        {
            InitializeComponent();

            this.dataGrid = dataGrid;
            this.selectedRow = selectedRow;

            commonValidation = new CommonValidation();

            ProductCombo.Items.Add(selectedRow.name);
            ProductCombo.SelectedIndex = 0;

            RequestedBox.Text = selectedRow.requested;
            ReleasedBox.Text = selectedRow.released;
            PriceBox.Text = selectedRow.price;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            string requested = RequestedBox.Text;
            string released = ReleasedBox.Text;
            string price = PriceBox.Text;

            if (commonValidation.isNumberInRange(requested, 2000) && commonValidation.isNumberInRange(released, 2000) && commonValidation.isNumberInRange(price, 2000))
            {
                selectedRow.requested = requested;
                selectedRow.released = released;
                selectedRow.price = price;
                selectedRow.amount = (Convert.ToInt32(requested) * Convert.ToInt32(price)).ToString();

                dataGrid.Items.Refresh();

                this.Close();
            }
            else
            {
                MessageBox.Show("Введите корректные значения!");
            }
        }
    }
}