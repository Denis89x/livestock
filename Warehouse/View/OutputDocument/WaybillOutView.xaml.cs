using Microsoft.Office.Interop.Word;
using System.Windows;
using Warehouse.Storage;
using Application = Microsoft.Office.Interop.Word.Application;
using System;
using System.Data.SqlClient;
using Warehouse.Entity;

namespace Warehouse.View.OutputDocument
{
    public partial class WaybillOutView : System.Windows.Window
    {
        private Database database;

        private ComboBoxRepo comboBoxRepo;

        private Application wordApplication;
        private Document document;

        private string templatePath = @"D:\Учеба\Диплом\warehouse-main\Warehouse\Resources\WaybillTotal.docx";


        public WaybillOutView()
        {
            InitializeComponent();

            database = new Database();
            comboBoxRepo = new ComboBoxRepoImpl();

            FirstDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
            SecondDate.Text = DateTime.Today.ToString("yyyy-MM-dd");

            comboBoxRepo.insertContractorIntoComboBox(ContractorCombo);
        }

        private void generateWordDocument()
        {
            try
            {
                wordApplication = new Application();
                document = wordApplication.Documents.Open(templatePath);

                ComboBoxEntity contractor = (ComboBoxEntity)ContractorCombo.SelectedItem;

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

                if (contractor != null)
                {
                    string query = $"SELECT waybill.date, document_number, quantity, mass, contractor.title FROM waybill, waybill_composition, contractor WHERE waybill.waybill_id = waybill_composition.waybill_id AND waybill.contractor_id = contractor.contractor_id AND waybill.waybill_id = waybill_composition.waybill_id AND waybill_composition.waybill_type = 'posted' AND waybill.date BETWEEN '{firstDate}' AND '{secondDate}' AND waybill.contractor_id = '{contractor.id}'";

                    int totalMass = 0;

                    database = new Database();

                    using (SqlConnection connection = database.getSqlConnection())
                    {
                        connection.Open();
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            SqlDataReader reader = command.ExecuteReader();

                            int index = 1;

                            while (reader.Read())
                            {
                                string date = Convert.ToDateTime(reader["date"]).ToString("dd.MM.yyyy");
                                string documentNumber = reader["document_number"].ToString();
                                string quantity = reader["quantity"].ToString();
                                string mass = reader["mass"].ToString();
                                string title = reader["title"].ToString();

                                replaceField($"{{d_{index}}}", date);
                                replaceField($"{{n_{index}}}", documentNumber);
                                replaceField($"{{q_{index}}}", quantity);
                                replaceField($"{{m_{index}}}", mass);
                                replaceField($"{{c_{index}}}", title);

                                totalMass += Convert.ToInt32(mass);

                                index++;
                            }

                            reader.Close();

                            for (int i = index; i <= 21; i++)
                            {
                                replaceField($"{{d_{i}}}", "");
                                replaceField($"{{n_{i}}}", "");
                                replaceField($"{{q_{i}}}", "");
                                replaceField($"{{m_{i}}}", "");
                                replaceField($"{{c_{i}}}", "");
                            }

                            replaceField("{m_sum}", totalMass.ToString());

                            connection.Close();
                        }
                    }

                    string fileName = "Отчёт реализации молока";

                    string outputPath = @"D:\Учеба\Диплом\warehouse-main\Warehouse\Resources\" + fileName + ".docx";

                    document.SaveAs2(outputPath);
                    document.Close();

                    System.Diagnostics.Process.Start(outputPath);
                } else
                {
                    MessageBox.Show("Выберите контрагента!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка. Закройте все экземпляры Microsoft Word и попробуйте снова!");

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

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            generateWordDocument();
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
