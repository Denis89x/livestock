using System.Windows;
using Warehouse.storage1;
using Warehouse.View.Main;

namespace Warehouse.View
{

    public partial class AuthView : Window
    {
        private AuthRepo authRepo;

        public AuthView()
        {
            InitializeComponent();
            this.authRepo = new AuthRepoImpl();
        }

        private void AuthorizationButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameBox.Text;
            string password = Password.Password;

            if (authRepo.checkExistsUser(username, password))
            {
                AuthSession.CurrentUsername = username;

                this.Hide();

                MainPage mainPage = new MainPage();
                mainPage.Show();
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль");
            }
        }
    }
}
