using System;
using System.Windows;
using System.Windows.Controls;
using Warehouse.Entity;
using Warehouse.storage1;

namespace Warehouse.View.RecordCard
{
    public partial class CreateRecordCard : Window
    {
        private DataGrid dataGrid;
        private ComboBoxRepo comboBoxRepo;
        private CrudRepo<RecordCardEntity> crudRepo;
        
        public CreateRecordCard(DataGrid dataGrid)
        {
            InitializeComponent();

            this.dataGrid = dataGrid;

            comboBoxRepo = new ComboBoxRepoImpl();
            crudRepo = new RecordCardRepoImpl();

            DatePicker.Text = DateTime.Today.ToString("yyyy-MM-dd");

            comboBoxRepo.insertDivisionsIntoComboBox(DivisionCombo);
            comboBoxRepo.insertEmployeesIntoComboBox(EmployeeCombo);
            comboBoxRepo.insertProductsIntoComboBox(ProductCombo);
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

                RecordCardEntity recordCard = new RecordCardEntity(product.id, division.id, employee.id, date);

                crudRepo.create(recordCard);
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
