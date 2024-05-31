using System;
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
            if (!commonValidation.isNumberInRange(recordCard.cowQuantity, 10000))
            {
                MessageBox.Show("Введите корректное количество коров!");

                return false;
            }

            if (!isNumberInRange(recordCard.morning))
            {
                MessageBox.Show("Введите корректное количество надоя утром!");

                return false;
            }

            if (!isNumberInRange(recordCard.midday))
            {
                MessageBox.Show("Введите корректное количество надоя днём!");

                return false;
            }

            if (!isNumberInRange(recordCard.evening))
            {
                MessageBox.Show("Введите корректное количество надоя вечером!");

                return false;
            }

            if (!commonValidation.isDateValid(recordCard.date))
            {
                MessageBox.Show("Дата некорректна. Пожалуйста, убедитесь, что введенная дата находится в допустимом формате и соответствует текущей дате. Не позже чем сегодня и не раньше чем 10 дней с сегодняшнего дня!");

                return false;
            }

            return true;
        }

        private bool isNumberInRange(string number)
        {
            if (string.IsNullOrWhiteSpace(number))
            {
                return false;
            }

            if (!int.TryParse(number, out int parsedNumber))
            {
                return false;
            }

            if (parsedNumber < 1 || parsedNumber > Convert.ToInt32(number) * 30)
            {
                return false;
            }

            return true;
        }
    }
}