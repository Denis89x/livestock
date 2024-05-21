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
            if (!commonValidation.isNumberInRange(statement.supplyRate, 100))
            {
                MessageBox.Show("Норма отпуска должна быть от 1 до 100");
                return false;
            }

            if (!commonValidation.isNumberInRange(statement.animalQuantity, 1000))
            {
                MessageBox.Show("Количество животных должно быть от 1 до 1000");
                return false;
            }

            if (!commonValidation.isNumberInRange(statement.actualRate, 10000))
            {
                MessageBox.Show("Количество фактически отпущенного корма должно быть от 1 до 10000");
                return false;
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