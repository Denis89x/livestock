using System.Text.RegularExpressions;
using System.Windows;
using Warehouse.Entity;

namespace Warehouse.Validation
{
    internal class EmployeeValidation
    {
        CommonValidation commonValidation;

        public EmployeeValidation()
        {
            commonValidation = new CommonValidation();
        }

        private bool isNameValid(string name)
        {
            string pattern = @"^(?=.*[a-zA-Zа-яА-Я])[a-zA-Zа-яА-Я\s]{3,35}$";

            return Regex.IsMatch(name, pattern);
        }

        public bool isEmployeeValid(EmployeeEntity employee)
        {
            if (!isNameValid(employee.surname))
            {
                MessageBox.Show("Фамилия должена содержать от 3 до 35 символов, включая буквы русского и английского алфавитов, а также пробелы. Строка не может состоять только из пробелов.");

                return false;
            }

            if (!isNameValid(employee.firstName))
            {
                MessageBox.Show("Имя должно содержать от 3 до 35 символов, включая буквы русского и английского алфавитов, а также пробелы. Строка не может состоять только из пробелов.");

                return false;
            }

            if (!isNameValid(employee.patronymic))
            {
                MessageBox.Show("Отчество должено содержать от 3 до 35 символов, включая буквы русского и английского алфавитов, а также пробелы. Строка не может состоять только из пробелов.");

                return false;
            }

            if (!commonValidation.isStringFieldValid(employee.position))
            {
                MessageBox.Show("Должность должна содержать хотя бы одну букву, может содержать буквы, цифры и символы, но не должен состоять только из цифр. Длина не должна превышать 35 символов.");
                
                return false;
            }

            return true;
        }
    }
}