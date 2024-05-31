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
            if (!commonValidation.isDoubleNumberInRange(waybillCompositionEntity.fat, 3.2, 5.4))
            {
                MessageBox.Show("Введите корректную жирность! (3,2% - 5,4%)");
                return false;
            }

            if (!commonValidation.isDoubleNumberInRange(waybillCompositionEntity.mass, 1, 10000))
            {
                MessageBox.Show("Введите корректную массу!");
                return false;
            }

            if (!commonValidation.isDoubleNumberInRange(waybillCompositionEntity.acidity, 1, 100))
            {
                MessageBox.Show("Введите корректную кислотность!");
                return false;
            }

            if (!commonValidation.isDoubleNumberInRange(waybillCompositionEntity.temperature, 10, 41))
            {
                MessageBox.Show("Введите корректную температуру!");
                return false;
            }

            if (!commonValidation.isDoubleNumberInRange(waybillCompositionEntity.density, 1, 100))
            {
                MessageBox.Show("Введите корректную плотность!");
                return false;
            }

            if (!commonValidation.isDoubleNumberInRange(waybillCompositionEntity.brutto, 1, 100))
            {
                MessageBox.Show("Введите корректное брутто!");
                return false;
            }

            if (waybillCompositionEntity.tara == null || waybillCompositionEntity.tara == "" || waybillCompositionEntity.tara == " ")
            {
                MessageBox.Show("Выберите тару!");
                return false;
            }

            if (!commonValidation.isDoubleNumberInRange(waybillCompositionEntity.netto, 1, 100))
            {
                MessageBox.Show("Введите корректное нетто!");
                return false;
            }

            return true;
        }
    }
}