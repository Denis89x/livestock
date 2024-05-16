using System.Windows;
using System.Windows.Controls;
using Warehouse.storage1;
using Warehouse.Validation;

namespace Warehouse.View.Contractor
{
    public partial class CreateContractor : Window
    {
        ContractorValidation validation;
        DataGrid dataGrid;

        ContractorRepo contractorRepo;

        public CreateContractor(DataGrid dataGrid)
        {
            InitializeComponent();

            this.dataGrid = dataGrid;

            contractorRepo = new ContractorRepoImpl();
            validation = new ContractorValidation();
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            string address = AddressBox.Text;
            string title = TitleBox.Text;
            string settlement = SettlementBox.Text;
            string phone = PhoneBox.Text;
            string bank = BankBox.Text;

            ContractorEntity contractor = new ContractorEntity(address, title, settlement, phone, bank);

            if (validation.isContractorValid(contractor))
            {
                contractorRepo.createContractor(contractor);
                contractorRepo.fetchContractorToGrid(dataGrid);

                AddressBox.Text = "";
                TitleBox.Text = "";
                SettlementBox.Text = "";
                PhoneBox.Text = "";
                BankBox.Text = "";
            }
        }
    }
}
