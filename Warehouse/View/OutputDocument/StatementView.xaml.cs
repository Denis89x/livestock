using System.Windows;
using Warehouse.Entity;
using Warehouse.Service;
using Warehouse.Storage;

namespace Warehouse.View.OutputDocument
{
    public partial class StatementView : Window
    {
        private ComboBoxRepo comboBoxRepo;
        private StatementOutput statementOutput;

        public StatementView()
        {
            InitializeComponent();

            comboBoxRepo = new ComboBoxRepoImpl();

            comboBoxRepo.insertStatementIntoComboBox(StatementCombo);
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {

            ComboBoxEntity statement = (ComboBoxEntity)StatementCombo.SelectedItem;

            if (statement != null)
            {
                statementOutput = new StatementOutput();

                statementOutput.generateWordDocument(statement.id);
            }
            else
            {
                MessageBox.Show("Выберите ведомость!");
            }
        }
    }
}