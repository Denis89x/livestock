using System.Windows;
using Warehouse.Entity;

namespace Warehouse.Validation
{
    internal class DivisionValidation
    {
        CommonValidation commonValidation;

        public DivisionValidation()
        {
            commonValidation = new CommonValidation();
        }

        public bool isDivisionValid(DivisionEntity division)
        {
            if (!commonValidation.isStringFieldValid(division.divisionType))
            {
                MessageBox.Show("Тип подразделения должен содержать хотя бы одну букву, может содержать буквы, цифры и символы, но не должен состоять только из цифр. Длина не должна превышать 35 символов.");
                return false;
            }

            return true;
        }
    }
}
