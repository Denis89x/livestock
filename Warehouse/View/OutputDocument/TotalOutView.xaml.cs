using Microsoft.Office.Interop.Word;
using System;
using System.Data.SqlClient;
using System.Windows;
using Warehouse.Storage;
using Application = Microsoft.Office.Interop.Word.Application;

namespace Warehouse.View.OutputDocument
{
    public partial class TotalOutView : System.Windows.Window
    {
        private Database database;

        private Application wordApplication;
        private Document document;

        private string templatePath = @"D:\Учеба\Диплом\warehouse-main\Warehouse\Resources\TotalTemplate.docx";

        public TotalOutView()
        {
            InitializeComponent();

            database = new Database();

            FirstDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
            SecondDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
        }

        private void generateWordDocument()
        {
            try
            {
                wordApplication = new Application();
                document = wordApplication.Documents.Open(templatePath);

                string firstDate = "";
                string secondDate = "";

                try
                {
                    firstDate = FirstDate.SelectedDate.Value.ToString("yyyy-MM-dd");
                    secondDate = SecondDate.SelectedDate.Value.ToString("yyyy-MM-dd");

                    replaceField("{f_date}", firstDate);
                    replaceField("{s_date}", secondDate);
                }
                catch (InvalidOperationException)
                {
                    MessageBox.Show("Выберите дату!");
                    return;
                }

                database = new Database();

                string waybillQuery = $"SELECT waybill_composition.quantity FROM waybill_composition, waybill WHERE waybill.waybill_id = waybill_composition.waybill_id AND waybill_type = 'posted' AND date BETWEEN '{firstDate}' AND '{secondDate}'";

                using (SqlConnection connection = database.getSqlConnection())
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(waybillQuery, connection))
                    {
                        SqlDataReader reader = command.ExecuteReader();

                        int index = 1;
                        int totalQuantity = 0;

                        while (reader.Read())
                        {
                            string quantity = reader["quantity"].ToString();

                            replaceField($"{{q_{index}}}", quantity);

                            totalQuantity += Convert.ToInt32(quantity);

                            index++;
                        }

                        reader.Close();

                        for (int i = index; i <= 17; i++)
                        {
                            replaceField($"{{q_{i}}}", "");
                        }

                        replaceField("{q_sum}", totalQuantity.ToString());

                        connection.Close();
                    }
                }

                database = new Database();

                string statementQuery = $"SELECT actual_rate FROM statement WHERE date BETWEEN '{firstDate}' AND '{secondDate}'";

                using (SqlConnection connection = database.getSqlConnection())
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(statementQuery, connection))
                    {
                        SqlDataReader reader = command.ExecuteReader();

                        int index = 1;
                        int totalRate = 0;

                        while (reader.Read())
                        {
                            string rate = reader["actual_rate"].ToString();

                            replaceField($"{{k_{index}}}", rate);

                            totalRate += Convert.ToInt32(rate);

                            index++;
                        }

                        reader.Close();

                        for (int i = index; i <= 17; i++)
                        {
                            replaceField($"{{k_{i}}}", "");
                        }

                        replaceField("{k_sum}", totalRate.ToString());

                        connection.Close();
                    }
                }

                database = new Database();

                string deliveryQuery = $"SELECT released FROM delivery_note, delivery_note_composition WHERE delivery_note.delivery_note_id = delivery_note_composition.delivery_note_id AND date BETWEEN '{firstDate}' AND '{secondDate}'";

                using (SqlConnection connection = database.getSqlConnection())
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(deliveryQuery, connection))
                    {
                        SqlDataReader reader = command.ExecuteReader();

                        int index = 1;
                        int totalReleased = 0;

                        while (reader.Read())
                        {
                            string released = reader["released"].ToString();

                            replaceField($"{{o_{index}}}", released);

                            totalReleased += Convert.ToInt32(released);

                            index++;
                        }

                        reader.Close();

                        for (int i = index; i <= 17; i++)
                        {
                            replaceField($"{{o_{i}}}", "");
                        }

                        replaceField("{o_sum}", totalReleased.ToString());

                        connection.Close();
                    }
                }

                string fileName = "Отчёт выбытия молока";

                string outputPath = @"D:\Учеба\Диплом\warehouse-main\Warehouse\Resources\" + fileName + ".docx";

                document.SaveAs2(outputPath);
                document.Close();

                System.Diagnostics.Process.Start(outputPath);
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

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            generateWordDocument();
        }
    }
}
