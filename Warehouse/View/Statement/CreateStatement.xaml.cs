using System;
using System.Windows;
using System.Windows.Controls;
using Warehouse.Entity;
using Warehouse.storage1;
using Warehouse.Validation;

namespace Warehouse.View.Statement
{
    public partial class CreateStatement : Window
    {
        private DataGrid dataGrid;
        private StatementRepo statementRepo;
        private StatementComboBoxRepo statementComboBoxRepo;
        private StatementValidation validation;

        public CreateStatement(DataGrid dataGrid)
        {
            InitializeComponent();

            this.dataGrid = dataGrid;

            validation = new StatementValidation();
            statementRepo = new StatementRepoImpl();
            statementComboBoxRepo = new StatementComboBoxRepoImpl();

            statementComboBoxRepo.insertDivisionsIntoComboBox(DivisionComboBox);
            statementComboBoxRepo.insertCattleIntoComboBox(CattleComboBox);
            statementComboBoxRepo.insertEmployeesIntoComboBox(EmployeeComboBox);
            statementComboBoxRepo.insertProductsIntoComboBox(ProductComboBox);

            DatePicker.Text = DateTime.Today.ToString("yyyy-MM-dd");
        }

        private void ReturnFirst_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void NextFirst_Click(object sender, RoutedEventArgs e)
        {
            DatePicker.Visibility = Visibility.Visible;
            FoundationBox.Visibility = Visibility.Visible;
            TitleBox.Visibility = Visibility.Visible;
            UnitCombo.Visibility = Visibility.Visible;
            PreviewFirst.Visibility = Visibility.Visible;
            NextSecond.Visibility = Visibility.Visible;

            ReturnFirst.Visibility = Visibility.Collapsed;
            NextFirst.Visibility = Visibility.Collapsed;
            DivisionComboBox.Visibility = Visibility.Collapsed;
            CattleComboBox.Visibility = Visibility.Collapsed;
            EmployeeComboBox.Visibility = Visibility.Collapsed;
            ProductComboBox.Visibility = Visibility.Collapsed;
        }

        private void PreviewFirst_Click(object sender, RoutedEventArgs e)
        {
            DivisionComboBox.Visibility = Visibility.Visible;
            CattleComboBox.Visibility = Visibility.Visible;
            EmployeeComboBox.Visibility = Visibility.Visible;
            ProductComboBox.Visibility = Visibility.Visible;
            ReturnFirst.Visibility = Visibility.Visible;
            NextFirst.Visibility = Visibility.Visible;

            PreviewFirst.Visibility = Visibility.Collapsed;
            NextSecond.Visibility = Visibility.Collapsed;
            DatePicker.Visibility = Visibility.Collapsed;
            FoundationBox.Visibility = Visibility.Collapsed;
            TitleBox.Visibility = Visibility.Collapsed;
            UnitCombo.Visibility = Visibility.Collapsed;
        }

        private void NextSecond_Click(object sender, RoutedEventArgs e)
        {
            SupplyRateBox.Visibility = Visibility.Visible;
            AnimalQuantityBox.Visibility = Visibility.Visible;
            FeedQuantity.Visibility = Visibility.Visible;
            Confirm.Visibility = Visibility.Visible;
            PreviewSecond.Visibility = Visibility.Visible;

            DatePicker.Visibility = Visibility.Collapsed;
            FoundationBox.Visibility = Visibility.Collapsed;
            TitleBox.Visibility = Visibility.Collapsed;
            UnitCombo.Visibility = Visibility.Collapsed;
            ReturnFirst.Visibility = Visibility.Collapsed;
            NextFirst.Visibility = Visibility.Collapsed;
        }

        private void PreviewSecond_Click(object sender, RoutedEventArgs e)
        {
            DatePicker.Visibility = Visibility.Visible;
            FoundationBox.Visibility = Visibility.Visible;
            TitleBox.Visibility = Visibility.Visible;
            UnitCombo.Visibility = Visibility.Visible;
            ReturnFirst.Visibility = Visibility.Visible;
            NextFirst.Visibility = Visibility.Visible;

            SupplyRateBox.Visibility = Visibility.Collapsed;
            AnimalQuantityBox.Visibility = Visibility.Collapsed;
            FeedQuantity.Visibility = Visibility.Collapsed;
            Confirm.Visibility = Visibility.Collapsed;
            PreviewSecond.Visibility = Visibility.Collapsed;
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
                string foundation = FoundationBox.Text;
                string title = TitleBox.Text;
                string supplyRate = SupplyRateBox.Text;
                string animalQuantity = AnimalQuantityBox.Text;
                string feedQuantity = FeedQuantity.Text;

                if (division != null && employee != null && cattle != null && product != null && (ComboBoxItem)UnitCombo.SelectedItem != null)
                {
                    string unit = ((ComboBoxItem)UnitCombo.SelectedItem).Content.ToString();
                    StatementEntity statement = new StatementEntity(division.id, cattle.id, employee.id, product.id, date, foundation, title, unit, supplyRate, animalQuantity, feedQuantity);

                    if (validation.isStatementValid(statement))
                    {
                        statementRepo.createStatement(statement);
                        statementRepo.fetchStatementToGrid(dataGrid);

                        this.Close();
                    }

                } else
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