using System.Windows;
using System.Windows.Controls;
using Warehouse.Entity;
using Warehouse.storage1;
using Warehouse.Validation;

namespace Warehouse.View.Division
{
    public partial class AddDivision : Window
    {
        DivisionRepo divisionRepo;
        DivisionValidation validation;

        DataGrid dataGrid;

        public AddDivision(DataGrid dataGrid)
        {
            InitializeComponent();

            this.dataGrid = dataGrid;

            divisionRepo = new DivisionRepoImpl();
            validation = new DivisionValidation();
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            string divisionType = DivisionTypeBox.Text;

            DivisionEntity division = new DivisionEntity(divisionType);

            if (validation.isDivisionValid(division))
            {
                divisionRepo.createDivision(division);
                divisionRepo.fetchDivisionToGrid(dataGrid);

                DivisionTypeBox.Text = "";
            }
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}