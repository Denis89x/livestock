using System;
using System.Windows;
using System.Windows.Controls;
using Warehouse.Entity;
using Warehouse.Storage;
using Warehouse.Validation;

namespace Warehouse.View.Auth
{
    public partial class RegistrationView : Window
    {
        private AuthRepo authRepo;
        private UserValidation validation;

        public RegistrationView()
        {
            InitializeComponent();

            authRepo = new AuthRepoImpl();
            validation = new UserValidation();
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameBox.Text;
            string password = PasswordBox.Text;
            string repeatedPassword = RepeatedPasswordBox.Text;

            if (RoleBox.SelectedItem != null)
            {
                string role = ((ComboBoxItem)RoleBox.SelectedItem).Tag.ToString();

                UserEntity user = new UserEntity(username, password, repeatedPassword, role);

                if (validation.isRegistrationAllowed(user))
                {
                    authRepo.createUser(user);
                    MessageBox.Show("Регистрация прошла успешно!");
                }

                this.Close();
            }
            else
            {
                MessageBox.Show("Выберите уровень доступа!");
            }
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
