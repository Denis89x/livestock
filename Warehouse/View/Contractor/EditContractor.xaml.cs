using System.Windows;
using System.Windows.Controls;
using Warehouse.storage1;
using Warehouse.Validation;

namespace Warehouse.View.Contractor
{
    public partial class EditContractor : Window
    {
        private long contractorId;
        private DataGrid dataGrid;
        private CrudRepo<ContractorEntity> contractorCrud;
        private ContractorValidation validation;

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

            contractorCrud = new ContractorRepoImpl();
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
            string settlementAccount = SettlementBox.Text;
            string phoneNumber = PhoneBox.Text;
            string bankName = BankBox.Text;

            ContractorEntity contractor = new ContractorEntity(contractorId, address, title, settlementAccount, phoneNumber, bankName);

            if (validation.isContractorValid(contractor))
            {
                contractorCrud.update(contractor);
                contractorCrud.fetchToGrid(dataGrid);

                this.Close();
            }
        }
    }
}
