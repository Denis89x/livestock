using System;
using System.Windows;
using System.Windows.Controls;
using Warehouse.Entity;
using Warehouse.storage1;
using Warehouse.Validation;

namespace Warehouse.View.DeliveryNote
{
    public partial class CreateDelivery : Window
    {
        DataGrid dataGrid;
        ComboBoxRepo comboBoxRepo;
        DeliveryNoteRepo deliveryNoteRepo;
        DeliveryNoteValidation validation;

        public CreateDelivery(DataGrid dataGrid)
        {
            InitializeComponent();

            this.dataGrid = dataGrid;

            validation = new DeliveryNoteValidation();
            comboBoxRepo = new ComboBoxRepoImpl();
            deliveryNoteRepo = new DeliveryNoteRepoImpl();

            DatePicker.Text = DateTime.Today.ToString("yyyy-MM-dd");

            comboBoxRepo.insertDivisionsIntoComboBox(DivisionComboBox);
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxEntity division = (ComboBoxEntity)DivisionComboBox.SelectedItem;

            string date = DatePicker.SelectedDate.Value.ToString("yyyy-MM-dd");
            string assignment = AssignmentBox.Text;

            if (division != null)
            {
                DeliveryNoteEntity deliveryNote = new DeliveryNoteEntity(division.id, date, assignment);

                if (validation.isDeliveryNoteValid(deliveryNote))
                {
                    deliveryNoteRepo.createDeliveryNote(deliveryNote);
                    deliveryNoteRepo.fetchDeliveryNoteToGrid(dataGrid);

                    this.Close();
                }
            } else
            {
                MessageBox.Show("Выберите подразделение!");
            }
        }
    }
}
