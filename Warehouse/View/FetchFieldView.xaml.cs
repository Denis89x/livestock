using System.Windows;

namespace Warehouse.View
{
    public partial class FetchFieldView : Window
    {
        public string field { get; set; }

        public FetchFieldView()
        {
            InitializeComponent();
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            field = FieldBox.Text;

            if (field == null || field == " ")
            {
                MessageBox.Show("Введите значение!");
            } else
            {
                this.Close();
            }
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
