using System.Windows;
using System.Windows.Controls;
using Warehouse.Entity;
using Warehouse.storage1;

namespace Warehouse.View.Cattle
{
    public partial class CreateCattle : Window
    {
        CattleRepo cattleRepo;

        DataGrid dataGrid;

        public CreateCattle(DataGrid dataGrid)
        {
            InitializeComponent();

            cattleRepo = new CattleRepoImpl();
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

            cattleRepo.createCattle(cattle);
            cattleRepo.fetchCattleToGrid(dataGrid);

            CattleTypeBox.Text = "";
        }
    }
}
