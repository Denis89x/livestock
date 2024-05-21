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
        private long waybillId;
        private DataGrid dataGrid;
        private CrudRepo<WaybillEntity> waybillCrud;
        private ComboBoxRepo comboBoxRepo;
        private WaybillValidation validation;

        public EditWaybill(long waybillId, string contractor, string employee, string carOwner, string date,
            string vehicle, string shipper, string consignor, string loadingPoint, string unloadingPoint,
            string treaty, string vehicleNumber, string guideList, string routeNumber, DataGrid dataGrid)
        {
            InitializeComponent();

            this.waybillId = waybillId;
            this.dataGrid = dataGrid;

            validation = new WaybillValidation();
            waybillCrud = new WaybillRepoImpl();
            comboBoxRepo = new ComboBoxRepoImpl();

            comboBoxRepo.insertEmployeesIntoComboBox(EmployeeComboBox);
            comboBoxRepo.insertContractorIntoComboBox(ContractorComboBox);

            CarOwnerBox.Text = carOwner;
            VehicleBox.Text = vehicle;
            ShipperBox.Text = shipper;
            DatePicker.Text = date; 
            ConsignorBox.Text = consignor;
            LoadingBox.Text = loadingPoint;
            UnloadingBox.Text = unloadingPoint;
            TreatyBox.Text = treaty;
            VehicleNumberBox.Text = vehicleNumber;
            GuideBox.Text = guideList;
            RouteNumber.Text = routeNumber;
        }

        private void ReturnFirst_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void NextFirst_Click(object sender, RoutedEventArgs e)
        {
            VehicleBox.Visibility = Visibility.Visible;
            ShipperBox.Visibility = Visibility.Visible;
            ConsignorBox.Visibility = Visibility.Visible;
            LoadingBox.Visibility = Visibility.Visible;
            PreviewFirst.Visibility = Visibility.Visible;
            NextSecond.Visibility = Visibility.Visible;

            ContractorComboBox.Visibility = Visibility.Collapsed;
            EmployeeComboBox.Visibility = Visibility.Collapsed;
            CarOwnerBox.Visibility = Visibility.Collapsed;
            DatePicker.Visibility = Visibility.Collapsed;
            ReturnFirst.Visibility = Visibility.Collapsed;
            NextFirst.Visibility = Visibility.Collapsed;
        }

        private void PreviewFirst_Click(object sender, RoutedEventArgs e)
        {
            ContractorComboBox.Visibility = Visibility.Visible;
            EmployeeComboBox.Visibility = Visibility.Visible;
            CarOwnerBox.Visibility = Visibility.Visible;
            DatePicker.Visibility = Visibility.Visible;
            ReturnFirst.Visibility = Visibility.Visible;
            NextFirst.Visibility = Visibility.Visible;

            VehicleBox.Visibility = Visibility.Collapsed;
            ShipperBox.Visibility = Visibility.Collapsed;
            ConsignorBox.Visibility = Visibility.Collapsed;
            LoadingBox.Visibility = Visibility.Collapsed;
            PreviewFirst.Visibility = Visibility.Collapsed;
            NextSecond.Visibility = Visibility.Collapsed;
        }

        private void NextSecond_Click(object sender, RoutedEventArgs e)
        {
            UnloadingBox.Visibility = Visibility.Visible;
            TreatyBox.Visibility = Visibility.Visible;
            VehicleNumberBox.Visibility = Visibility.Visible;
            GuideBox.Visibility = Visibility.Visible;
            PreviewSecond.Visibility = Visibility.Visible;
            NextThird.Visibility = Visibility.Visible;

            VehicleBox.Visibility = Visibility.Collapsed;
            ShipperBox.Visibility = Visibility.Collapsed;
            ConsignorBox.Visibility = Visibility.Collapsed;
            LoadingBox.Visibility = Visibility.Collapsed;
            PreviewFirst.Visibility = Visibility.Collapsed;
            NextFirst.Visibility = Visibility.Collapsed;
        }

        private void PreviewSecond_Click(object sender, RoutedEventArgs e)
        {
            VehicleBox.Visibility = Visibility.Visible;
            ShipperBox.Visibility = Visibility.Visible;
            ConsignorBox.Visibility = Visibility.Visible;
            LoadingBox.Visibility = Visibility.Visible;
            PreviewFirst.Visibility = Visibility.Visible;
            NextFirst.Visibility = Visibility.Visible;

            UnloadingBox.Visibility = Visibility.Collapsed;
            TreatyBox.Visibility = Visibility.Collapsed;
            VehicleNumberBox.Visibility = Visibility.Collapsed;
            GuideBox.Visibility = Visibility.Collapsed;
            PreviewSecond.Visibility = Visibility.Collapsed;
            NextThird.Visibility = Visibility.Collapsed;
        }

        private void PreviewThird_Click(object sender, RoutedEventArgs e)
        {
            UnloadingBox.Visibility = Visibility.Visible;
            TreatyBox.Visibility = Visibility.Visible;
            VehicleNumberBox.Visibility = Visibility.Visible;
            GuideBox.Visibility = Visibility.Visible;
            PreviewSecond.Visibility = Visibility.Visible;
            NextThird.Visibility = Visibility.Visible;

            RouteNumber.Visibility = Visibility.Collapsed;
            Confirm.Visibility = Visibility.Collapsed;
            PreviewThird.Visibility = Visibility.Collapsed;
        }

        private void NextThird_Click(object sender, RoutedEventArgs e)
        {
            RouteNumber.Visibility = Visibility.Visible;
            Confirm.Visibility = Visibility.Visible;
            PreviewThird.Visibility = Visibility.Visible;

            UnloadingBox.Visibility = Visibility.Collapsed;
            TreatyBox.Visibility = Visibility.Collapsed;
            VehicleNumberBox.Visibility = Visibility.Collapsed;
            GuideBox.Visibility = Visibility.Collapsed;
            PreviewSecond.Visibility = Visibility.Collapsed;
            NextThird.Visibility = Visibility.Collapsed;
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            /*ComboBoxEntity contractor = (ComboBoxEntity)ContractorComboBox.SelectedItem;
            ComboBoxEntity employee = (ComboBoxEntity)EmployeeComboBox.SelectedItem;

            string carOwner = CarOwnerBox.Text;
            string vehicle = VehicleBox.Text;
            string shipper = ShipperBox.Text;
            string consignor = ConsignorBox.Text;
            string loadingPoint = LoadingBox.Text;
            string unloadingPoint = UnloadingBox.Text;
            string treaty = TreatyBox.Text;
            string vehicleNumber = VehicleNumberBox.Text;
            string guide = GuideBox.Text;
            string routeNumber = RouteNumber.Text;

            try
            {
                string date = DatePicker.SelectedDate.Value.ToString("yyyy-MM-dd");

                if (contractor != null && employee != null)
                {
                    WaybillEntity waybill = new WaybillEntity(waybillId, contractor.id, employee.id, carOwner, date, vehicle, shipper, consignor, loadingPoint, unloadingPoint, treaty, vehicleNumber, guide, routeNumber);
                    
                    if (validation.isWaybillValid(waybill))
                    {
                        waybillCrud.update(waybill);
                        waybillCrud.fetchToGrid(dataGrid);

                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Укажите все данные!");
                }
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("Выберите дату!");
            }*/
        }
    }
}