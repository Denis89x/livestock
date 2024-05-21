using System.Windows;
using Warehouse.Entity;

namespace Warehouse.Validation
{
    internal class RecordCardValidation
    {
        CommonValidation commonValidation;

        public RecordCardValidation()
        {
            commonValidation = new CommonValidation();
        }

        public bool isRecordCardValid(RecordCardEntity recordCard)
        {
            if (!commonValidation.isNumberInRange(recordCard.cowQuantity, 1000))
            {
                MessageBox.Show("Количество коров должно быть от 1 до 1000!");

                return false;
            }

            if (!commonValidation.isNumberInRange(recordCard.morning, 1000))
            {
                MessageBox.Show("Надой утром должен быть от 1 до 1000!");

                return false;
            }

            if (!commonValidation.isNumberInRange(recordCard.midday, 1000))
            {
                MessageBox.Show("Надой днём должен быть от 1 до 1000!");

                return false;
            }

            if (!commonValidation.isNumberInRange(recordCard.evening, 1000))
            {
                MessageBox.Show("Надой вечером должен быть от 1 до 1000!");

                return false;
            }

            if (!commonValidation.isDateValid(recordCard.date))
            {
                MessageBox.Show("Дата некорректна. Пожалуйста, убедитесь, что введенная дата находится в допустимом формате и соответствует текущей дате. Не позже чем сегодня и не раньше чем 10 дней с сегодняшнего дня!");

                return false;
            }

            return true;
        }
    }
}