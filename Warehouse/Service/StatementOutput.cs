using Microsoft.Office.Interop.Word;
using System;
using System.Data.SqlClient;
using Warehouse.Storage;

namespace Warehouse.Service
{
    internal class StatementOutput
    {
        private Database database;
        private Application wordApplication;
        private Document document;

        private string templatePath = @"D:\Учеба\Диплом\warehouse-main\Warehouse\Resources\VedomostTemplate.docx";

        public StatementOutput()
        {
            database = new Database();
            wordApplication = new Application();
            document = wordApplication.Documents.Open(templatePath);
        }

        public void generateWordDocument(long statementId)
        {
            string query = $"SELECT division.division_type, cattle.cattle_type, employee.surname + ' ' + employee.first_name + ' ' + employee.patronymic as fio, finished_product.title as product_title, date, foundation, statement_number, statement.title, statement.unit, supply_rate, animal_quantity, feed_quantity FROM statement, finished_product, division, cattle, employee WHERE statement.product_id = finished_product.product_id AND statement.division_id = division.division_id AND statement.cattle_id = cattle.cattle_id AND statement.employee_id = employee.employee_id AND statement_id = '{statementId}'";

            database.checkConnection();

            string fileName = "Ведомость учёта надоя молока";

            using (SqlCommand statementCommand = new SqlCommand(query, database.getSqlConnection()))
            {
                SqlDataReader reader = statementCommand.ExecuteReader();

                if (reader.Read())
                {
                    string division = reader["division_type"].ToString();
                    string cattle = reader["cattle_type"].ToString();
                    string fio = reader["fio"].ToString();
                    string productTitle = reader["product_title"].ToString();
                    string foundation = reader["foundation"].ToString();
                    string statementNumber = reader["statement_number"].ToString();
                    string title = reader["title"].ToString();
                    string unit = reader["unit"].ToString();
                    string supplyRate = reader["supply_rate"].ToString();
                    string animalQuantity = reader["animal_quantity"].ToString();
                    string feedQuantity = reader["feed_quantity"].ToString();

                    DateTime date = Convert.ToDateTime(reader["date"]);
                    int day = date.Day;
                    int month = date.Month;

                    string monthName = date.ToString("MMMM", System.Globalization.CultureInfo.CurrentCulture);

                    replaceField("{division}", division);
                    replaceField("{number}", statementNumber);
                    replaceField("{assignment}", foundation);
                    replaceField("{cattle}", cattle);
                    replaceField("{fio}", fio);
                    replaceField("{product}", productTitle);
                    replaceField("{feed_title}", title);
                    replaceField("{unit}", unit);
                    replaceField("{supply_rate}", supplyRate);
                    replaceField("{animal_quantity}", animalQuantity);
                    replaceField("{feed_quantity}", feedQuantity);
                    replaceField("{day}", day.ToString());
                    replaceField("{month}", monthName);
                }

                reader.Close();
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