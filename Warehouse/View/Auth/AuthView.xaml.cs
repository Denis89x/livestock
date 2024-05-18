using System.Windows;
using Warehouse.Storage;
using Warehouse.Validation;
using Warehouse.View.Main;

namespace Warehouse.View
{

    public partial class AuthView : Window
    {
        private AuthRepo authRepo;

        public AuthView()
        {
            InitializeComponent();
            authRepo = new AuthRepoImpl();
        }

        private void AuthorizationButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameBox.Text;
            string password = Password.Password;

            if (authRepo.isUserCredentialsValid(username, password))
            {
                AuthSession.currentUsername = username;

                this.Hide();

                string userRole = authRepo.fetchRoleByUsername(username);

                MainPage mainPage = new MainPage(userRole);
                mainPage.Show();
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль");
            }
        }
    }
}
