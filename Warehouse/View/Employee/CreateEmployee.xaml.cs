using System.Windows;
using System.Windows.Controls;
using Warehouse.Entity;
using Warehouse.Storage;
using Warehouse.Validation;

namespace Warehouse.View.Employee
{
    public partial class CreateEmployee : Window
    {
        private DataGrid dataGrid;

        private EmployeeValidation validation;
        private CrudRepo<EmployeeEntity> employeeCrud;

        public CreateEmployee(DataGrid dataGrid)
        {
            InitializeComponent();

            this.dataGrid = dataGrid;

            employeeCrud = new EmployeeRepoImpl();
            validation = new EmployeeValidation();
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            string surname = SurnameBox.Text;
            string firstName = FirstnameBox.Text;
            string patronymic = PatronymicBox.Text;
            string position = PositionCombo.Text;

            EmployeeEntity employee = new EmployeeEntity(surname, firstName, patronymic, position);

            if (validation.isEmployeeValid(employee))
            {
                employeeCrud.create(employee);
                employeeCrud.fetchToGrid(dataGrid);

                SurnameBox.Text = "";
                FirstnameBox.Text = "";
                PatronymicBox.Text = "";

                this.Close();
            }
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}