using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using Warehouse.storage1;
using Warehouse.View.Contractor;
using Warehouse.View.Division;
using Warehouse.View.Employee;

namespace Warehouse.View.Main
{
    public partial class MainPage : Window
    {
        private DivisionRepo divisionRepo;
        private EmployeeRepo employeeRepo;
        private ContractorRepo contractorRepo;

        public MainPage()
        {
            InitializeComponent();

            contractorRepo = new ContractorRepoImpl();
            employeeRepo = new EmployeeRepoImpl();
            divisionRepo = new DivisionRepoImpl();
        }

        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
            ButtonCloseMenu.Visibility = Visibility.Visible;
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Visible;
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
        }


        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Contractor_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            contractorRepo.fetchContractorToGrid(ContractorGrid);

            ContractorGrid.Visibility = Visibility.Visible;

            EmployeeGrid.Visibility = Visibility.Collapsed;
            DivisionGrid.Visibility = Visibility.Collapsed;
        }

        private void Employee_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            employeeRepo.fetchEmployeeToGrid(EmployeeGrid);

            EmployeeGrid.Visibility = Visibility.Visible;

            ContractorGrid.Visibility = Visibility.Collapsed;
            DivisionGrid.Visibility = Visibility.Collapsed;
        }


        private void Division_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            divisionRepo.fetchDivisionToGrid(DivisionGrid);

            DivisionGrid.Visibility = Visibility.Visible;

            ContractorGrid.Visibility = Visibility.Collapsed;
            EmployeeGrid.Visibility = Visibility.Collapsed;
        }

        private void AddContractor_Click(object sender, RoutedEventArgs e)
        {
            CreateContractor contractor = new CreateContractor(ContractorGrid);
            contractor.ShowDialog();
        }

        private void EditContractor_Click(object sender, RoutedEventArgs e)
        {
            var selectedRow = ContractorGrid.SelectedItem as DataRowView;

            if (selectedRow != null)
            {
                EditContractor contractor = new EditContractor(Convert.ToInt64(selectedRow.Row.ItemArray[0]), Convert.ToString(selectedRow.Row.ItemArray[1]), Convert.ToString(selectedRow.Row.ItemArray[2]), Convert.ToString(selectedRow.Row.ItemArray[3]), Convert.ToString(selectedRow.Row.ItemArray[4]), Convert.ToString(selectedRow.Row.ItemArray[5]), ContractorGrid);
                contractor.ShowDialog();
            }
            else
            {
                MessageBox.Show("Не выбрана строка для редактирования", "Ошибка", MessageBoxButton.OK);
            }
        }

        private void DeleteContractor_Click(object sender, RoutedEventArgs e)
        {
            var selectedRow = ContractorGrid.SelectedItem as DataRowView;

            if (selectedRow != null)
            {
                try
                {
                    long contractorId = Convert.ToInt64(selectedRow.Row.ItemArray[0]);

                    contractorRepo.deleteContractor(contractorId);
                    contractorRepo.fetchContractorToGrid(ContractorGrid);
                }
                catch (SqlException)
                {
                    MessageBox.Show("Удаление невозможно. Удалите связанные данные с этим контрагентом!");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Выберите поле для удаления!");
            }
        }

        private void AddEmployee_Click(object sender, RoutedEventArgs e)
        {
            CreateEmployee employee = new CreateEmployee(EmployeeGrid);
            employee.ShowDialog();
        }

        private void EditEmployee_Click(object sender, RoutedEventArgs e)
        {
            var selectedRow = EmployeeGrid.SelectedItem as DataRowView;

            if (selectedRow != null)
            {
                EditEmployee employee = new EditEmployee(Convert.ToInt64(selectedRow.Row.ItemArray[0]), Convert.ToString(selectedRow.Row.ItemArray[1]), Convert.ToString(selectedRow.Row.ItemArray[2]), Convert.ToString(selectedRow.Row.ItemArray[3]), Convert.ToString(selectedRow.Row.ItemArray[4]), EmployeeGrid);
                employee.ShowDialog();
            }
            else
            {
                MessageBox.Show("Не выбрана строка для редактирования", "Ошибка", MessageBoxButton.OK);
            }
        }

        private void DeleteEmployee_Click(object sender, RoutedEventArgs e)
        {
            var selectedRow = EmployeeGrid.SelectedItem as DataRowView;

            if (selectedRow != null)
            {
                try
                {
                    long employeeId = Convert.ToInt64(selectedRow.Row.ItemArray[0]);

                    employeeRepo.deleteEmployee(employeeId);
                    employeeRepo.fetchEmployeeToGrid(EmployeeGrid);
                }
                catch (SqlException)
                {
                    MessageBox.Show("Удаление невозможно. Удалите связанные данные с этим сотрудником!");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Выберите поле для удаления!");
            }
        }

        private void AddDivision_Click(object sender, RoutedEventArgs e)
        {
            AddDivision division = new AddDivision(DivisionGrid);
            division.ShowDialog();
        }

        private void EditDivision_Click(object sender, RoutedEventArgs e)
        {
            var selectedRow = DivisionGrid.SelectedItem as DataRowView;

            if (selectedRow != null)
            {
                EditDivision division = new EditDivision(Convert.ToInt64(selectedRow.Row.ItemArray[0]), Convert.ToString(selectedRow.Row.ItemArray[1]), DivisionGrid);
                division.ShowDialog();
            }
            else
            {
                MessageBox.Show("Не выбрана строка для редактирования", "Ошибка", MessageBoxButton.OK);
            }
        }

        private void DeleteDivision_Click(object sender, RoutedEventArgs e)
        {
            var selectedRow = DivisionGrid.SelectedItem as DataRowView;

            if (selectedRow != null)
            {
                try
                {
                    long divisionId = Convert.ToInt64(selectedRow.Row.ItemArray[0]);

                    divisionRepo.deleteDivision(divisionId);
                    divisionRepo.fetchDivisionToGrid(DivisionGrid);
                }
                catch (SqlException)
                {
                    MessageBox.Show("Удаление невозможно. Удалите связанные данные с этим подразделением!");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Выберите поле для удаления!");
            }
        }
    }
}