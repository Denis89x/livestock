using Microsoft.Office.Interop.Word;
using System;
using System.Data.SqlClient;
using System.Windows;
using Warehouse.Storage;
using Application = Microsoft.Office.Interop.Word.Application;

namespace Warehouse.Service
{
    internal class DeliveryNoteOutput
    {
        private Database database;
        private Application wordApplication;
        private Document document;

        private string templatePath = @"D:\Учеба\Диплом\warehouse-main\Warehouse\Resources\DeliveryNoteTemplate.docx";

        public DeliveryNoteOutput()
        {
            database = new Database();
            wordApplication = new Application();
            document = wordApplication.Documents.Open(templatePath);
        }

        public void generateWordDocument(long deliveryId)
        {
            try
            {
                string deliveryNoteQuery = $"SELECT delivery_note.delivery_note_id, division.division_type, date, assignment, broker FROM delivery_note, division WHERE delivery_note.division_id = division.division_id AND delivery_note_id = '{deliveryId}'";

                string deliveryNoteCompositionQuery = $"SELECT finished_product.title, finished_product.unit, requested, released, price, amount FROM delivery_note_composition, delivery_note, finished_product WHERE delivery_note_composition.delivery_note_id = delivery_note.delivery_note_id AND delivery_note_composition.product_id = finished_product.product_id AND delivery_note_composition.delivery_note_id = '{deliveryId}'";

                string fileName = "Требования накладная";

                database = new Database();
                using (SqlConnection connection = database.getSqlConnection())
                {
                    database = new Database();
                    connection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand(deliveryNoteQuery, connection))
                    {
                        database = new Database();
                        SqlDataReader reader = sqlCommand.ExecuteReader();

                        if (reader.Read())
                        {
                            string id = reader["delivery_note_id"].ToString();
                            string division = reader["division_type"].ToString();
                            string assignment = reader["assignment"].ToString();
                            string broker = reader["broker"].ToString();

                            DateTime date = Convert.ToDateTime(reader["date"]);

                            string day = date.Day.ToString();
                            int month = date.Month;
                            string year = date.Year.ToString();

                            string monthName = date.ToString("MMMM", System.Globalization.CultureInfo.CurrentCulture);

                            replaceField("{division}", division);
                            replaceField("{day}", day);
                            replaceField("{month}", monthName);
                            replaceField("{year}", year);
                            replaceField("{assignment}", assignment);
                            replaceField("{broker}", broker);
                        }

                        reader.Close();
                    }

                    replaceField("{id}", deliveryId.ToString());

                    using (SqlCommand sqlCommand = new SqlCommand(deliveryNoteCompositionQuery, connection))
                    {
                        SqlDataReader reader = sqlCommand.ExecuteReader();

                        int index = 1;
                        int amountSum = 0;

                        while (reader.Read())
                        {
                            string title = reader["title"].ToString();
                            string unit = reader["unit"].ToString();
                            string requested = reader["requested"].ToString();
                            string released = reader["released"].ToString();
                            string price = reader["price"].ToString();
                            string amount = reader["amount"].ToString();

                            replaceField($"{{title_{index}}}", title);
                            replaceField($"{{unit_{index}}}", unit);
                            replaceField($"{{req_{index}}}", requested);
                            replaceField($"{{rel_{index}}}", released);
                            replaceField($"{{price_{index}}}", price);
                            replaceField($"{{amount_{index}}}", amount);

                            amountSum += Convert.ToInt32(amount);

                            index++;
                        }

                        reader.Close();

                        for (int i = index; i <= 9; i++)
                        {
                            replaceField($"{{title_{i}}}", "");
                            replaceField($"{{unit_{i}}}", "");
                            replaceField($"{{req_{i}}}", "");
                            replaceField($"{{rel_{i}}}", "");
                            replaceField($"{{price_{i}}}", "");
                            replaceField($"{{amount_{i}}}", "");
                        }

                        replaceField("{amount_sum}", amountSum.ToString());
                    }

                    connection.Close();
                }  

                string outputPath = @"D:\Учеба\Диплом\warehouse-main\Warehouse\Resources\" + fileName + ".docx";

                document.SaveAs2(outputPath);
                document.Close();

                System.Diagnostics.Process.Start(outputPath);

                database.checkConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка. Закройте все экземпляры Microsoft Word и попробуйте снова!" + ex);

                foreach (var process in System.Diagnostics.Process.GetProcessesByName("WINWORD"))
                {
                    process.Kill();
                }

                return;
            }
        }

        private void replaceField(string field, string value)
        {
            document.Content.Find.ClearFormatting();
            document.Content.Find.Execute(FindText: field, ReplaceWith: value);
        }
    }
}