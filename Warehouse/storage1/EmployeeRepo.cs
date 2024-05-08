using System.Windows.Controls;
using Warehouse.Entity;

namespace Warehouse.storage1
{
    internal interface EmployeeRepo
    {
        void fetchEmployeeToGrid(DataGrid dataGrid);

        void createEmployee(EmployeeEntity employee);

        void updateEmployee(EmployeeEntity employee);

        void deleteEmployee(long employeeId);
    }
}
