using System.Windows;
using System.Windows.Controls;
using Warehouse.storage1;

namespace Warehouse.View.Contractor
{
    public partial class EditContractor : Window
    {
        long contractorId;
        DataGrid dataGrid;
        ContractorRepo contractorRepo;

        public EditContractor(long contractorId, string address, string title, string settlementAccount, string phoneNumber, string bankName, DataGrid dataGrid)
        {
            InitializeComponent();

            this.dataGrid = dataGrid;
            this.contractorId = contractorId;

            AddressBox.Text = address;
            TitleBox.Text = title;
            SettlementBox.Text = settlementAccount;
            PhoneBox.Text = phoneNumber;
            BankBox.Text = bankName;

            contractorRepo = new ContractorRepoImpl();
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            string address = AddressBox.Text;
            string title = TitleBox.Text;
            string settlementAccount = SettlementBox.Text;
            string phoneNumber = PhoneBox.Text;
            string bankName = BankBox.Text;

            ContractorEntity contractor = new ContractorEntity(contractorId, address, title, settlementAccount, phoneNumber, bankName);

            contractorRepo.updateContractor(contractor);
            contractorRepo.fetchContractorToGrid(dataGrid);

            this.Close();
        }
    }
}
