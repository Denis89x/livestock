using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using Warehouse.storage1;
using Warehouse.View.Contractor;
using Warehouse.View.EditPage;

namespace Warehouse.View.Main
{
    public partial class MainPage : Window
    {
        private ContractorRepo contractorRepo;

        public MainPage()
        {
            InitializeComponent();

            contractorRepo = new ContractorRepoImpl();
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
    }
}