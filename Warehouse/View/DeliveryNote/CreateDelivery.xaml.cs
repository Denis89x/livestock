using System;
using System.Windows;
using System.Windows.Controls;
using Warehouse.Entity;
using Warehouse.Storage;
using Warehouse.Validation;

namespace Warehouse.View.DeliveryNote
{
    public partial class CreateDelivery : Window
    {
        private DataGrid dataGrid;
        private ComboBoxRepo comboBoxRepo;
        private CrudRepo<DeliveryNoteEntity> deliveryNoteCrud;
        private DeliveryNoteValidation validation;

        public CreateDelivery(DataGrid dataGrid)
        {
            InitializeComponent();

            this.dataGrid = dataGrid;

            validation = new DeliveryNoteValidation();
            comboBoxRepo = new ComboBoxRepoImpl();
            deliveryNoteCrud = new DeliveryNoteRepoImpl();

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
                    deliveryNoteCrud.create(deliveryNote);
                    deliveryNoteCrud.fetchToGrid(dataGrid);

                    this.Close();
                }
            } else
            {
                MessageBox.Show("Выберите подразделение!");
            }
        }
    }
}