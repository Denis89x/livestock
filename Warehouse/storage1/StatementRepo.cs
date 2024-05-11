using System.Windows.Controls;
using Warehouse.Entity;

namespace Warehouse.storage1
{
    internal interface StatementRepo
    {
        void fetchStatementToGrid(DataGrid dataGrid);

        void createStatement(StatementEntity statement);

        void updateStatement(StatementEntity statement);

        void deleteStatement(long statementId);
    }
}
