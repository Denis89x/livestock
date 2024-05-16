using System.Text.RegularExpressions;

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
            string pattern = @"^\d{3,12}$";

            return Regex.IsMatch(field, pattern);
        }
    }
}
