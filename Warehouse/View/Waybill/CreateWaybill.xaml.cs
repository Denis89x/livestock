using System;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using Warehouse.Entity;
using Warehouse.Storage;
using Warehouse.Validation;

namespace Warehouse.View.Waybill
{
    public partial class CreateWaybill : Window
    {
        private Database database;
        private DataGrid dataGrid;
        private CrudRepo<WaybillEntity> waybillCrud;
        private ComboBoxRepo comboBoxRepo;
        private CommonValidation commonValidation;
        private WaybillValidation waybillValidation;
        private WaybillCompositionValidation waybillCompositionValidation;

        public CreateWaybill(DataGrid dataGrid)
        {
            InitializeComponent();

            this.dataGrid = dataGrid;

            database = new Database();
            waybillValidation = new WaybillValidation();
            waybillCompositionValidation = new WaybillCompositionValidation();
            waybillCrud = new WaybillRepoImpl();
            comboBoxRepo = new ComboBoxRepoImpl();
            commonValidation = new CommonValidation();

            comboBoxRepo.insertEmployeesIntoComboBox(EmployeeComboBox);
            comboBoxRepo.insertContractorIntoComboBox(ContractorComboBox);
            comboBoxRepo.insertContractorIntoComboBox(CarOwnerBox);
            comboBoxRepo.insertProductsIntoComboBox(ProductComboBox);

            DatePicker.Text = DateTime.Today.ToString("yyyy-MM-dd");
        }

        private void ReturnFirst_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxEntity contractor = (ComboBoxEntity)ContractorComboBox.SelectedItem;
            ComboBoxEntity carOwner = (ComboBoxEntity)ContractorComboBox.SelectedItem;
            ComboBoxEntity employee = (ComboBoxEntity)EmployeeComboBox.SelectedItem;
            ComboBoxEntity product = (ComboBoxEntity)ProductComboBox.SelectedItem;
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

                if (contractor != null && employee != null && product != null)
                {
                    WaybillEntity waybill = new WaybillEntity(contractor.id, employee.id, product.id, carOwner.name, date, vehicle, shipper, consignor, loadingPoint, unloadingPoint, vehicleNumber, guide, routeNumber, driver, trastType);

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
                                long waybillId = createWaybillAndReturnId(waybill, transaction);
                                string fat = FatBox.Text;
                                string mass = MassBox.Text;
                                string massResult = MassResultBox.Text;
                                string acidity = AcidityBox.Text;
                                string temperature = TemperatureBox.Text;
                                string cleaningGroup = CleningGroupCombo.Text;
                                string density = DensityBox.Text;
                                string brutto = BruttoBox.Text;
                                string netto = NettoBox.Text;
                                string grade = GradeCombo.Text;
                                string tara = TaraBox.Text;
                                string quantity = QuantityBox.Text;
                                string sort = SortCombo.Text;

                                WaybillCompositionEntity waybillComposition = new WaybillCompositionEntity(waybillId, "posted", fat, mass, massResult, acidity, temperature, cleaningGroup, density, "Без упаковки", brutto, tara, netto, grade, quantity, sort);
  
                                if (waybillCompositionValidation.isWaybillCompositionValid(waybillComposition))
                                {
                                    createWaybillComposition(waybillComposition, transaction);
 
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
                }
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show("Выберите дату!");
            }
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private long createWaybillAndReturnId(WaybillEntity entity, SqlTransaction transaction)
        {
            string query = $"INSERT INTO waybill(contractor_id, employee_id, product_id, document_number, car_owner, date, vehicle, shipper, consignor, loading_point, unloading_point, vehicle_number, guide_list_number, route_number, driver, trans_type) " +
                           $"OUTPUT INSERTED.waybill_id " +
                           $"VALUES('{entity.contractorId}', '{entity.employeeId}', '{entity.productId}', '{entity.documentNumber}', N'{entity.carOwner}', '{entity.date}', N'{entity.vehicle}', N'{entity.shipper}', N'{entity.consignor}', N'{entity.loadingPoint}', N'{entity.unloadingPoint}', N'{entity.vehicleNumber}', '{entity.guideListNumber}', '{entity.routeNumber}', N'{entity.driver}', N'{entity.transType}')";

            SqlCommand command = new SqlCommand(query, transaction.Connection, transaction);

            long insertedId = (long) command.ExecuteScalar();

            return insertedId;
        }

        private void createWaybillComposition(WaybillCompositionEntity entity, SqlTransaction transaction)
        {
            try
            {
                string massResult = Convert.ToDouble(entity.massResult).ToString(System.Globalization.CultureInfo.InvariantCulture);

                string query = $"INSERT INTO waybill_composition(waybill_id, waybill_type, fat, mass, mass_result, acidity, temperature, cleaning_group, density, packaging_type, brutto, tara, netto, grade, quantity, sort) VALUES('{entity.waybillId}', N'{entity.waybillType}', '{entity.fat}', '{entity.mass}', '{massResult}', '{entity.acidity}', '{entity.temperature}', N'{entity.cleaningGroup}', N'{entity.density}', N'{entity.packagingType}', '{entity.brutto}', N'{entity.tara}', N'{entity.netto}', N'{entity.grade}', '{entity.quantity}', N'{entity.sort}')";
                SqlCommand command = new SqlCommand(query, transaction.Connection, transaction);

                command.ExecuteNonQuery();
            } catch (SqlException ex)
            {
                MessageBox.Show("Введите допустимое количество продукта!" + ex);
            }
        }

        private void MassBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            calculateMassResult();
        }

        private void FatBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            calculateMassResult();
        }

        private void calculateMassResult()
        {
            if (FatBox.Text.Length > 0 && MassBox.Text.Length > 0)
            {
                string fat = FatBox.Text;
                string mass = MassBox.Text;

                if (commonValidation.isDoubleNumberInRange(fat, 3.0, 5.4) && commonValidation.isDoubleNumberInRange(mass, 10, 10000))
                {
                    MassResultBox.Text = (((Convert.ToDouble(fat) / 100) * Convert.ToDouble(mass)) / 0.034).ToString("0.00");
                }
            }
        }
    }
}