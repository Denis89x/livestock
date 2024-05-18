using System.ComponentModel.DataAnnotations;
using System.Windows;
using System.Windows.Controls;
using Warehouse.Entity;
using Warehouse.Storage;
using Warehouse.Validation;

namespace Warehouse.View.WaybillComposition
{
    public partial class CreateWaybillComposition : Window
    {
        private DataGrid dataGrid;
        private ComboBoxRepo comboBoxRepo;
        private CrudRepo<WaybillCompositionEntity> waybillCrud;
        private WaybillCompositionValidation validation;

        public CreateWaybillComposition(DataGrid dataGrid)
        {
            InitializeComponent();

            this.dataGrid = dataGrid;

            validation = new WaybillCompositionValidation();
            comboBoxRepo = new ComboBoxRepoImpl();
            waybillCrud = new WaybillCompositionRepoImpl();

            comboBoxRepo.insertWaybillIntoComboBox(WaybillComboBox);
            comboBoxRepo.insertProductsIntoComboBox(ProductComboBox);
        }

        private void ReturnFirst_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void NextFirst_Click(object sender, RoutedEventArgs e)
        {
            MassBox.Visibility = Visibility.Visible;
            AcidityBox.Visibility = Visibility.Visible;
            TemperatureBox.Visibility = Visibility.Visible;
            CleaningGroupBox.Visibility = Visibility.Visible;
            PreviewFirst.Visibility = Visibility.Visible;
            NextSecond.Visibility = Visibility.Visible;

            WaybillComboBox.Visibility = Visibility.Collapsed;
            ProductComboBox.Visibility = Visibility.Collapsed;
            WaybillTypeBox.Visibility = Visibility.Collapsed;
            FatBox.Visibility = Visibility.Collapsed;
            ReturnFirst.Visibility = Visibility.Collapsed;
            NextFirst.Visibility = Visibility.Collapsed;
        }

        private void PreviewFirst_Click(object sender, RoutedEventArgs e)
        {
            WaybillComboBox.Visibility = Visibility.Visible;
            ProductComboBox.Visibility = Visibility.Visible;
            WaybillTypeBox.Visibility = Visibility.Visible;
            FatBox.Visibility = Visibility.Visible;
            ReturnFirst.Visibility = Visibility.Visible;
            NextFirst.Visibility = Visibility.Visible;

            MassBox.Visibility = Visibility.Collapsed;
            AcidityBox.Visibility = Visibility.Collapsed;
            TemperatureBox.Visibility = Visibility.Collapsed;
            CleaningGroupBox.Visibility = Visibility.Collapsed;
            PreviewFirst.Visibility = Visibility.Collapsed;
            NextSecond.Visibility = Visibility.Collapsed;
        }

        private void NextSecond_Click(object sender, RoutedEventArgs e)
        {
            DensityBox.Visibility = Visibility.Visible;
            SortBox.Visibility = Visibility.Visible;
            PackagingTypeBox.Visibility = Visibility.Visible;
            QuantityBox.Visibility = Visibility.Visible;
            PreviewSecond.Visibility = Visibility.Visible;
            NextThird.Visibility = Visibility.Visible;

            MassBox.Visibility = Visibility.Collapsed;
            AcidityBox.Visibility = Visibility.Collapsed;
            TemperatureBox.Visibility = Visibility.Collapsed;
            CleaningGroupBox.Visibility = Visibility.Collapsed;
            PreviewFirst.Visibility = Visibility.Collapsed;
            NextFirst.Visibility = Visibility.Collapsed;
        }

        private void PreviewSecond_Click(object sender, RoutedEventArgs e)
        {
            MassBox.Visibility = Visibility.Visible;
            AcidityBox.Visibility = Visibility.Visible;
            TemperatureBox.Visibility = Visibility.Visible;
            CleaningGroupBox.Visibility = Visibility.Visible;
            PreviewFirst.Visibility = Visibility.Visible;
            NextFirst.Visibility = Visibility.Visible;

            DensityBox.Visibility = Visibility.Collapsed;
            SortBox.Visibility = Visibility.Collapsed;
            PackagingTypeBox.Visibility = Visibility.Collapsed;
            QuantityBox.Visibility = Visibility.Collapsed;
            PreviewSecond.Visibility = Visibility.Collapsed;
            NextThird.Visibility = Visibility.Collapsed;
        }


        private void PreviewThird_Click(object sender, RoutedEventArgs e)
        {
            DensityBox.Visibility = Visibility.Visible;
            SortBox.Visibility = Visibility.Visible;
            PackagingTypeBox.Visibility = Visibility.Visible;
            QuantityBox.Visibility = Visibility.Visible;
            PreviewSecond.Visibility = Visibility.Visible;
            NextThird.Visibility = Visibility.Visible;

            BruttoBox.Visibility = Visibility.Collapsed;
            TaraBox.Visibility = Visibility.Collapsed;
            NettoBox.Visibility = Visibility.Collapsed;
            GradeBox.Visibility = Visibility.Collapsed;
            Confirm.Visibility = Visibility.Collapsed;
            PreviewThird.Visibility = Visibility.Collapsed;
        }

        private void NextThird_Click(object sender, RoutedEventArgs e)
        {
            BruttoBox.Visibility = Visibility.Visible;
            TaraBox.Visibility = Visibility.Visible;
            NettoBox.Visibility = Visibility.Visible;
            GradeBox.Visibility = Visibility.Visible;
            Confirm.Visibility = Visibility.Visible;
            PreviewThird.Visibility = Visibility.Visible;

            DensityBox.Visibility = Visibility.Collapsed;
            SortBox.Visibility = Visibility.Collapsed;
            PackagingTypeBox.Visibility = Visibility.Collapsed;
            QuantityBox.Visibility = Visibility.Collapsed;
            PreviewSecond.Visibility = Visibility.Collapsed;
            NextThird.Visibility = Visibility.Collapsed;
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxEntity waybill = (ComboBoxEntity)WaybillComboBox.SelectedItem;
            ComboBoxEntity product = (ComboBoxEntity)ProductComboBox.SelectedItem;

            string waybillType = "accepted";
            string fat = FatBox.Text;
            string mass = MassBox.Text;
            string acidity = AcidityBox.Text;
            string temperature = TemperatureBox.Text;
            string cleaningGroup = CleaningGroupBox.Text;
            string density = DensityBox.Text;
            string sort = SortBox.Text;
            string packagingType = PackagingTypeBox.Text;
            string quantity = QuantityBox.Text;
            string brutto = BruttoBox.Text;
            string tara = TaraBox.Text;
            string netto = NettoBox.Text;
            string grade = GradeBox.Text;

            if (waybill != null && product != null)
            {
                WaybillCompositionEntity waybillComposition = new WaybillCompositionEntity(waybill.id, product.id, waybillType, fat, mass, acidity, temperature, cleaningGroup, density, sort, packagingType, quantity, brutto, tara, netto, grade);

                if (validation.isWaybillCompositionValid(waybillComposition))
                {
                    waybillCrud.create(waybillComposition);
                    waybillCrud.fetchToGrid(dataGrid);

                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Укажите все поля!");
            }  
        }
    }
}