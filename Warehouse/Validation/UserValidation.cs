using System.Text.RegularExpressions;
using System.Windows;
using Warehouse.Entity;
using Warehouse.Storage;

namespace Warehouse.Validation
{
    internal class UserValidation
    {
        private AuthRepo authRepo;

        public UserValidation()
        {
            authRepo = new AuthRepoImpl();
        }

        private bool isUsernameCorrect(string username)
        {
            string patternUsername = @"^[a-zA-Z]{4,12}$";

            if (!Regex.IsMatch(username, patternUsername))
            {
                return false;
            }

            return true;
        }

        private bool isPasswordCorrect(string password)
        {
            string pattern = @"^(?=.*[A-Z])(?=.*\d)[A-Za-z\d]{6,30}$";

            if (!Regex.IsMatch(password, pattern))
            {
                return false;
            }

            return true;
        }

        private bool isPasswordMatching(string firstPassword, string secondPassword)
        {
            return firstPassword.Equals(secondPassword);
        }

        public bool isRegistrationAllowed(UserEntity userEntity)
        {
            if (!isPasswordMatching(userEntity.password, userEntity.repeatedPassword))
            {
                MessageBox.Show("Пароли не совпадают!");
                return false;
            }
                
            if (authRepo.isUserExistsByUsername(userEntity.username))
            {
                MessageBox.Show("Такой логин уже используется!");
                return false;
            }
                

            if (!isPasswordCorrect(userEntity.password))
            {
                MessageBox.Show("Минимальный размер пароля - 6, минимум 1 заглавная буква и минимум 1 цифра, максимальный размер 30!");
                return false;
            }
                

            if (!isUsernameCorrect(userEntity.username))
            {
                MessageBox.Show("Логин должен быть длинной от 4х и до 12 латинскими символами!");
                return false;
            }
                
            return true;
        }
    }
}