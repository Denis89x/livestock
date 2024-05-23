using System.Windows;
using Warehouse.Entity;

namespace Warehouse.Validation
{
    internal class WaybillCompositionValidation
    {
        CommonValidation commonValidation;

        public WaybillCompositionValidation()
        {
            commonValidation = new CommonValidation();
        }

        public bool isWaybillCompositionValid(WaybillCompositionEntity waybillCompositionEntity)
        {
            if (!commonValidation.isNumberInRange(waybillCompositionEntity.fat, 100))
            {
                MessageBox.Show("Жирность должна состоять только из цифр и быть от 1 до 100!");
                return false;
            }

            if (!commonValidation.isNumberInRange(waybillCompositionEntity.mass, 10000))
            {
                MessageBox.Show("Масса должна состоять только из цифр и быть от 1 до 10000!");
                return false;
            }

            if (!commonValidation.isNumberInRange(waybillCompositionEntity.acidity, 100))
            {
                MessageBox.Show("Кислотность должна состоять только из цифр и быть от 1 до 100!");
                return false;
            }

            if (!commonValidation.isNumberInRange(waybillCompositionEntity.temperature, 41))
            {
                MessageBox.Show("Температура должна состоять только из цифр и быть от 1 до 41!");
                return false;
            }

            if (!commonValidation.isNumberInRange(waybillCompositionEntity.density, 100))
            {
                MessageBox.Show("Плотность должна состоять только из цифр и быть от 1 до 100!");
                return false;
            }

            if (!commonValidation.isNumberInRange(waybillCompositionEntity.brutto, 100))
            {
                MessageBox.Show("Брутто должно состоять только из цифр и быть от 1 до 100!");
                return false;
            }

            if (!commonValidation.isNumberInRange(waybillCompositionEntity.tara, 100))
            {
                MessageBox.Show("Тара должна состоять только из цифр и быть от 1 до 100!");
                return false;
            }

            if (!commonValidation.isNumberInRange(waybillCompositionEntity.netto, 100))
            {
                MessageBox.Show("Нетто должно состоять только из цифр и быть от 1 до 100!");
                return false;
            }

            return true;
        }
    }
}