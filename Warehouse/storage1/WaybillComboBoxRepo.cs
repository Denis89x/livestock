using System.Windows.Controls;

namespace Warehouse.storage1
{
    internal interface WaybillComboBoxRepo
    {
        void insertContractorIntoComboBox(ComboBox box);

        void insertEmployeeIntoComboBox(ComboBox box);
    }
}
