using Microsoft.Office.Interop.Word;
using System.Windows;
using Warehouse.Storage;
using Warehouse.Validation;
using Application = Microsoft.Office.Interop.Word.Application;
using Warehouse.Entity;
using System;
using System.Data.SqlClient;

namespace Warehouse.View.OutputDocument
{
    public partial class RecordOutView : System.Windows.Window
    {
        private Database database;
        private CommonValidation comoValidation;

        private ComboBoxRepo comboBoxRepo;
        private Application wordApplication;
        private Document document;

        private string templatePath = @"D:\Учеба\Диплом\warehouse-main\Warehouse\Resources\RecordCardTemplate.docx";

        public RecordOutView()
        {
            InitializeComponent();

            database = new Database();
            comoValidation = new CommonValidation();
            comboBoxRepo = new ComboBoxRepoImpl();

            comboBoxRepo.insertDivisionsIntoComboBox(DivisionCombo);
            comboBoxRepo.insertEmployeesIntoComboBox(EmployeeCombo);
            comboBoxRepo.insertProductsIntoComboBox(ProductCombo);

            FirstDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
            SecondDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
        }

        private void generateWordDocument()
        {
            try
            {
                wordApplication = new Application();
                document = wordApplication.Documents.Open(templatePath);

                ComboBoxEntity employee = (ComboBoxEntity)EmployeeCombo.SelectedItem;
                ComboBoxEntity division = (ComboBoxEntity)DivisionCombo.SelectedItem;
                ComboBoxEntity product = (ComboBoxEntity)ProductCombo.SelectedItem;

                string firstDate = "";
                string secondDate = "";

                try
                {
                    firstDate = FirstDate.SelectedDate.Value.ToString("yyyy-MM-dd");
                    secondDate = SecondDate.SelectedDate.Value.ToString("yyyy-MM-dd");

                    replaceField("{first_date}", firstDate);
                    replaceField("{second_date}", secondDate);
                }
                catch (InvalidOperationException)
                {
                    MessageBox.Show("Выберите дату!");
                    return;
                }
                if (employee != null && division != null && product != null)
                {
                    string headerQuery = $"SELECT employee.surname + ' ' + employee.first_name + ' ' + employee.patronymic as fio from employee WHERE employee_id = '{employee.id}'";
                    database = new Database();
                    using (SqlConnection connection = database.getSqlConnection())
                    {
                        connection.Open();
                        using (SqlCommand command = new SqlCommand(headerQuery, connection))
                        {
                            SqlDataReader reader = command.ExecuteReader();

                            if (reader.Read())
                            {
                                string fio = reader["fio"].ToString();

                                replaceField("{division}", division.name);
                                replaceField("{fio}", fio);
                            }

                            reader.Close();
                        }

                        string tableQuery = $"SELECT date, cow_quantity, morning, midday, evening, card_number FROM record_card WHERE product_id = '{product.id}' AND division_id = '{division.id}' AND employee_id = '{employee.id}' AND date BETWEEN '{firstDate}' AND '{secondDate}'";

                        int morningSum = 0, middaySum = 0, eveningSum = 0, summaryResult = 0;

                        using (SqlCommand command = new SqlCommand(tableQuery, connection))
                        {
                            SqlDataReader reader = command.ExecuteReader();

                            int index = 1;

                            while (reader.Read())
                            {
                                string date = Convert.ToDateTime(reader["date"]).ToString("dd.MM.yyyy");
                                string cowQuantity = reader["cow_quantity"].ToString();
                                string morning = reader["morning"].ToString();
                                string midday = reader["midday"].ToString();
                                string evening = reader["evening"].ToString();
                                string cardNumber = reader["card_number"].ToString();

                                int summary = Convert.ToInt32(morning) + Convert.ToInt32(midday) + Convert.ToInt32(evening);

                                replaceField($"{{date_{index}}}", date);
                                replaceField($"{{num_{index}}}", cardNumber);
                                replaceField($"{{cow_quantity_{index}}}", cowQuantity);
                                replaceField($"{{morning_{index}}}", morning);
                                replaceField($"{{midday_{index}}}", midday);
                                replaceField($"{{evening_{index}}}", evening);
                                replaceField($"{{summary_{index}}}", summary.ToString());

                                morningSum += Convert.ToInt32(morning);
                                middaySum += Convert.ToInt32(midday);
                                eveningSum += Convert.ToInt32(evening);
                                summaryResult += Convert.ToInt32(summary);

                                index++;
                            }

                            reader.Close();

                            for (int i = index; i <= 9; i++)
                            {
                                replaceField($"{{date_{i}}}", "");
                                replaceField($"{{num_{i}}}", "");
                                replaceField($"{{cow_quantity_{i}}}", "");
                                replaceField($"{{morning_{i}}}", "");
                                replaceField($"{{midday_{i}}}", "");
                                replaceField($"{{evening_{i}}}", "");
                                replaceField($"{{summary_{i}}}", "");
                            }
                        }

                        if (morningSum > 0 && middaySum > 0 && eveningSum > 0 && summaryResult > 0)
                        {
                            replaceField("{mor_sum}", morningSum.ToString());
                            replaceField("{mid_sum}", middaySum.ToString());
                            replaceField("{ev_sum}", eveningSum.ToString());
                            replaceField("{s_sum}", summaryResult.ToString());

                        } else
                        {
                            replaceField("{mor_sum}", "");
                            replaceField("{mid_sum}", "");
                            replaceField("{ev_sum}", "");
                            replaceField("{s_sum}", "");
                        }

                        connection.Close();
                    }
                }

                string fileName = "Карточка учёта надоя молока";

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
            this.Close();
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            generateWordDocument();
        }
    }
}
