using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Warehouse.Entity;
using Warehouse.storage1;

namespace Warehouse.View.Cattle
{
    public partial class EditCattle : Window
    {
        long cattleId;
        DataGrid dataGrid;
        CattleRepo cattleRepo;

        public EditCattle(long cattleId, string cattleType, DataGrid dataGrid)
        {
            InitializeComponent();

            this.cattleId = cattleId;
            this.dataGrid = dataGrid;
            this.cattleRepo = new CattleRepoImpl();

            CattleTypeBox.Text = cattleType;
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            string cattleType = CattleTypeBox.Text;

            CattleEntity cattle = new CattleEntity(cattleId, cattleType);

            cattleRepo.updateCattle(cattle);
            cattleRepo.fetchCattleToGrid(dataGrid);

            this.Close();
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
