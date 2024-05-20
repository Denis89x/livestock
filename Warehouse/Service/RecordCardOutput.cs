using Microsoft.Office.Interop.Word;
using System;
using System.Data.SqlClient;
using Warehouse.Storage;

namespace Warehouse.Service
{
    internal class RecordCardOutput
    {
        private Database database;
        private Application wordApplication;
        private Document document;

        private string templatePath = @"D:\Учеба\Диплом\warehouse-main\Warehouse\Resources\RecordCardTemplate.docx";

        public RecordCardOutput()
        {
            database = new Database();
            wordApplication = new Application();
            document = wordApplication.Documents.Open(templatePath);
        }

        public void generateWordDocument(long recordCardId, string firstDate, string secondDate)
        {
            string recordCardQuery = $"SELECT division.division_type as division, employee.surname as employee_surname, employee.first_name as employee_first_name, employee.patronymic as employee_patronymic FROM record_card, finished_product, division, employee WHERE record_card.employee_id = employee.employee_id AND record_card.division_id = division.division_id AND record_card.product_id = finished_product.product_id AND record_card.record_card_id = '{recordCardId}'";

            string recordCardCompositionQuery = $"SELECT record_card_composition.date as date, cow_quantity, milk_yield_morning as morning, milk_yield_midday as midday, milk_yield_evening as evening, (milk_yield_morning + milk_yield_midday + milk_yield_evening) AS summary FROM record_card_composition WHERE record_card_composition.record_card_id = '{recordCardId}'";

            string fileName = "Карточка учёта надоя молока №" + recordCardId;

            database.checkConnection();

            using (SqlCommand recordCardCommand = new SqlCommand(recordCardQuery, database.getSqlConnection()))
            {
                SqlDataReader reader = recordCardCommand.ExecuteReader();

                if (reader.Read())
                {
                    string division = reader["division"].ToString();
                    string surname = reader["employee_surname"].ToString();
                    string firstname = reader["employee_first_name"].ToString();
                    string patronymic = reader["employee_patronymic"].ToString();

                    replaceField("{division}", division);
                    replaceField("{employee_surname}", surname);
                    replaceField("{employee_firstname}", firstname);
                    replaceField("{employee_patronymic}", patronymic);
                }

                reader.Close();
            }

            replaceField("{first_date}", firstDate);
            replaceField("{second_date}", secondDate);

            using (SqlCommand recordCardCompositionCommand = new SqlCommand(recordCardCompositionQuery, database.getSqlConnection()))
            {
                SqlDataReader reader = recordCardCompositionCommand.ExecuteReader();

                int index = 1;

                while (reader.Read())
                {
                    string date = Convert.ToDateTime(reader["date"]).ToString("dd.MM.yyyy");
                    string cowQuantity = reader["cow_quantity"].ToString();
                    string morning = reader["morning"].ToString();
                    string midday = reader["midday"].ToString();
                    string evening = reader["evening"].ToString();
                    string summary = reader["summary"].ToString();

                    replaceField($"{{date_{index}}}", date);
                    replaceField($"{{cow_quantity_{index}}}", cowQuantity);
                    replaceField($"{{morning_{index}}}", morning);
                    replaceField($"{{midday_{index}}}", midday);
                    replaceField($"{{evening_{index}}}", evening);
                    replaceField($"{{summary_{index}}}", summary);

                    index++;
                }

                reader.Close();

                for (int i = index; i <= 18; i++)
                {
                    replaceField($"{{date_{i}}}", "");
                    replaceField($"{{cow_quantity_{i}}}", "");
                    replaceField($"{{morning_{i}}}", "");
                    replaceField($"{{midday_{i}}}", "");
                    replaceField($"{{evening_{i}}}", "");
                    replaceField($"{{summary_{i}}}", "");
                }
            }

            string outputPath = @"D:\Учеба\Диплом\warehouse-main\Warehouse\Resources\" + fileName + ".docx";

            document.SaveAs2(outputPath);
            document.Close();

            System.Diagnostics.Process.Start(outputPath);

            database.checkConnection();
        }

        private void replaceField(string field, string value)
        {
            document.Content.Find.ClearFormatting();
            document.Content.Find.Execute(FindText: field, ReplaceWith: value);
        }
    }
}