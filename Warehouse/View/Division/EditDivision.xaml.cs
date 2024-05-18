using System.Windows;
using System.Windows.Controls;
using Warehouse.Entity;
using Warehouse.storage1;
using Warehouse.Validation;

namespace Warehouse.View.Division
{
    public partial class EditDivision : Window
    {
        private long divisionId;
        private DataGrid dataGrid;
        private CrudRepo<DivisionEntity> divisionCrud;
        private DivisionValidation validation;

        public EditDivision(long divisionId, string divisionType, DataGrid dataGrid)
        {
            InitializeComponent();

            this.divisionId = divisionId;
            this.dataGrid = dataGrid;

            validation = new DivisionValidation();
            divisionCrud = new DivisionRepoImpl();
            
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

            if (validation.isDivisionValid(division))
            {
                divisionCrud.update(division);
                divisionCrud.fetchToGrid(dataGrid);

                this.Close();
            }
        }
    }
}