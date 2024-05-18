using System.Windows;
using System.Windows.Controls;
using Warehouse.Entity;
using Warehouse.storage1;
using Warehouse.Validation;

namespace Warehouse.View.Cattle
{
    public partial class EditCattle : Window
    {
        private long cattleId;
        private DataGrid dataGrid;
        private CrudRepo<CattleEntity> cattleCrud;
        private CattleValidation cattleValidation;

        public EditCattle(long cattleId, string cattleType, DataGrid dataGrid)
        {
            InitializeComponent();

            this.cattleId = cattleId;
            this.dataGrid = dataGrid;

            cattleCrud = new CattleRepoImpl();
            cattleValidation = new CattleValidation();

            CattleTypeBox.Text = cattleType;
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            string cattleType = CattleTypeBox.Text;

            CattleEntity cattle = new CattleEntity(cattleId, cattleType);

            if (cattleValidation.isCattleValid(cattle))
            {
                cattleCrud.update(cattle);
                cattleCrud.fetchToGrid(dataGrid);

                this.Close();
            }
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}