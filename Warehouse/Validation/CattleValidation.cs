using System.Windows;
using Warehouse.Entity;

namespace Warehouse.Validation
{
    internal class CattleValidation
    {
        CommonValidation commonValidation;

        public CattleValidation()
        {
            commonValidation = new CommonValidation();
        }

        public bool isCattleValid(CattleEntity cattleEntity) {
            if (!commonValidation.isStringFieldValid(cattleEntity.cattleType))
            {
                MessageBox.Show("Тип скота должен содержать хотя бы одну букву, может содержать буквы, цифры и символы, но не должен состоять только из цифр. Длина не должна превышать 35 символов.");
                return false;
            }

            return true;
        }
    }
}