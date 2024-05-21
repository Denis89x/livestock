using System.Windows;
using System.Windows.Controls;
using Warehouse.Entity;
using Warehouse.Storage;
using Warehouse.Validation;

namespace Warehouse.View.Employee
{
    public partial class EditEmployee : Window
    {
        private long employeeId;
        private DataGrid dataGrid;
        private CrudRepo<EmployeeEntity> employeeCrud;
        private EmployeeValidation validation;

        public EditEmployee(long employeeId, string surname, string firstName, string patronymic, string position, DataGrid dataGrid)
        {
            InitializeComponent();

            this.employeeId = employeeId; 
            this.dataGrid = dataGrid;

            SurnameBox.Text = surname;
            FirstnameBox.Text = firstName;
            PatronymicBox.Text = patronymic;
            PositionCombo.Text = position;

            employeeCrud = new EmployeeRepoImpl();
            validation = new EmployeeValidation();
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
            string position = PositionCombo.Text;

            EmployeeEntity employee = new EmployeeEntity(employeeId, surname, firstName, patronymic, position);

            if (validation.isEmployeeValid(employee))
            {
                employeeCrud.update(employee);
                employeeCrud.fetchToGrid(dataGrid);

                this.Close();
            }
        }
    }
}