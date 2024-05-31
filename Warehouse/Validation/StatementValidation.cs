using System;
using System.Windows;
using Warehouse.Entity;

namespace Warehouse.Validation
{
    internal class StatementValidation
    {
        CommonValidation commonValidation;

        public StatementValidation()
        {
            commonValidation = new CommonValidation();
        }

        public bool isStatementValid(StatementEntity statement)
        {
            if (!commonValidation.isNumberInRange(statement.supplyRate, 5, 20))
            {
                MessageBox.Show("Введите корректную норму отпуска!");
                return false;
            }

            if (!commonValidation.isNumberInRange(statement.animalQuantity, 10000))
            {
                MessageBox.Show("Введите корректное число животных!");
                return false;
            }

            if (!commonValidation.isNumberInRange(statement.actualRate, 10000))
            {
                MessageBox.Show("Введите корректное количество фактически отпущенного корма!");
                return false;
            }

            if (Convert.ToInt32(statement.actualRate) > Convert.ToInt32(statement.feedQuantity))
            {
                MessageBox.Show("Количество фактически отпущенного корма больше чем норма!");
            }

            if (Convert.ToInt32(statement.actualRate) < Convert.ToInt32(statement.feedQuantity))
            {
                MessageBox.Show("Количество фактически отпущенного корма меньше чем норма!");
            }

            if (!commonValidation.isDateValid(statement.date))
            {
                MessageBox.Show("Дата некорректна. Пожалуйста, убедитесь, что введенная дата находится в допустимом формате и соответствует текущей дате. Не позже чем сегодня и не раньше чем 10 дней с сегодняшнего дня!");

                return false;
            }

            return true;
        }
    }
}