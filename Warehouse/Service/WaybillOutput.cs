using Microsoft.Office.Interop.Word;
using System.Data.SqlClient;
using System;
using Warehouse.Storage;
using System.Windows;
using Application = Microsoft.Office.Interop.Word.Application;

namespace Warehouse.Service
{
    internal class WaybillOutput
    {
        private Database database;
        private Application wordApplication;
        private Document document;

        private string templatePath = @"D:\Учеба\Диплом\warehouse-main\Warehouse\Resources\WaybillTemplate.docx";

        public WaybillOutput()
        {
            database = new Database();
        }

        public void generateWordDocument(long waybillId)
        {
            try
            {
                wordApplication = new Application();
                document = wordApplication.Documents.Open(templatePath);

                string fileName = "Товарно-транспортная накладная";
                string outputPath = @"D:\Учеба\Диплом\warehouse-main\Warehouse\Resources\" + fileName + ".docx";

                string headerQuery = $"SELECT document_number, shipper, consignor, contractor.title as con_t, date, vehicle, guide_list_number, car_owner, driver, trans_type, contractor.title + ', ' + contractor.address + ', ' + contractor.settlement_account + ', ' + contractor.bank_name as contractor, loading_point, unloading_point, route_number, finished_product.title as product_title, employee.position + ', ' + employee.surname + ' ' + employee.first_name + ' ' + employee.patronymic as fio FROM waybill, finished_product, employee, contractor WHERE waybill.product_id = finished_product.product_id AND employee.employee_id = waybill.employee_id AND contractor.contractor_id = waybill.contractor_id AND waybill_id = '{waybillId}'";

                using (SqlConnection connection = database.getSqlConnection())
                {
                    connection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand(headerQuery, connection))
                    {
                        SqlDataReader reader = sqlCommand.ExecuteReader();

                        if (reader.Read())
                        {
                            string documentNumber = reader["document_number"].ToString();
                            string shipper = reader["shipper"].ToString();
                            string consignor = reader["consignor"].ToString();
                            string conT = reader["con_t"].ToString();

                            DateTime date = Convert.ToDateTime(reader["date"]);
                            string day = date.Day.ToString();
                            string month = date.ToString("MMMM", System.Globalization.CultureInfo.CurrentCulture);
                            string year = date.Year.ToString();

                            string vehicle = reader["vehicle"].ToString();
                            string guideNumber = reader["guide_list_number"].ToString();
                            string carOwner = reader["car_owner"].ToString();
                            string driver = reader["driver"].ToString();
                            string transType = reader["trans_type"].ToString();
                            string contractor = reader["contractor"].ToString();
                            string loading = reader["loading_point"].ToString();
                            string unloading = reader["unloading_point"].ToString();
                            string routeNumber = reader["route_number"].ToString();
                            string productTitle = reader["product_title"].ToString();
                            string fio = reader["fio"].ToString();

                            replaceField("{shipper}", shipper);
                            replaceField("{consignor}", consignor);
                            replaceField("{con_t}", conT);
                            replaceField("{number}", documentNumber);
                            replaceField("{day}", day);
                            replaceField("{month}", month);
                            replaceField("{year}", year);
                            replaceField("{vehicle}", vehicle);
                            replaceField("{guide_number}", guideNumber);
                            replaceField("{car_owner}", carOwner);
                            replaceField("{driver}", driver);
                            replaceField("{trans_type}", transType);
                            replaceField("{contractor}", contractor);
                            replaceField("{loading}", loading);
                            replaceField("{unloading}", unloading);
                            replaceField("{route_number}", routeNumber);
                            replaceField("{product_title}", productTitle);
                            replaceField("{fio}", fio);
                        }

                        reader.Close();
                    }

                    string waybillCompQuery = $"SELECT fat, mass, acidity, temperature, cleaning_group, density, packaging_type, brutto, tara, netto, grade, quantity, sort FROM waybill_composition WHERE waybill_id = '{waybillId}' AND waybill_type = 'posted'";

                    using (SqlCommand sqlCommand = new SqlCommand(waybillCompQuery, connection))
                    {
                        SqlDataReader reader = sqlCommand.ExecuteReader();

                        if (reader.Read())
                        {
                            string fat = reader["fat"].ToString();
                            string mass = reader["mass"].ToString();
                            string acidity = reader["acidity"].ToString();
                            string temperature = reader["temperature"].ToString();
                            string cleaningGroup = reader["cleaning_group"].ToString();
                            string density = reader["density"].ToString();
                            string packagingType = reader["packaging_type"].ToString();
                            string quantity = reader["quantity"].ToString();
                            string brutto = reader["brutto"].ToString();
                            string netto = reader["netto"].ToString();
                            string tara = reader["tara"].ToString();
                            string grade = reader["grade"].ToString();
                            string sort = reader["sort"].ToString();

                            replaceField("{fat}", fat);
                            replaceField("{mass}", mass);
                            replaceField("{acidit}", acidity);
                            replaceField("{temp}", temperature);
                            replaceField("{group}", cleaningGroup);
                            replaceField("{class}", grade);
                            replaceField("{densi}", density);
                            replaceField("{sor}", sort);
                            replaceField("{type}", packagingType);
                            replaceField("{quant}", quantity);
                            replaceField("{brut}", brutto);
                            replaceField("{tar}", tara);
                            replaceField("{net}", netto);
                        }

                        reader.Close();
                    }

                    string waybillCompAccQuery = $"SELECT fat, mass, acidity, temperature, cleaning_group, density, packaging_type, brutto, tara, netto, grade, quantity, sort FROM waybill_composition WHERE waybill_id = '{waybillId}' AND waybill_type = 'accepted'";

                    using (SqlCommand sqlCommand = new SqlCommand(waybillCompAccQuery, connection))
                    {
                        SqlDataReader reader = sqlCommand.ExecuteReader();

                        if (reader.Read())
                        {
                            string fat = reader["fat"].ToString();
                            string mass = reader["mass"].ToString();
                            string acidity = reader["acidity"].ToString();
                            string temperature = reader["temperature"].ToString();
                            string cleaningGroup = reader["cleaning_group"].ToString();
                            string density = reader["density"].ToString();
                            string packagingType = reader["packaging_type"].ToString();
                            string quantity = reader["quantity"].ToString();
                            string brutto = reader["brutto"].ToString();
                            string netto = reader["netto"].ToString();
                            string tara = reader["tara"].ToString();
                            string grade = reader["grade"].ToString();
                            string sort = reader["sort"].ToString();

                            replaceField("{ft}", fat);
                            replaceField("{ms}", mass);
                            replaceField("{ac}", acidity);
                            replaceField("{tmp}", temperature);
                            replaceField("{gr}", cleaningGroup);
                            replaceField("{cl}", grade);
                            replaceField("{dn}", density);
                            replaceField("{sr}", sort);
                            replaceField("{tp}", packagingType);
                            replaceField("{qt}", quantity);
                            replaceField("{br}", brutto);
                            replaceField("{ta}", tara);
                            replaceField("{ne}", netto);
                        } else
                        {
                            replaceField("{ft}", "");
                            replaceField("{ms}", "");
                            replaceField("{ac}", "");
                            replaceField("{tmp}", "");
                            replaceField("{gr}", "");
                            replaceField("{cl}", "");
                            replaceField("{dn}", "");
                            replaceField("{sr}", "");
                            replaceField("{tp}", "");
                            replaceField("{qt}", "");
                            replaceField("{br}", "");
                            replaceField("{ta}", "");
                            replaceField("{ne}", "");
                        }

                        reader.Close();
                    }

                    connection.Close();
                }

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
    }
}