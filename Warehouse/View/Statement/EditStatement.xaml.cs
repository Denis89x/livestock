using System;
using System.Windows;
using System.Windows.Controls;
using Warehouse.Entity;
using Warehouse.Storage;
using Warehouse.Validation;

namespace Warehouse.View.Statement
{
    public partial class EditStatement : Window
    {
        private long statementId;
        private DataGrid dataGrid;
        private CrudRepo<StatementEntity> statementCrud;
        private ComboBoxRepo comboBoxRepo;
        private StatementValidation validation;

        public EditStatement(
            long statementId, string division, string cattle, string employee, string product, 
            string date, string supplyRate, string animalQuantity, string actualRate, DataGrid dataGrid)
        {
            InitializeComponent();

            this.statementId = statementId;
            this.dataGrid = dataGrid;

            validation = new StatementValidation();
            statementCrud = new StatementRepoImpl();
            comboBoxRepo = new ComboBoxRepoImpl();

            comboBoxRepo.insertCattleIntoComboBox(CattleComboBox);
            comboBoxRepo.insertDivisionsIntoComboBox(DivisionComboBox);
            comboBoxRepo.insertEmployeesIntoComboBox(EmployeeComboBox);

            ProductComboBox.Items.Add(product);
            ProductComboBox.SelectedIndex = 0;

            DatePicker.Text = date;
            SupplyRate.Text = supplyRate;
            AnimalQuantityBox.Text = animalQuantity;
            ActualRateBox.Text = actualRate;
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxEntity division = (ComboBoxEntity)DivisionComboBox.SelectedItem;
            ComboBoxEntity employee = (ComboBoxEntity)EmployeeComboBox.SelectedItem;
            ComboBoxEntity cattle = (ComboBoxEntity)CattleComboBox.SelectedItem;

            string supplyRate = SupplyRate.Text;
            string animalQuantity = AnimalQuantityBox.Text;
            string actualRate = ActualRateBox.Text;

            try
            {
                string date = DatePicker.SelectedDate.Value.ToString("yyyy-MM-dd");

                if (division != null && employee != null && cattle != null)
                {
                    StatementEntity statement = new StatementEntity(statementId, division.id, cattle.id, employee.id, 0, date, supplyRate, animalQuantity, actualRate);

                    if (validation.isStatementValid(statement))
                    {
                        statementCrud.update(statement);
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

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}