using System.Windows;
using Warehouse.Profile;
using Warehouse.View.AddPage;

namespace Warehouse.View.Main
{
    public partial class MainPage : Window
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}