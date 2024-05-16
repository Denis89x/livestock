using System;
using System.Windows;
using System.Windows.Controls;
using Warehouse.Entity;
using Warehouse.storage1;

namespace Warehouse.View.RecordCard
{
    public partial class EditRecordCard : Window
    {
        private long recordCardId;
        private DataGrid dataGrid;
        private ComboBoxRepo comboBoxRepo;
        private CrudRepo<RecordCardEntity> crudRepo;

        public EditRecordCard(long recordCardId, string product, string division, string employee, string date, DataGrid dataGrid)
        {
            InitializeComponent();

            this.recordCardId = recordCardId;
            this.dataGrid = dataGrid;

            comboBoxRepo = new ComboBoxRepoImpl();
            crudRepo = new RecordCardRepoImpl();

            DatePicker.Text = DateTime.Today.ToString("yyyy-MM-dd");

            comboBoxRepo.insertDivisionsIntoComboBox(DivisionCombo);
            comboBoxRepo.insertEmployeesIntoComboBox(EmployeeCombo);
            comboBoxRepo.insertProductsIntoComboBox(ProductCombo);

            DatePicker.Text = date;
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxEntity division = (ComboBoxEntity)DivisionCombo.SelectedItem;
            ComboBoxEntity employee = (ComboBoxEntity)EmployeeCombo.SelectedItem;
            ComboBoxEntity product = (ComboBoxEntity)ProductCombo.SelectedItem;

            try
            {
                string date = DatePicker.SelectedDate.Value.ToString("yyyy-MM-dd");

                RecordCardEntity recordCard = new RecordCardEntity(recordCardId, product.id, division.id, employee.id, date);

                crudRepo.update(recordCard);
                crudRepo.fetchToGrid(dataGrid);

                this.Close();
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("Выберите дату!");
            }
        }
    }
}
