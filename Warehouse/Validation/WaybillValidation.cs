using System.Text.RegularExpressions;
using System.Windows;
using Warehouse.Entity;

namespace Warehouse.Validation
{
    internal class WaybillValidation
    {
        CommonValidation commonValidation;

        public WaybillValidation()
        {
            commonValidation = new CommonValidation();
        }

        private bool isCarNumber(string carNumber)
        {
            string pattern = @"^[а-яА-Я]{2,3}\d{4}$|^[а-яА-Я]{2}\s\d{3}\s\d{3}$";

            return Regex.IsMatch(carNumber, pattern);
        }

        public bool isWaybillValid(WaybillEntity waybill)
        {
            if (!commonValidation.isStringFieldValid(waybill.carOwner))
            {
                MessageBox.Show("Владелец авто должен содержать хотя бы одну букву, может содержать буквы, цифры и символы, но не должен состоять только из цифр. Длина не должна превышать 35 символов.");
                return false;
            }

            if (!commonValidation.isDateValid(waybill.date))
            {
                MessageBox.Show("Дата некорректна. Пожалуйста, убедитесь, что введенная дата находится в допустимом формате и соответствует текущей дате. Не позже чем сегодня и не раньше чем 10 дней с сегодняшнего дня!");

                return false;
            }

            if (!commonValidation.isStringFieldValid(waybill.vehicle))
            {
                MessageBox.Show("Авто должено содержать хотя бы одну букву, может содержать буквы, цифры и символы, но не должен состоять только из цифр. Длина не должна превышать 35 символов.");
                return false;
            }

            if (!commonValidation.isStringFieldValid(waybill.shipper))
            {
                MessageBox.Show("Грузоотправитель должен содержать хотя бы одну букву, может содержать буквы, цифры и символы, но не должен состоять только из цифр. Длина не должна превышать 35 символов.");
                return false;
            }

            if (!commonValidation.isStringFieldValid(waybill.consignor))
            {
                MessageBox.Show("Грузополучатель должен содержать хотя бы одну букву, может содержать буквы, цифры и символы, но не должен состоять только из цифр. Длина не должна превышать 35 символов.");
                return false;
            }

            if (!commonValidation.isStringFieldValid(waybill.loadingPoint))
            {
                MessageBox.Show("Место погрузки должно содержать хотя бы одну букву, может содержать буквы, цифры и символы, но не должен состоять только из цифр. Длина не должна превышать 35 символов.");
                return false;
            }

            if (!commonValidation.isStringFieldValid(waybill.unloadingPoint))
            {
                MessageBox.Show("Место разгрузки должно содержать хотя бы одну букву, может содержать буквы, цифры и символы, но не должен состоять только из цифр. Длина не должна превышать 35 символов.");
                return false;
            }

            if (!commonValidation.isStringFieldValid(waybill.treaty))
            {
                MessageBox.Show("Договор должен содержать хотя бы одну букву, может содержать буквы, цифры и символы, но не должен состоять только из цифр. Длина не должна превышать 35 символов.");
                return false;
            }

            if (!isCarNumber(waybill.vehicleNumber))
            {
                MessageBox.Show("Номерной знак автомобиля в Республике Беларусь должен соответствовать одному из следующих форматов:\n\n- Две или три буквы (русские или белорусские) и четыре цифры.\n- Две буквы (русские или белорусские), пробел, три цифры, пробел и ещё три цифры.");
                return false;
            }

            if (!commonValidation.isNumberInRange(waybill.guideListNumber, 200))
            {
                MessageBox.Show("Некорректный номер путеводителя! Номер путеводителя должен состоять только из цифр и быть от 1 до 200!");
                return false;
            }

            if (!commonValidation.isNumberInRange(waybill.routeNumber, 200))
            {
                MessageBox.Show("Некорректный номер маршрута! Номер маршрута должен состоять только из цифр и быть от 1 до 200!");
                return false;
            }

            return true;
        }
    }
}