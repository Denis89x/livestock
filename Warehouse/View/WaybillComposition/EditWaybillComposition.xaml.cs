using System.Windows;
using System.Windows.Controls;
using Warehouse.Entity;
using Warehouse.storage1;

namespace Warehouse.View.WaybillComposition
{

    public partial class EditWaybillComposition : Window
    {
        DataGrid dataGrid;
        long waybillCompositionId;
        ComboBoxRepo comboBoxRepo;
        WaybillCompositionRepo waybillCompositionRepo;

        public EditWaybillComposition(long waybillCompositionId, string waybill, string product,
            string fat, string mass, string acidity, string temperature, string cleaningGroup,
            string density, string sort, string packagingType, string quantity, string brutto,
            string tara, string netto, string grade, DataGrid dataGrid)
        {
            InitializeComponent();

            this.waybillCompositionId = waybillCompositionId;
            this.dataGrid = dataGrid;

            WaybillComboBox.Items.Add(waybill);
            WaybillComboBox.SelectedIndex = 0;

            comboBoxRepo = new ComboBoxRepoImpl();
            waybillCompositionRepo = new WaybillCompositionRepoImpl();

            comboBoxRepo.insertProductsIntoComboBox(ProductComboBox);

            FatBox.Text = fat;
            MassBox.Text = mass;
            AcidityBox.Text = acidity;
            TemperatureBox.Text = temperature;
            CleaningGroupBox.Text = cleaningGroup;
            DensityBox.Text = density;
            SortBox.Text = sort;
            PackagingTypeBox.Text = packagingType;
            QuantityBox.Text = quantity;
            BruttoBox.Text = brutto;
            TaraBox.Text = tara;
            NettoBox.Text = netto;
            GradeBox.Text = grade;
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

            WaybillCompositionEntity waybillComposition = new WaybillCompositionEntity(waybillCompositionId, waybill.id, product.id, waybillType, fat, mass, acidity, temperature, cleaningGroup, density, sort, packagingType, quantity, brutto, tara, netto, grade);

            waybillCompositionRepo.updateWaybillComposition(waybillComposition);
            waybillCompositionRepo.fetchWaybillCompositionToGrid(dataGrid);

            this.Close();
        }
    }
}
