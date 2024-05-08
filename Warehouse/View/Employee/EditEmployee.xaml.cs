using System.Windows;
using System.Windows.Controls;
using Warehouse.Entity;
using Warehouse.storage1;

namespace Warehouse.View.Employee
{
    public partial class EditEmployee : Window
    {
        long employeeId;
        DataGrid dataGrid;
        EmployeeRepo employeeRepo;

        public EditEmployee(long employeeId, string surname, string firstName, string patronymic, string position, DataGrid dataGrid)
        {
            InitializeComponent();

            this.employeeId = employeeId; 
            this.dataGrid = dataGrid;

            SurnameBox.Text = surname;
            FirstnameBox.Text = firstName;
            PatronymicBox.Text = patronymic;
            PositionBox.Text = position;

            employeeRepo = new EmployeeRepoImpl();
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            string surname = SurnameBox.Text;
            string firstName = FirstnameBox.Text;
            string patronymic = PatronymicBox.Text;
            string position = PositionBox.Text;

            EmployeeEntity employee = new EmployeeEntity(employeeId, surname, firstName, patronymic, position);

            employeeRepo.updateEmployee(employee);
            employeeRepo.fetchEmployeeToGrid(dataGrid);

            this.Close();
        }
    }
}
