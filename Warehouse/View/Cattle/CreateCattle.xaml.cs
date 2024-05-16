using System.Windows;
using System.Windows.Controls;
using Warehouse.Entity;
using Warehouse.storage1;
using Warehouse.Validation;

namespace Warehouse.View.Cattle
{
    public partial class CreateCattle : Window
    {
        CattleValidation cattleValidation;
        CattleRepo cattleRepo;

        DataGrid dataGrid;

        public CreateCattle(DataGrid dataGrid)
        {
            InitializeComponent();

            cattleRepo = new CattleRepoImpl();
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
                cattleRepo.createCattle(cattle);
                cattleRepo.fetchCattleToGrid(dataGrid);

                CattleTypeBox.Text = "";
            }
        }
    }
}
