using System.Windows;
using System.Windows.Controls;
using Warehouse.Entity;
using Warehouse.storage1;
using Warehouse.Validation;

namespace Warehouse.View.Employee
{
    public partial class CreateEmployee : Window
    {
        DataGrid dataGrid;

        EmployeeValidation validation;
        EmployeeRepo employeeRepo;

        public CreateEmployee(DataGrid dataGrid)
        {
            InitializeComponent();

            this.dataGrid = dataGrid;
            
            employeeRepo = new EmployeeRepoImpl();
            validation = new EmployeeValidation();
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            string surname = SurnameBox.Text;
            string firstName = FirstnameBox.Text;
            string patronymic = PatronymicBox.Text;
            string position = PositionBox.Text;

            EmployeeEntity employee = new EmployeeEntity(surname, firstName, patronymic, position);

            if (validation.isEmployeeValid(employee))
            {
                employeeRepo.createEmployee(employee);
                employeeRepo.fetchEmployeeToGrid(dataGrid);

                SurnameBox.Text = "";
                FirstnameBox.Text = "";
                PatronymicBox.Text = "";
                PositionBox.Text = "";
            }
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}