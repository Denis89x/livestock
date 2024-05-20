using System.Windows;
using Warehouse.Entity;
using Warehouse.Service;
using Warehouse.Storage;

namespace Warehouse.View.OutputDocument
{
    public partial class WaybillView : Window
    {
        private ComboBoxRepo comboBoxRepo;
        private WaybillOutput waybillOutput;

        public WaybillView()
        {
            InitializeComponent();

            comboBoxRepo = new ComboBoxRepoImpl();

            comboBoxRepo.insertValidWaybillIntoComboBox(WaybillCombo);
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {

            ComboBoxEntity waybill = (ComboBoxEntity)WaybillCombo.SelectedItem;

            if (waybill != null)
            {
                waybillOutput = new WaybillOutput();

                waybillOutput.generateWordDocument(waybill.id);
            }
            else
            {
                MessageBox.Show("Выберите накладную!!");
            }
        }
    }
}