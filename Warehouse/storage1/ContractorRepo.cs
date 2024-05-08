using System.Windows.Controls;

namespace Warehouse.storage1
{
    internal interface ContractorRepo
    {
        void fetchContractorToGrid(DataGrid dataGrid);

        void createContractor(ContractorEntity contractor);

        void updateContractor(ContractorEntity contractor);

        void deleteContractor(long contractorId);
    }
}
