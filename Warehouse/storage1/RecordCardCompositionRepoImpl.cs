using System.Windows.Controls;
using Warehouse.Entity;

namespace Warehouse.storage1
{
    internal class RecordCardCompositionRepoImpl : CrudRepo<RecordCardCompositionEntity>
    {
        private Database database;

        public RecordCardCompositionRepoImpl()
        {
            this.database = new Database();
        }

        public void create(RecordCardCompositionEntity entity)
        {
            string query = $"INSERT INTO record_card_composition(record_card_id, date, cow_quantity, milk_yield_morning, milk_yield_midday, milk_yield_evening) VALUES('{entity.recordCardId}', '{entity.date}', '{entity.cowQuantity}', '{entity.milkMorning}', '{entity.milkMidday}', '{entity.milkEvening}')";
        
            database.executeQuery(query);
        }

        public void delete(long entityId)
        {
            string query = $"DELETE FROM record_card_composition WHERE record_card_composition_id = '{entityId}'";

            database.executeQuery(query);
        }

        public void fetchToGrid(DataGrid dataGrid)
        {
            string query = "SELECT * FROM record_card_composition";

            database.selectQuery(query, dataGrid);
        }

        public void update(RecordCardCompositionEntity entity)
        {
            string query = $"UPDATE record_card_composition SET record_card_id = '{entity.recordCardId}', date = '{entity.date}', cow_quantity = '{entity.cowQuantity}', milk_yield_morning = '{entity.milkMorning}', milk_yield_midday = '{entity.milkMidday}', milk_yield_evening = '{entity.milkEvening}' WHERE record_card_composition_id = '{entity.recordCardCompositionId}'";

            database.executeQuery(query);
        }
    }
}