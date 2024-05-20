using System.Windows;
using Warehouse.Entity;
using Warehouse.Service;
using Warehouse.Storage;

namespace Warehouse.View.OutputDocument
{
    public partial class DeliveryNoteView : Window
    {
        private ComboBoxRepo comboBoxRepo;
        private DeliveryNoteOutput deliveryNoteOutput;

        public DeliveryNoteView()
        {
            InitializeComponent();

            comboBoxRepo = new ComboBoxRepoImpl();

            comboBoxRepo.insertValidDeliveryIntoComboBox(DeliveryNoteCombo);
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {

            ComboBoxEntity deliveryNote = (ComboBoxEntity)DeliveryNoteCombo.SelectedItem;

            if (deliveryNote != null)
            {
                deliveryNoteOutput = new DeliveryNoteOutput();

                deliveryNoteOutput.generateWordDocument(deliveryNote.id);
            }
            else
            {
                MessageBox.Show("Выберите накладную!");
            }
        }
    }
}