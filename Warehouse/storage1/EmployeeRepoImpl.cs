using System;
using System.Windows.Controls;
using Warehouse.Entity;

namespace Warehouse.storage1
{
    internal class EmployeeRepoImpl : EmployeeRepo
    {
        Database database;

        public EmployeeRepoImpl()
        {
            this.database = new Database();
        }

        public void createEmployee(EmployeeEntity employee)
        {
            string query = $"INSERT INTO employee(surname, first_name, patronymic, position) VALUES(N'{employee.surname}', N'{employee.firstName}', N'{employee.patronymic}', N'{employee.position}')";
        
            database.executeQuery(query);
        }

        public void deleteEmployee(long employeeId)
        {
            string query = $"DELETE FROM employee WHERE employee_id = '{employeeId}'";

            database.executeQuery(query);
        }

        public void fetchEmployeeToGrid(DataGrid dataGrid)
        {
            string query = $"SELECT * FROM employee";

            database.selectQuery(query, dataGrid);
        }

        public void updateEmployee(EmployeeEntity employee)
        {
            string query = $"UPDATE employee SET surname = N'{employee.surname}', first_name = N'{employee.firstName}', patronymic = N'{employee.patronymic}', position = N'{employee.position}'";

            database.executeQuery(query);
        }
    }
}
