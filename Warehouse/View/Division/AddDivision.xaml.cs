using System.Windows;
using System.Windows.Controls;
using Warehouse.Entity;
using Warehouse.storage1;

namespace Warehouse.View.Division
{
    public partial class AddDivision : Window
    {
        DivisionRepo divisionRepo;

        DataGrid dataGrid;

        public AddDivision(DataGrid dataGrid)
        {
            InitializeComponent();

            this.dataGrid = dataGrid;
            divisionRepo = new DivisionRepoImpl();
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            string divisionType = DivisionTypeBox.Text;

            DivisionEntity division = new DivisionEntity(divisionType);

            divisionRepo.createDivision(division);
            divisionRepo.fetchDivisionToGrid(dataGrid);

            DivisionTypeBox.Text = "";
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
