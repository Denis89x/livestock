using System;
using System.Windows.Controls;
using Warehouse.Entity;

namespace Warehouse.Storage
{
    internal class EmployeeRepoImpl : CrudRepo<EmployeeEntity>
    {
        Database database;

        public EmployeeRepoImpl()
        {
            this.database = new Database();
        }

        public void create(EmployeeEntity entity)
        {
            string query = $"INSERT INTO employee(surname, first_name, patronymic, position) VALUES(N'{entity.surname}', N'{entity.firstName}', N'{entity.patronymic}', N'{entity.position}')";

            database.executeQuery(query);
        }

        public void delete(long entityId)
        {
            string query = $"DELETE FROM employee WHERE employee_id = '{entityId}'";

            database.executeQuery(query);
        }

        public void fetchToGrid(DataGrid dataGrid)
        {
            string query = $"SELECT * FROM employee";

            database.selectQuery(query, dataGrid);
        }

        public void update(EmployeeEntity entity)
        {
            string query = $"UPDATE employee SET surname = N'{entity.surname}', first_name = N'{entity.firstName}', patronymic = N'{entity.patronymic}', position = N'{entity.position}'";

            database.executeQuery(query);
        }
    }
}