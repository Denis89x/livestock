using System;
using System.Data.SqlClient;
using System.Windows;
using Warehouse.Storage;

namespace Warehouse.View.Waybill
{

    public partial class ViewWaybill : Window
    {
        private Database database;
        private long waybillId;

        public ViewWaybill(long waybillId)
        {
            InitializeComponent();

            database = new Database();

            this.waybillId = waybillId;

            fetchPostedWaybill();
            fetchAcceptedWaybill();
            fetchWaybill();
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void fetchPostedWaybill()
        {
            string compositionPostedQuery = $"SELECT fat, mass, mass_result, acidity, temperature, cleaning_group, density, packaging_type, brutto, tara, netto, grade, waybill_composition.quantity, waybill_composition.sort FROM waybill_composition WHERE waybill_id = '{waybillId}' AND waybill_type = 'posted'";

            SqlCommand command = new SqlCommand(compositionPostedQuery, database.getSqlConnection());

            database.checkConnection();

            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    FatBox.Text = reader["fat"].ToString();
                    MassBox.Text = reader["mass"].ToString();
                    SortCombo.Text = reader["sort"].ToString();
                    MassResultBox.Text = reader["mass_result"].ToString();
                    AcidityBox.Text = reader["acidity"].ToString();
                    TemperatureBox.Text = reader["temperature"].ToString();
                    CleningGroupCombo.Text = reader["cleaning_group"].ToString();
                    DensityBox.Text = reader["density"].ToString();
                    BruttoBox.Text = reader["brutto"].ToString();
                    TaraBox.Text = reader["tara"].ToString();
                    NettoBox.Text = reader["netto"].ToString();
                    GradeCombo.Text = reader["grade"].ToString();
                    QuantityBox.Text = reader["quantity"].ToString();
                }
            }

            database.checkConnection();
        }

        private void fetchAcceptedWaybill()
        {
            string compositionAcceptedQuery = $"SELECT fat, mass, mass_result, waybill_composition.sort, acidity, temperature, cleaning_group, density, packaging_type, brutto, tara, netto, grade, waybill_composition.quantity FROM waybill_composition WHERE waybill_id = '{waybillId}' AND waybill_type = 'accepted'";

            SqlCommand command = new SqlCommand(compositionAcceptedQuery, database.getSqlConnection());

            database.checkConnection();

            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    FatAccBox.Text = reader["fat"].ToString();
                    MassAccBox.Text = reader["mass"].ToString();
                    MassResultAccBox.Text = reader["mass_result"].ToString();
                    SortAccCombo.Text = reader["sort"].ToString();
                    AcidityAccBox.Text = reader["acidity"].ToString();
                    TemperatureAccBox.Text = reader["temperature"].ToString();
                    CleningGroupAccCombo.Text = reader["cleaning_group"].ToString();
                    DensityAccBox.Text = reader["density"].ToString();
                    BruttoAccBox.Text = reader["brutto"].ToString();
                    TaraAccBox.Text = reader["tara"].ToString();
                    NettoAccBox.Text = reader["netto"].ToString();
                    GradeAccCombo.Text = reader["grade"].ToString();
                    QuantityAccBox.Text = reader["quantity"].ToString();
                }
            }

            database.checkConnection();
        }

        private void fetchWaybill()
        {
            string waybillQuery = $"SELECT document_number, car_owner, date, vehicle, shipper, consignor, loading_point, unloading_point, vehicle_number, guide_list_number, route_number, driver, trans_type, contractor.title as contractor_title, employee.surname, finished_product.title as product_title FROM waybill, contractor, employee, finished_product WHERE waybill.contractor_id = contractor.contractor_id AND waybill.employee_id = employee.employee_id AND waybill.product_id = finished_product.product_id AND waybill_id = '{waybillId}'";

            SqlCommand command = new SqlCommand(waybillQuery, database.getSqlConnection());

            database.checkConnection();

            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    ProductComboBox.Items.Add(reader["product_title"].ToString());
                    ProductComboBox.SelectedIndex = 0;

                    ContractorComboBox.Items.Add(reader["contractor_title"].ToString());
                    ContractorComboBox.SelectedIndex = 0;

                    EmployeeComboBox.Items.Add(reader["surname"].ToString());
                    EmployeeComboBox.SelectedIndex = 0;

                    CarOwnerBox.Text = reader["car_owner"].ToString();
                    DatePicker.Text = Convert.ToDateTime(reader["date"]).ToString("dd.MM.yyyy");
                    VehicleBox.Text = reader["vehicle"].ToString();
                    ShipperBox.Text = reader["shipper"].ToString();
                    ConsignorBox.Text = reader["consignor"].ToString();
                    LoadingBox.Text = reader["loading_point"].ToString();
                    UnloadingBox.Text = reader["unloading_point"].ToString();
                    VehicleNumberBox.Text = reader["vehicle_number"].ToString();
                    GuideBox.Text = reader["guide_list_number"].ToString();
                    DriverCombo.Text = reader["driver"].ToString();
                    TransTypeCombo.Text = reader["trans_type"].ToString();
                    RouteNumber.Text = reader["route_number"].ToString();
                    WaybillLabel.Content = "Товарно-транспортная накладная #" + reader["document_number"].ToString();
                }
            }

            database.checkConnection();
        }

    }
}