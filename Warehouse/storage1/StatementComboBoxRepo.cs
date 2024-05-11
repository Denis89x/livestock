using System.Windows.Controls;

namespace Warehouse.storage1
{
    internal interface StatementComboBoxRepo
    {
        void insertDivisionsIntoComboBox(ComboBox box);

        void insertCattleIntoComboBox(ComboBox box);
        
        void insertEmployeesIntoComboBox(ComboBox box);

        void insertProductsIntoComboBox(ComboBox box);
    }
}
