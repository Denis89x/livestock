using System.Windows;
using System.Windows.Controls;
using Warehouse.Entity;
using Warehouse.Storage;
using Warehouse.Validation;

namespace Warehouse.View.Division
{
    public partial class AddDivision : Window
    {
        private CrudRepo<DivisionEntity> divisionCrud;
        private DivisionValidation validation;

        private DataGrid dataGrid;

        public AddDivision(DataGrid dataGrid)
        {
            InitializeComponent();

            this.dataGrid = dataGrid;

            divisionCrud = new DivisionRepoImpl();
            validation = new DivisionValidation();
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            string divisionType = DivisionTypeBox.Text;

            DivisionEntity division = new DivisionEntity(divisionType);

            if (validation.isDivisionValid(division))
            {
                divisionCrud.create(division);
                divisionCrud.fetchToGrid(dataGrid);

                DivisionTypeBox.Text = "";
            }
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}