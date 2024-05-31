using System.Data.SqlClient;
using System;
using System.Windows;
using System.Windows.Controls;
using Warehouse.Entity;
using Warehouse.Storage;
using Warehouse.Validation;

namespace Warehouse.View.Waybill
{
    public partial class EditWaybill : Window
    {
        private Database database;
        private long waybillId;
        private DataGrid dataGrid;
        private CrudRepo<WaybillEntity> waybillCrud;
        private ComboBoxRepo comboBoxRepo;
        private WaybillValidation waybillValidation;
        private WaybillCompositionValidation waybillCompositionValidation;
        private CommonValidation commonValidation;

        public EditWaybill(long waybillId, DataGrid dataGrid)
        {
            InitializeComponent();

            this.waybillId = waybillId;
            this.dataGrid = dataGrid;

            database = new Database();
            waybillValidation = new WaybillValidation();
            commonValidation = new CommonValidation();
            waybillCompositionValidation = new WaybillCompositionValidation();
            waybillCrud = new WaybillRepoImpl();
            comboBoxRepo = new ComboBoxRepoImpl();

            comboBoxRepo.insertEmployeesIntoComboBox(EmployeeComboBox);
            comboBoxRepo.insertContractorIntoComboBox(ContractorComboBox);
            comboBoxRepo.insertContractorIntoComboBox(CarOwnerBox);

            CheckAccBox.IsChecked = true;

            fetchWaybill();
            fetchAcceptedWaybill();
            fetchPostedWaybill();
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {

            ComboBoxEntity contractor = (ComboBoxEntity)ContractorComboBox.SelectedItem;
            ComboBoxEntity carOwner = (ComboBoxEntity)ContractorComboBox.SelectedItem;
            ComboBoxEntity employee = (ComboBoxEntity)EmployeeComboBox.SelectedItem;
            string vehicle = VehicleBox.Text;
            string shipper = ShipperBox.Text;
            string consignor = ConsignorBox.Text;
            string loadingPoint = LoadingBox.Text;
            string unloadingPoint = UnloadingBox.Text;
            string vehicleNumber = VehicleNumberBox.Text;
            string guide = GuideBox.Text;
            string routeNumber = RouteNumber.Text;
            string trastType = TransTypeCombo.Text;
            string driver = DriverCombo.Text;

            try
            {
                string date = DatePicker.SelectedDate.Value.ToString("yyyy-MM-dd");

                if (contractor != null && employee != null)
                {
                    WaybillEntity waybill = new WaybillEntity(waybillId, contractor.id, 0, employee.id, carOwner.name, date, vehicle, shipper, consignor, loadingPoint, unloadingPoint, vehicleNumber, guide, routeNumber, driver, trastType);

                    if (waybillValidation.isWaybillValid(waybill))
                    {
                        database = new Database();
                        using (SqlConnection connection = database.getSqlConnection())
                        {
                            database = new Database();
                            connection.Open();

                            SqlTransaction transaction = connection.BeginTransaction();

                            try
                            {
                                string fat = FatBox.Text;
                                string mass = MassBox.Text;
                                string acidity = AcidityBox.Text;
                                string temperature = TemperatureBox.Text;
                                string cleaningGroup = CleningGroupCombo.Text;
                                string density = DensityBox.Text;
                                string brutto = BruttoBox.Text;
                                string netto = NettoBox.Text;
                                string grade = GradeCombo.Text;
                                string tara = TaraBox.Text;
                                string quantity = QuantityBox.Text;
                                string massResult = MassResultBox.Text;
                                string sort = SortCombo.Text;

                                WaybillCompositionEntity waybillComposition = new WaybillCompositionEntity(waybillId, "posted", fat, mass, massResult, acidity, temperature, cleaningGroup, density, "Без упаковки", brutto, tara, netto, grade, quantity, sort);

                                if (CheckAccBox.IsChecked == false)
                                {
                                    string fatAcc = FatAccBox.Text;
                                    string massAcc = MassAccBox.Text;
                                    string massResultAcc = MassResultAccBox.Text;
                                    string acidityAcc = AcidityAccBox.Text;
                                    string temperatureAcc = TemperatureAccBox.Text;
                                    string cleaningGroupAcc = CleningGroupAccCombo.Text;
                                    string densityAcc = DensityAccBox.Text;
                                    string bruttoAcc = BruttoAccBox.Text;
                                    string nettoAcc = NettoAccBox.Text;
                                    string gradeAcc = GradeAccCombo.Text;
                                    string taraAcc = TaraAccBox.Text;
                                    string quantityAcc = QuantityAccBox.Text;
                                    string sortAcc = SortAccCombo.Text;

                                    WaybillCompositionEntity waybillAccComposition = new WaybillCompositionEntity(waybillId, "accepted", fatAcc, massAcc, massResultAcc, acidityAcc, temperatureAcc, cleaningGroupAcc, densityAcc, "Без упаковки", bruttoAcc, taraAcc, nettoAcc, gradeAcc, quantityAcc, sortAcc);

                                    if (waybillCompositionValidation.isWaybillCompositionValid(waybillComposition) && waybillCompositionValidation.isWaybillCompositionValid(waybillAccComposition))
                                    {
                                        updateWaybill(waybill, transaction);
                                           
                                        updateWaybillComposition(waybillComposition, "posted", transaction);
                               
                                        if (checkExistAcceptedComposition(transaction))
                                        {
                                            updateWaybillComposition(waybillAccComposition, "accepted", transaction);
                                        } else
                                        {
                                            createWaybillComposition(waybillAccComposition, transaction);
                                        }

                                        transaction.Commit();

                                        waybillCrud.fetchToGrid(dataGrid);

                                        connection.Close();

                                        this.Close();
                                    }
                                    else
                                    {
                                        throw new Exception("Validation failed for WaybillComposition");
                                    }
                                }
                                else
                                {
                                    if (waybillCompositionValidation.isWaybillCompositionValid(waybillComposition))
                                    {
                                        updateWaybill(waybill, transaction);

                                        updateWaybillComposition(waybillComposition, "posted", transaction);

                                        transaction.Commit();

                                        waybillCrud.fetchToGrid(dataGrid);

                                        connection.Close();

                                        this.Close();
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                transaction.Rollback();
                                connection.Close();
                                return;
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Укажите все данные!");
                    return;
                }
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show("Выберите дату!" + ex.Message);
                return;
            }
        }

        private void fetchPostedWaybill()
        {
            string compositionPostedQuery = $"SELECT fat, mass, mass_result, waybill_composition.sort, acidity, temperature, cleaning_group, density, packaging_type, brutto, tara, netto, grade, waybill_composition.quantity FROM waybill_composition WHERE waybill_id = '{waybillId}' AND waybill_type = 'posted'";

            SqlCommand command = new SqlCommand(compositionPostedQuery, database.getSqlConnection());

            database.checkConnection();

            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    FatBox.Text = reader["fat"].ToString();
                    MassBox.Text = reader["mass"].ToString();
                    AcidityBox.Text = reader["acidity"].ToString();
                    TemperatureBox.Text = reader["temperature"].ToString();
                    CleningGroupCombo.Text = reader["cleaning_group"].ToString();
                    DensityBox.Text = reader["density"].ToString();
                    BruttoBox.Text = reader["brutto"].ToString();
                    TaraBox.Text = reader["tara"].ToString();
                    SortCombo.Text = reader["sort"].ToString();
                    NettoBox.Text = reader["netto"].ToString();
                    GradeCombo.Text = reader["grade"].ToString();
                    QuantityBox.Text = reader["quantity"].ToString();
                }
            }

            database.checkConnection();
        }

        private void updateWaybill(WaybillEntity entity, SqlTransaction transaction)
        {
            try
            {
                string query = $"UPDATE waybill SET contractor_id = '{entity.contractorId}', employee_id = '{entity.employeeId}', car_owner = N'{entity.carOwner}', date = N'{entity.date}', vehicle = N'{entity.vehicle}', shipper = N'{entity.shipper}', consignor = N'{entity.consignor}', loading_point = N'{entity.loadingPoint}', unloading_point = N'{entity.unloadingPoint}', vehicle_number = N'{entity.vehicleNumber}', guide_list_number = N'{entity.guideListNumber}', route_number = N'{entity.routeNumber}', driver = N'{entity.driver}', trans_type = N'{entity.transType}' WHERE waybill_id = '{waybillId}'";
                SqlCommand command = new SqlCommand(query, transaction.Connection, transaction);

                command.ExecuteNonQuery();
            } catch (SqlException ex)
            {
                MessageBox.Show("EX: " + ex);
            }
            
        }

        private void createWaybillComposition(WaybillCompositionEntity entity, SqlTransaction transaction)
        {
            string massResult = Convert.ToDouble(entity.massResult).ToString(System.Globalization.CultureInfo.InvariantCulture);

            string query = $"INSERT INTO waybill_composition(waybill_id, waybill_type, fat, mass, mass_result, acidity, temperature, cleaning_group, density, packaging_type, brutto, tara, netto, grade, quantity, sort) VALUES('{waybillId}', N'{entity.waybillType}', N'{entity.fat}', N'{entity.mass}', N'{massResult}', N'{entity.acidity}', N'{entity.temperature}', N'{entity.cleaningGroup}', N'{entity.density}', N'{entity.packagingType}', N'{entity.brutto}', N'{entity.tara}', N'{entity.netto}', N'{entity.grade}', '{entity.quantity}', N'{entity.sort}')";
            SqlCommand command = new SqlCommand(query, transaction.Connection, transaction);

            command.ExecuteNonQuery();
        }

        private bool checkExistAcceptedComposition(SqlTransaction transaction)
        {
            string query = $"SELECT COUNT(*) as result FROM waybill_composition WHERE waybill_type = 'accepted' AND waybill_id = '{waybillId}'";

            SqlCommand command = new SqlCommand(query, transaction.Connection, transaction);

            int result = (int)command.ExecuteScalar();

            return result > 0;
        }

        private void updateWaybillComposition(WaybillCompositionEntity entity, string waybillType, SqlTransaction transaction)
        {
            string massResult = Convert.ToDouble(entity.massResult).ToString(System.Globalization.CultureInfo.InvariantCulture);

            string query = $"UPDATE waybill_composition SET fat = N'{entity.fat}', mass = N'{entity.mass}', mass_result = '{massResult}', acidity = N'{entity.acidity}', temperature = N'{entity.temperature}', cleaning_group = N'{entity.cleaningGroup}', density = N'{entity.density}', packaging_type = N'{entity.packagingType}', brutto = N'{entity.brutto}', tara = N'{entity.tara}', netto = N'{entity.netto}', grade = N'{entity.grade}', quantity = N'{entity.quantity}', sort = N'{entity.sort}' WHERE waybill_id = '{waybillId}' AND waybill_type = '{waybillType}'";
            SqlCommand command = new SqlCommand(query, transaction.Connection, transaction);

            command.ExecuteNonQuery();
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
                }
            }

            database.checkConnection();
        }

        private void CheckAccBox_Checked(object sender, RoutedEventArgs e)
        {
            FatAccBox.IsEnabled = false;
            MassAccBox.IsEnabled = false;
            SortAccCombo.IsEnabled = false;
            AcidityAccBox.IsEnabled = false;
            CleningGroupAccCombo.IsEnabled = false;
            DensityAccBox.IsEnabled = false;
            TemperatureAccBox.IsEnabled = false;
            GradeAccCombo.IsEnabled = false;
            BruttoAccBox.IsEnabled = false;
            TaraAccBox.IsEnabled = false;
            NettoAccBox.IsEnabled = false;
            QuantityAccBox.IsEnabled = false;
            TaraAccBox.IsEnabled = false;
        }

        private void CheckAccBox_Unchecked(object sender, RoutedEventArgs e)
        {
            FatAccBox.IsEnabled = true;
            MassAccBox.IsEnabled = true;
            SortAccCombo.IsEnabled = true;
            AcidityAccBox.IsEnabled = true;
            CleningGroupAccCombo.IsEnabled = true;
            DensityAccBox.IsEnabled = true;
            TemperatureAccBox.IsEnabled = true;
            GradeAccCombo.IsEnabled = true;
            BruttoAccBox.IsEnabled = true;
            TaraAccBox.IsEnabled = true;
            NettoAccBox.IsEnabled = true;
            QuantityAccBox.IsEnabled = true;
            TaraAccBox.IsEnabled = true;
        }

        private void MassBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            calculateMassResult(FatBox, MassBox, MassResultBox);
        }

        private void FatBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            calculateMassResult(FatBox, MassBox, MassResultBox);
        }

        private void calculateMassResult(TextBox fatBox, TextBox massBox, TextBox result)
        {
            if (fatBox.Text.Length > 0 && massBox.Text.Length > 0)
            {
                string fat = fatBox.Text;
                string mass = massBox.Text;

                if (commonValidation.isDoubleNumberInRange(fat, 3.0, 5.4) && commonValidation.isDoubleNumberInRange(mass, 10, 10000))
                {
                    result.Text = (((Convert.ToDouble(fat) / 100) * Convert.ToDouble(mass)) / 0.034).ToString("0.00");
                }
            }
        }

        private void FatAccBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            calculateMassResult(FatAccBox, MassAccBox, MassResultAccBox);
        }

        private void MassAccBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            calculateMassResult(FatAccBox, MassAccBox, MassResultAccBox);
        }
    }
}