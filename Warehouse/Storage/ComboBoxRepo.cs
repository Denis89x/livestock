using System.Windows.Controls;

namespace Warehouse.Storage
{
    internal interface ComboBoxRepo
    {
        void insertDivisionsIntoComboBox(ComboBox box);

        void insertCattleIntoComboBox(ComboBox box);

        void insertEmployeesIntoComboBox(ComboBox box);

        void insertProductsIntoComboBox(ComboBox box);

        void insertContractorIntoComboBox(ComboBox box);

        void insertWaybillIntoComboBox(ComboBox box);

        void insertDeliveryIntoComboBox(ComboBox box);

        void insertRecordCardIntoComboBox(ComboBox box);
    }
}
