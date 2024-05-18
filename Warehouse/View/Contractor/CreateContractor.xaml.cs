using System.Windows;
using System.Windows.Controls;
using Warehouse.storage1;
using Warehouse.Validation;

namespace Warehouse.View.Contractor
{
    public partial class CreateContractor : Window
    {
        private ContractorValidation validation;
        private DataGrid dataGrid;

        private CrudRepo<ContractorEntity> contractorCrud;

        public CreateContractor(DataGrid dataGrid)
        {
            InitializeComponent();

            this.dataGrid = dataGrid;

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
            string settlement = SettlementBox.Text;
            string phone = PhoneBox.Text;
            string bank = BankBox.Text;

            ContractorEntity contractor = new ContractorEntity(address, title, settlement, phone, bank);

            if (validation.isContractorValid(contractor))
            {
                contractorCrud.create(contractor);
                contractorCrud.fetchToGrid(dataGrid);

                AddressBox.Text = "";
                TitleBox.Text = "";
                SettlementBox.Text = "";
                PhoneBox.Text = "";
                BankBox.Text = "";
            }
        }
    }
}