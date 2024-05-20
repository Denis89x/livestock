using Microsoft.Office.Interop.Word;
using System.Data.SqlClient;
using System;
using Warehouse.Storage;
using System.Windows;
using Application = Microsoft.Office.Interop.Word.Application;
using System.IO;
using System.Diagnostics;

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
            wordApplication = new Application();
            try
            {
                document = wordApplication.Documents.Open(templatePath);
            } catch (Exception)
            {
                MessageBox.Show("Закройте шаблон этого файла!");
            }
        }

        public void generateWordDocument(long waybillId)
        {
            if (document != null)
            {
                /*if (IsWordRunning())
                {
                    MessageBox.Show("Закройте Microsoft Word перед генерацией документа.");
                    return;
                }*/

                string query = $"SELECT contractor.title + ' ' + contractor.address + ' ' + contractor.settlement_account + ' ' + contractor.bank_name as contractor, employee.surname + ' ' + employee.first_name + ' ' + employee.patronymic + ' ' + employee.position as fio, waybill.document_number, car_owner, date, vehicle, shipper, consignor, loading_point, unloading_point, treaty, vehicle_number, guide_list_number, route_number, finished_product.title as product_title, fat, mass, acidity, temperature, cleaning_group, density, waybill_composition.sort, waybill_composition.packaging_type, quantity, brutto, netto, tara, grade FROM waybill, waybill_composition, finished_product, employee, contractor WHERE waybill.contractor_id = contractor.contractor_id AND waybill.employee_id = employee.employee_id AND waybill_composition.waybill_id = waybill.waybill_id AND waybill_composition.product_id = finished_product.product_id AND waybill_composition.waybill_id = '{waybillId}'";

                database.checkConnection();

                string fileName = "Товарно-транспортная накладная";
                string outputPath = @"D:\Учеба\Диплом\warehouse-main\Warehouse\Resources\" + fileName + ".docx";

                if (File.Exists(outputPath))
                {
                    try
                    {
                        File.Delete(outputPath);
                    }
                    catch (IOException ex)
                    {
                        MessageBox.Show($"Ошибка удаления файла: {ex.Message}");
                        return;
                    }
                }

                using (SqlCommand sqlCommand = new SqlCommand(query, database.getSqlConnection()))
                {
                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    if (reader.Read())
                    {
                        string contractor = reader["contractor"].ToString();
                        string fio = reader["fio"].ToString();
                        string documentNumber = reader["document_number"].ToString();
                        string carOwner = reader["car_owner"].ToString();
                        string vehicle = reader["vehicle"].ToString();
                        string shipper = reader["shipper"].ToString();
                        string consignor = reader["consignor"].ToString();
                        string loading = reader["loading_point"].ToString();
                        string unloading = reader["unloading_point"].ToString();
                        string treaty = reader["treaty"].ToString();
                        string vehicleNumber = reader["vehicle_number"].ToString();
                        string guideList = reader["guide_list_number"].ToString();
                        string routeNumber = reader["route_number"].ToString();
                        string productTitle = reader["product_title"].ToString();
                        string fat = reader["fat"].ToString();
                        string mass = reader["mass"].ToString();
                        string acidity = reader["acidity"].ToString();
                        string temperature = reader["temperature"].ToString();
                        string cleaningGroup = reader["cleaning_group"].ToString();
                        string density = reader["density"].ToString();
                        string sort = reader["sort"].ToString();
                        string packagingType = reader["packaging_type"].ToString();
                        string quantity = reader["quantity"].ToString();
                        string brutto = reader["brutto"].ToString();
                        string netto = reader["netto"].ToString();
                        string tara = reader["tara"].ToString();
                        string grade = reader["grade"].ToString();

                        DateTime date = Convert.ToDateTime(reader["date"]);
                        int day = date.Day;
                        int month = date.Month;
                        int year = date.Year;

                        string monthName = date.ToString("MMMM", System.Globalization.CultureInfo.CurrentCulture);

                        replaceField("{day}", day.ToString());
                        replaceField("{number}", documentNumber);
                        replaceField("{month}", monthName);
                        replaceField("{year}", year.ToString());
                        replaceField("{vehicle}", vehicle);
                        replaceField("{guide_number}", guideList);
                        replaceField("{car_owner}", carOwner);
                        replaceField("{contractor}", contractor);
                        replaceField("{shipper}", shipper);
                        replaceField("{consignor}", consignor);
                        replaceField("{loading}", loading);
                        replaceField("{unloading}", unloading);
                        replaceField("{route_number}", routeNumber);
                        replaceField("{treaty}", treaty);
                        replaceField("{product_title}", productTitle);
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
                        replaceField("{fio}", fio);
                        replaceField("{net}", netto);
                    }

                    reader.Close();
                }

                try
                {
                    document.SaveAs2(outputPath);
                    document.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Закройте текущий файл перед созданием нового!");
                }

                System.Diagnostics.Process.Start(outputPath);

                database.checkConnection();
            } else
            {
                MessageBox.Show("Закройте word!");
            }
        }

        private void replaceField(string field, string value)
        {
            document.Content.Find.ClearFormatting();
            document.Content.Find.Execute(FindText: field, ReplaceWith: value);
        }

        private bool IsWordRunning()
        {
            Process[] processes = Process.GetProcessesByName("WINWORD");

            return processes.Length > 0;
        }
    }
}