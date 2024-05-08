using System.Windows.Controls;

namespace Warehouse.storage1
{
    internal class ContractorRepoImpl : ContractorRepo
    {
        private Database database;

        public ContractorRepoImpl()
        {
            this.database = new Database();
        }

        public void createContractor(ContractorEntity contractor)
        {
            string query = $"INSERT INTO contractor(address, title, settlement_account, phone_number, bank_name) VALUES(N'{contractor.address}', N'{contractor.title}', N'{contractor.settlementAccount}', N'{contractor.phoneNumber}', N'{contractor.bankName}')";
        
            database.executeQuery(query);
        }

        public void deleteContractor(long contractorId)
        {
            string query = $"DELETE FROM contractor WHERE contractor_id = '{contractorId}'";

            database.executeQuery(query);
        }

        public void fetchContractorToGrid(DataGrid dataGrid)
        {
            string query = $"SELECT * FROM contractor";

            database.selectQuery(query, dataGrid);
        }

        public void updateContractor(ContractorEntity contractor)
        {
            string query = $"UPDATE contractor SET address = N'{contractor.address}', title = N'{contractor.title}', settlement_account = N'{contractor.settlementAccount}', phone_number = N'{contractor.phoneNumber}', bank_name = N'{contractor.bankName}' where contractor_id = '{contractor.contractorId}'";

            database.executeQuery(query);
        }
    }
}
