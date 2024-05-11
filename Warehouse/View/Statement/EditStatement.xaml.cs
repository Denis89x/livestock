using System;
using System.Windows;
using System.Windows.Controls;
using Warehouse.Entity;
using Warehouse.storage1;

namespace Warehouse.View.Statement
{
    public partial class EditStatement : Window
    {
        long statementId;
        DataGrid dataGrid;
        StatementRepo statementRepo;
        StatementComboBoxRepo statementComboBoxRepo;

        public EditStatement(long statementId, string division, string cattle, string employee,  
            string product, string date, string foundation, string title, string unit, string supplyRate,
            string animalQuantity, string feedQuantity, DataGrid dataGrid)
        {
            InitializeComponent();

            this.statementId = statementId;
            this.dataGrid = dataGrid;

            MessageBox.Show("ID: " + statementId);

            statementRepo = new StatementRepoImpl();
            statementComboBoxRepo = new StatementComboBoxRepoImpl();

            /*            statementComboBoxRepo.insertDivisionsIntoComboBox(DivisionComboBox);
                        DivisionComboBox.SelectedItem = DivisionComboBox.Items.IndexOf(division); TODO: CHANGE */
            /*DivisionComboBox.Items.Add(division);
            DivisionComboBox.SelectedIndex = 0;
            EmployeeComboBox.Items.Add(employee); 
            EmployeeComboBox.SelectedIndex = 0;
            CattleComboBox.Items.Add(cattle);
            CattleComboBox.SelectedIndex = 0;
            ProductComboBox.Items.Add(product);
            ProductComboBox.SelectedIndex = 0;*/

            statementComboBoxRepo.insertCattleIntoComboBox(CattleComboBox);
            statementComboBoxRepo.insertDivisionsIntoComboBox(DivisionComboBox);
            statementComboBoxRepo.insertEmployeesIntoComboBox(EmployeeComboBox);
            statementComboBoxRepo.insertProductsIntoComboBox(ProductComboBox);

            DatePicker.Text = date;
            FoundationBox.Text = foundation;
            TitleBox.Text = title;
            UnitBox.Text = unit;
            SupplyRateBox.Text = supplyRate;
            AnimalQuantityBox.Text = animalQuantity;
            FeedQuantity.Text = feedQuantity;
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxEntity division = (ComboBoxEntity)DivisionComboBox.SelectedItem;
            ComboBoxEntity employee = (ComboBoxEntity)EmployeeComboBox.SelectedItem;
            ComboBoxEntity cattle = (ComboBoxEntity)CattleComboBox.SelectedItem;
            ComboBoxEntity product = (ComboBoxEntity)ProductComboBox.SelectedItem;

            string foundation = FoundationBox.Text;
            string title = TitleBox.Text;
            string unit = UnitBox.Text;
            string supplyRate = SupplyRateBox.Text;
            string animalQuantity = AnimalQuantityBox.Text;
            string feedQuantity = FeedQuantity.Text;

            try
            {
                string date = DatePicker.SelectedDate.Value.ToString("yyyy-MM-dd");

                StatementEntity statement = new StatementEntity(statementId, division.id, cattle.id, employee.id, product.id, date, foundation, title, unit, supplyRate, animalQuantity, feedQuantity);

                statementRepo.updateStatement(statement);
                statementRepo.fetchStatementToGrid(dataGrid);

                this.Close();
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("Выберите дату!");
            }
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
            UnitBox.Visibility = Visibility.Visible;
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
            UnitBox.Visibility = Visibility.Collapsed;
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
            UnitBox.Visibility = Visibility.Collapsed;
            ReturnFirst.Visibility = Visibility.Collapsed;
            NextFirst.Visibility = Visibility.Collapsed;
        }

        private void PreviewSecond_Click(object sender, RoutedEventArgs e)
        {
            DatePicker.Visibility = Visibility.Visible;
            FoundationBox.Visibility = Visibility.Visible;
            TitleBox.Visibility = Visibility.Visible;
            UnitBox.Visibility = Visibility.Visible;
            ReturnFirst.Visibility = Visibility.Visible;
            NextFirst.Visibility = Visibility.Visible;

            SupplyRateBox.Visibility = Visibility.Collapsed;
            AnimalQuantityBox.Visibility = Visibility.Collapsed;
            FeedQuantity.Visibility = Visibility.Collapsed;
            Confirm.Visibility = Visibility.Collapsed;
            PreviewSecond.Visibility = Visibility.Collapsed;
        }
    }
}
