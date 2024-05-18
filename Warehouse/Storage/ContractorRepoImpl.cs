using System.Windows.Controls;

namespace Warehouse.Storage
{
    internal class ContractorRepoImpl : CrudRepo<ContractorEntity>
    {
        private Database database;

        public ContractorRepoImpl()
        {
            this.database = new Database();
        }

        public void create(ContractorEntity entity)
        {
            string query = $"INSERT INTO contractor(address, title, settlement_account, phone_number, bank_name) VALUES(N'{entity.address}', N'{entity.title}', N'{entity.settlementAccount}', N'{entity.phoneNumber}', N'{entity.bankName}')";
            
            database.executeQuery(query);
        }

        public void delete(long entityId)
        {
            string query = $"DELETE FROM contractor WHERE contractor_id = '{entityId}'";
            database.executeQuery(query);
        }

        
        public void fetchToGrid(DataGrid dataGrid)
        {
            string query = $"SELECT * FROM contractor";

            database.selectQuery(query, dataGrid);
        }
        public void update(ContractorEntity entity)
        {
            string query = $"UPDATE contractor SET address = N'{entity.address}', title = N'{entity.title}', settlement_account = N'{entity.settlementAccount}', phone_number = N'{entity.phoneNumber}', bank_name = N'{entity.bankName}' where contractor_id = '{entity.contractorId}'";

            database.executeQuery(query);
        }
    }
}