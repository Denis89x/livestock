using System;
using System.Windows;
using System.Windows.Controls;
using Warehouse.Entity;
using Warehouse.Storage;
using Warehouse.Validation;

namespace Warehouse.View.Statement
{
    public partial class CreateStatement : Window
    {
        private DataGrid dataGrid;
        private CrudRepo<StatementEntity> statementCrud;
        private StatementValidation validation;
        private ComboBoxRepo comboBoxRepo;

        public CreateStatement(DataGrid dataGrid)
        {
            InitializeComponent();

            this.dataGrid = dataGrid;

            validation = new StatementValidation();
            statementCrud = new StatementRepoImpl();
            comboBoxRepo = new ComboBoxRepoImpl();

            comboBoxRepo.insertDivisionsIntoComboBox(DivisionComboBox);
            comboBoxRepo.insertCattleIntoComboBox(CattleComboBox);
            comboBoxRepo.insertEmployeesIntoComboBox(EmployeeComboBox);
            comboBoxRepo.insertProductsIntoComboBox(ProductComboBox);

            DatePicker.Text = DateTime.Today.ToString("yyyy-MM-dd");
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxEntity division = (ComboBoxEntity)DivisionComboBox.SelectedItem;
            ComboBoxEntity employee = (ComboBoxEntity)EmployeeComboBox.SelectedItem;
            ComboBoxEntity cattle = (ComboBoxEntity)CattleComboBox.SelectedItem;
            ComboBoxEntity product = (ComboBoxEntity)ProductComboBox.SelectedItem;

            try
            {
                string date = DatePicker.SelectedDate.Value.ToString("yyyy-MM-dd");
                string supplyRate = SupplyRate.Text;
                string animalQuantity = AnimalQuantityBox.Text;
                string actualRate = ActualRateBox.Text;

                if (division != null && employee != null && cattle != null && product != null)
                {
                    StatementEntity statement = new StatementEntity(division.id, cattle.id, employee.id, product.id, date, supplyRate, animalQuantity, actualRate);

                    if (validation.isStatementValid(statement))
                    {
                        statementCrud.create(statement);
                        statementCrud.fetchToGrid(dataGrid);

                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Укажите все данные!");
                }
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("Выберите дату!");
            }
        }
    }
}