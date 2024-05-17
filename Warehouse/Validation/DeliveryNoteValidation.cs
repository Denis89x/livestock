using System.Windows;
using Warehouse.Entity;

namespace Warehouse.Validation
{
    internal class DeliveryNoteValidation
    {
        CommonValidation commonValidation;

        public DeliveryNoteValidation()
        {
            commonValidation = new CommonValidation();
        }

        public bool isDeliveryNoteValid(DeliveryNoteEntity delivery)
        {
            if (!commonValidation.isDateValid(delivery.date))
            {
                MessageBox.Show("Дата некорректна. Пожалуйста, убедитесь, что введенная дата находится в допустимом формате и соответствует текущей дате. Не позже чем сегодня и не раньше чем 10 дней с сегодняшнего дня!");

                return false;
            }

            if (!commonValidation.isTitleValid(delivery.assignment))
            {
                MessageBox.Show("Введите текст, содержащий от 3 до 50 символов.");

                return false;
            }

            return true;
        }
    }
}