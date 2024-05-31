using Microsoft.Office.Interop.Word;
using System.Data.SqlClient;
using System;
using Warehouse.Storage;
using Warehouse.Validation;
using Application = Microsoft.Office.Interop.Word.Application;
using Warehouse.Entity;
using System.Windows;

namespace Warehouse.View.OutputDocument
{
    public partial class StatementOutView : System.Windows.Window
    {
        private Database database;
        private CommonValidation comoValidation;

        private ComboBoxRepo comboBoxRepo;
        private Application wordApplication;
        private Document document;

        private string templatePath = @"D:\Учеба\Диплом\warehouse-main\Warehouse\Resources\StatementTemplate.docx";

        public StatementOutView()
        {
            InitializeComponent();

            database = new Database();
            comoValidation = new CommonValidation();
            comboBoxRepo = new ComboBoxRepoImpl();

            comboBoxRepo.insertDivisionsIntoComboBox(DivisionCombo);
            comboBoxRepo.insertEmployeesIntoComboBox(EmployeeCombo);
            comboBoxRepo.insertCattleIntoComboBox(CattleCombo);

            FirstDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
            SecondDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
        }

        public void generateWordDocument()
        {
            try
            {
                wordApplication = new Application();
                document = wordApplication.Documents.Open(templatePath);

                ComboBoxEntity employee = (ComboBoxEntity)EmployeeCombo.SelectedItem;
                ComboBoxEntity division = (ComboBoxEntity)DivisionCombo.SelectedItem;
                ComboBoxEntity cattle = (ComboBoxEntity)CattleCombo.SelectedItem;

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
                if (employee != null && division != null && cattle != null)
                {
                    string headerQuery = $"SELECT statement_number, division.division_type, cattle.cattle_type, employee.surname + ' ' + employee.first_name + ' ' + employee.patronymic as fio FROM statement, employee, cattle, division WHERE statement.employee_id = employee.employee_id AND statement.cattle_id = cattle.cattle_id AND statement.division_id = division.division_id AND statement.division_id = '{division.id}' AND statement.cattle_id = '{cattle.id}' AND statement.employee_id = '{employee.id}' AND statement.date BETWEEN '{firstDate}' AND '{secondDate}'";
                    database = new Database();
                    using (SqlConnection connection = database.getSqlConnection())
                    {
                        connection.Open();
                        using (SqlCommand command = new SqlCommand(headerQuery, connection))
                        {
                            SqlDataReader reader = command.ExecuteReader();

                            if (reader.Read())
                            {
                                string number = reader["statement_number"].ToString();

                                string fio = reader["fio"].ToString();

                                replaceField("{division}", division.name);
                                replaceField("{number}", number);
                                replaceField("{cattle}", cattle.name);
                                replaceField("{fio}", fio);
                            }

                            reader.Close();
                        }

                        string tableQuery = $"SELECT date, supply_rate, animal_quantity, finished_product.title + ', ' + finished_product.unit as pu, actual_rate FROM statement, finished_product WHERE statement.product_id = finished_product.product_id AND statement.division_id = '{division.id}' AND statement.cattle_id = '{cattle.id}' AND statement.employee_id = '{employee.id}' AND statement.date BETWEEN '{firstDate}' AND '{secondDate}'";

                        using (SqlCommand command = new SqlCommand(tableQuery, connection))
                        {
                            SqlDataReader reader = command.ExecuteReader();

                            int index = 1;

                            while (reader.Read())
                            {
                                string date = Convert.ToDateTime(reader["date"]).ToString("dd.MM.yyyy");
                                string supplyRate = reader["supply_rate"].ToString();
                                string animalQuantity = reader["animal_quantity"].ToString();
                                string pu = reader["pu"].ToString();
                                string actualRate = reader["actual_rate"].ToString();

                                replaceField($"{{d_{index}}}", date);
                                replaceField($"{{sp_{index}}}", supplyRate);
                                replaceField($"{{p_{index}}}", pu);
                                replaceField($"{{a_{index}}}", animalQuantity);
                                replaceField($"{{ar_{index}}}", actualRate);

                                index++;
                            }

                            reader.Close();

                            for (int i = index; i <= 7; i++)
                            {
                                replaceField($"{{d_{i}}}", "");
                                replaceField($"{{sp_{i}}}", "");
                                replaceField($"{{p_{i}}}", "");
                                replaceField($"{{a_{i}}}", "");
                                replaceField($"{{ar_{i}}}", "");
                            }
                        }

                        connection.Close();
                    }
                }
                        
                string fileName = "Ведомость учёта расхода кормов";

                string outputPath = @"D:\Учеба\Диплом\warehouse-main\Warehouse\Resources\" + fileName + ".docx";

                document.SaveAs2(outputPath);
                document.Close();

                System.Diagnostics.Process.Start(outputPath);

            } catch (Exception ex)
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