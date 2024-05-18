using System;
using System.Windows;
using System.Windows.Controls;
using Warehouse.Entity;
using Warehouse.storage1;
using Warehouse.Validation;

namespace Warehouse.View.RecordCard
{
    public partial class CreateRecordCard : Window
    {
        private CommonValidation commonValidation;
        private DataGrid dataGrid;
        private ComboBoxRepo comboBoxRepo;
        private CrudRepo<RecordCardEntity> recordCrud;
        
        public CreateRecordCard(DataGrid dataGrid)
        {
            InitializeComponent();

            this.dataGrid = dataGrid;

            comboBoxRepo = new ComboBoxRepoImpl();
            recordCrud = new RecordCardRepoImpl();
            commonValidation = new CommonValidation();

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
                if (division != null && employee != null && product != null) {
                    string date = DatePicker.SelectedDate.Value.ToString("yyyy-MM-dd");
                    if (commonValidation.isDateValid(date))
                    {
                        RecordCardEntity recordCard = new RecordCardEntity(product.id, division.id, employee.id, date);

                        recordCrud.create(recordCard);
                        recordCrud.fetchToGrid(dataGrid);

                        this.Close();
                    }
                } else
                {
                    MessageBox.Show("Заполните все поля!");
                }
                
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("Выберите дату!");
            }
        }
    }
}