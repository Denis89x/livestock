using System.Windows;
using System.Windows.Controls;
using Warehouse.Entity;
using Warehouse.storage1;

namespace Warehouse.View.Division
{
    public partial class EditDivision : Window
    {
        long divisionId;
        DataGrid dataGrid;
        DivisionRepo divisionRepo;

        public EditDivision(long divisionId, string divisionType, DataGrid dataGrid)
        {
            InitializeComponent();

            this.divisionId = divisionId;
            this.divisionRepo = new DivisionRepoImpl();
            this.dataGrid = dataGrid;

            DivisionTypeBox.Text = divisionType;
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            string divisionType = DivisionTypeBox.Text;

            DivisionEntity division = new DivisionEntity(divisionId, divisionType);

            divisionRepo.updateDivision(division);
            divisionRepo.fetchDivisionToGrid(dataGrid);

            this.Close();
        }
    }
}
