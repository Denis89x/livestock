using System.Windows;
using Warehouse.Entity;

namespace Warehouse.Validation
{
    internal class ProductValidation
    {
        CommonValidation commonValidation;

        public ProductValidation()
        {
            commonValidation = new CommonValidation();
        }

        public bool isProductValid(ProductEntity product)
        {
            if (!commonValidation.isStringFieldValid(product.title))
            {
                MessageBox.Show("Название должно содержать хотя бы одну букву, может содержать буквы, цифры и символы, но не должен состоять только из цифр. Длина не должна превышать 35 символов.");

                return false;
            }

            if (!commonValidation.isStringFieldValid(product.sort))
            {
                MessageBox.Show("Сорт должен содержать хотя бы одну букву, может содержать буквы, цифры и символы, но не должен состоять только из цифр. Длина не должна превышать 35 символов.");

                return false;
            } 

            return true;
        }
    }
}