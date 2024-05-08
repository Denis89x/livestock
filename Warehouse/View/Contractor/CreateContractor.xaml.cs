using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Warehouse.storage1;

namespace Warehouse.View.Contractor
{
    public partial class CreateContractor : Window
    {
        DataGrid dataGrid;

        ContractorRepo contractorRepo;

        public CreateContractor(DataGrid dataGrid)
        {
            InitializeComponent();

            this.dataGrid = dataGrid;
            this.contractorRepo = new ContractorRepoImpl();
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
