using System.Windows;
using System.Windows.Controls;
using Warehouse.Entity;
using Warehouse.Storage;
using Warehouse.Validation;

namespace Warehouse.View.Cattle
{
    public partial class CreateCattle : Window
    {
        private CattleValidation cattleValidation;
        private CrudRepo<CattleEntity> cattleCrud;

        private DataGrid dataGrid;

        public CreateCattle(DataGrid dataGrid)
        {
            InitializeComponent();

            cattleCrud = new CattleRepoImpl();
            cattleValidation = new CattleValidation();

            this.dataGrid = dataGrid;
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            string cattleType = CattleTypeBox.Text;

            CattleEntity cattle = new CattleEntity(cattleType);

            if (cattleValidation.isCattleValid(cattle))
            {
                cattleCrud.create(cattle);
                cattleCrud.fetchToGrid(dataGrid);

                CattleTypeBox.Text = "";
            }
        }
    }
}
