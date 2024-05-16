using System.Text.RegularExpressions;
using System.Windows;

namespace Warehouse.Validation
{
    internal class ContractorValidation
    {
        CommonValidation commonValidation;

        public ContractorValidation()
        {
            commonValidation = new CommonValidation();
        }

        private bool isPhoneNumberCorrect(string phoneNumber)
        {
            string pattern = @"^(\+375|80)(44|29|25|33)\d{7}$";

            if (!Regex.IsMatch(phoneNumber, pattern))
            {
                return false;
            }

            return true;
        }

        public bool isContractorValid(ContractorEntity contractor)
        {
            if (!commonValidation.isStringFieldValid(contractor.address))
            {
                MessageBox.Show("Адрес должен содержать хотя бы одну букву, может содержать буквы, цифры и символы, но не должен состоять только из цифр. Длина не должна превышать 35 символов.");
                return false;
            }

            if (!commonValidation.isStringFieldValid(contractor.title))
            {
                MessageBox.Show("Название должно содержать хотя бы одну букву, может содержать буквы, цифры и символы, но не должен состоять только из цифр. Длина не должна превышать 35 символов.");
                return false;
            }

            if (!isPhoneNumberCorrect(contractor.phoneNumber))
            {
                MessageBox.Show("Введите корректный номер телефона! Пример: +375441234567");
                return false;
            }

            if (!commonValidation.isIntFieldValid(contractor.settlementAccount))
            {
                MessageBox.Show("Некорректный расчетный счет контрагента! Расчетный счет должен состоять только из цифр и быть от 3 до 12 символов.");
                return false;
            }

            if (!commonValidation.isStringFieldValid(contractor.bankName))
            {
                MessageBox.Show("Название банка должено содержать хотя бы одну букву, может содержать буквы, цифры и символы, но не должен состоять только из цифр. Длина не должна превышать 35 символов.");
                return false;
            }

            return true;
        }
    }
}