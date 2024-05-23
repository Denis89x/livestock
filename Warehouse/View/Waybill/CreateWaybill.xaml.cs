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

            comboBoxRepo.insertEmployeesIntoComboBox(EmployeeComboBox);
            comboBoxRepo.insertContractorIntoComboBox(ContractorComboBox);
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
            ComboBoxEntity employee = (ComboBoxEntity)EmployeeComboBox.SelectedItem;
            ComboBoxEntity product = (ComboBoxEntity)ProductComboBox.SelectedItem;
            string carOwner = CarOwnerBox.Text;
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
                    WaybillEntity waybill = new WaybillEntity(contractor.id, employee.id, product.id, carOwner, date, vehicle, shipper, consignor, loadingPoint, unloadingPoint, vehicleNumber, guide, routeNumber, driver, trastType);

                    if (waybillValidation.isWaybillValid(waybill))
                    {
                        using (SqlConnection connection = database.getSqlConnection())
                        {
                            database.checkConnection();
                            SqlTransaction transaction = connection.BeginTransaction();

                            try
                            {
                                long waybillId = createWaybillAndReturnId(waybill, transaction);
      
                                string fat = FatBox.Text;
                                string mass = MassBox.Text;
                                string acidity = AcidityBox.Text;
                                string temperature = TemperatureBox.Text;
                                string cleaningGroup = CleningGroupCombo.Text;
                                string density = DensityBox.Text;
                                string packagingType = PackagingTypeCombo.Text;
                                string brutto = BruttoBox.Text;
                                string netto = NettoBox.Text;
                                string grade = GradeCombo.Text;
                                string tara = TaraBox.Text;
                                string quantity = QuantityBox.Text;

                                WaybillCompositionEntity waybillComposition = new WaybillCompositionEntity(waybillId, "posted", fat, mass, acidity, temperature, cleaningGroup, density, packagingType, brutto, tara, netto, grade, quantity);

                                if (waybillCompositionValidation.isWaybillCompositionValid(waybillComposition))
                                {
                  
                                    createWaybillComposition(waybillComposition, transaction);
                     
                                    transaction.Commit();
                                    waybillCrud.fetchToGrid(dataGrid);
                                    database.checkConnection();
                       
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
                MessageBox.Show("Выберите дату!" + ex.Message);
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
            string query = $"INSERT INTO waybill_composition(waybill_id, waybill_type, fat, mass, acidity, temperature, cleaning_group, density, packaging_type, brutto, tara, netto, grade, quantity) VALUES('{entity.waybillId}', N'{entity.waybillType}', N'{entity.fat}', N'{entity.mass}', N'{entity.acidity}', N'{entity.temperature}', N'{entity.cleaningGroup}', N'{entity.density}', N'{entity.packagingType}', N'{entity.brutto}', N'{entity.tara}', N'{entity.netto}', N'{entity.grade}', '{entity.quantity}')";
            SqlCommand command = new SqlCommand(query, transaction.Connection, transaction);

            command.ExecuteNonQuery();
        }
    }
}