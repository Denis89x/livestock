using System;
using System.Windows;
using System.Windows.Controls;
using Warehouse.Entity;
using Warehouse.storage1;
using Warehouse.Validation;

namespace Warehouse.View.RecordCardComposition
{
    public partial class CreateRecordCardComposition : Window
    {
        private DataGrid dataGrid;
        private ComboBoxRepo comboBoxRepo;
        private CrudRepo<RecordCardCompositionEntity> recordCrud;
        private RecordCardCompositionValidation validation;

        public CreateRecordCardComposition(DataGrid dataGrid)
        {
            InitializeComponent();

            this.dataGrid = dataGrid;

            comboBoxRepo = new ComboBoxRepoImpl();
            recordCrud = new RecordCardCompositionRepoImpl();
            validation = new RecordCardCompositionValidation();

            comboBoxRepo.insertRecordCardIntoComboBox(RecordCardCombo);

            DatePicker.Text = DateTime.Today.ToString("yyyy-MM-dd");
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void NextFirst_Click(object sender, RoutedEventArgs e)
        {
            MilkMiddayBox.Visibility = Visibility.Visible;
            MilkEveningBox.Visibility = Visibility.Visible;
            Confirm.Visibility = Visibility.Visible;
            ReturnFirst.Visibility = Visibility.Visible;

            RecordCardCombo.Visibility = Visibility.Collapsed;
            DatePicker.Visibility = Visibility.Collapsed;
            CowCombo.Visibility = Visibility.Collapsed;
            MilkMorningBox.Visibility = Visibility.Collapsed;
            Close.Visibility = Visibility.Collapsed;
            NextFirst.Visibility = Visibility.Collapsed;
        }

        private void ReturnFirst_Click(object sender, RoutedEventArgs e)
        {
            RecordCardCombo.Visibility = Visibility.Visible;
            DatePicker.Visibility = Visibility.Visible;
            CowCombo.Visibility = Visibility.Visible;
            MilkMorningBox.Visibility = Visibility.Visible;
            Close.Visibility = Visibility.Visible;
            NextFirst.Visibility = Visibility.Visible;

            MilkMiddayBox.Visibility = Visibility.Collapsed;
            MilkEveningBox.Visibility = Visibility.Collapsed;
            Confirm.Visibility = Visibility.Collapsed;
            ReturnFirst.Visibility = Visibility.Collapsed;
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxEntity recordCard = (ComboBoxEntity)RecordCardCombo.SelectedItem;

            string cowQuantity = CowCombo.Text;
            string milkMorning = MilkMorningBox.Text;
            string milkMidday = MilkMiddayBox.Text;
            string milkEvening = MilkEveningBox.Text;

            try
            {
                string date = DatePicker.SelectedDate.Value.ToString("yyyy-MM-dd");

                if (recordCard != null)
                {
                    RecordCardCompositionEntity recordCardComposition = new RecordCardCompositionEntity(recordCard.id, date, cowQuantity, milkMorning, milkMidday, milkEvening);

                    if (validation.isRecordCardCompositionValid(recordCardComposition))
                    {
                        recordCrud.create(recordCardComposition);
                        recordCrud.fetchToGrid(dataGrid);

                        this.Close();
                    }
                } else
                {
                    MessageBox.Show("Выберите карточку!");
                }
                
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("Выберите дату!");
            }
        }
    }
}