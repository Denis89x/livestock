using System;
using System.Windows;
using Warehouse.Entity;
using Warehouse.Service;
using Warehouse.Storage;

namespace Warehouse.View.OutputDocument
{
    public partial class RecordCardView : Window
    {
        private ComboBoxRepo comboBoxRepo;
        private RecordCardOutput recordCardOutput;

        public RecordCardView()
        {
            InitializeComponent();

            comboBoxRepo = new ComboBoxRepoImpl();

            comboBoxRepo.insertRecordCardIntoComboBox(RecordCardCombo);

            FirstDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
            SecondDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxEntity recordCard = (ComboBoxEntity)RecordCardCombo.SelectedItem;

            try
            {
                if (recordCard != null)
                {
                    string firstDate = FirstDate.SelectedDate.Value.ToString("yyyy-MM-dd");
                    string secondDate = SecondDate.SelectedDate.Value.ToString("yyyy-MM-dd");

                    recordCardOutput = new RecordCardOutput();

                    recordCardOutput.generateWordDocument(recordCard.id, firstDate, secondDate);
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
