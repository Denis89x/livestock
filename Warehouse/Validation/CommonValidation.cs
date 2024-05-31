using System.Globalization;
using System;
using System.Text.RegularExpressions;
using System.Windows;

namespace Warehouse.Validation
{
    internal class CommonValidation
    {
        public bool isStringFieldValid(string field)
        {
            string pattern = @"^(?=.*[a-zA-Zа-яА-Я])[\w\d\s]{1,35}$";

            return Regex.IsMatch(field, pattern);
        }

        public bool isIntFieldValid(string field)
        {
            string pattern = @"^\d{13}$";

            return Regex.IsMatch(field, pattern);
        }

        public bool isNumberInRange(string number, int maxValue)
        {
            if (string.IsNullOrWhiteSpace(number))
            {
                return false;
            }

            if (!int.TryParse(number, out int parsedNumber))
            {
                return false;
            }

            if (parsedNumber < 1 || parsedNumber > maxValue)
            {
                return false;
            }

            return true;
        }

        public bool isDoubleNumberInRange(string number, double minValue, double maxValue)
        {
            if (string.IsNullOrWhiteSpace(number))
            {
                return false;
            }

            if (!double.TryParse(number, out double parsedNumber))
            {
                return false;
            }

            if (parsedNumber < minValue || parsedNumber > maxValue)
            {
                return false;
            }

            return true;
        }

        public bool isNumberInRange(string number, double minValue, double maxValue)
        {
            if (string.IsNullOrWhiteSpace(number))
            {
                return false;
            }

            if (!int.TryParse(number, out int parsedNumber))
            {
                return false;
            }

            if (parsedNumber < 1 || parsedNumber > maxValue || parsedNumber < minValue)
            {
                return false;
            }

            return true;
        }

        public bool isDateValid(string date)
        {
            
            DateTime parsedDate;
            if (!DateTime.TryParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate))
            {
                MessageBox.Show("Неккоректная дата!");
                return false;
            }

            DateTime today = DateTime.Today;
            DateTime minDate = today.AddDays(-10);
            DateTime maxDate = today;

            return parsedDate >= minDate && parsedDate <= maxDate;
        }

        public bool isTitleValid(string title)
        {
            string pattern = @"^[a-zA-Zа-яА-Я\s]{3,50}$";

            return Regex.IsMatch(title, pattern);
        }
    }
}