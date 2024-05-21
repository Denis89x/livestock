using System;
using System.Windows;
using System.Windows.Controls;
using Warehouse.Entity;
using Warehouse.Storage;
using Warehouse.Validation;

namespace Warehouse.View.RecordCard
{
    public partial class EditRecordCard : Window
    {
        private long recordCardId;
        private DataGrid dataGrid;
        private ComboBoxRepo comboBoxRepo;
        private CrudRepo<RecordCardEntity> recordCrud;
        private RecordCardValidation recordCardValidation;

        public EditRecordCard(long recordCardId, string product, string division, string employee, string date, string quantity, string morning, string midday, string evening, DataGrid dataGrid)
        {
            InitializeComponent();

            this.recordCardId = recordCardId;
            this.dataGrid = dataGrid;

            comboBoxRepo = new ComboBoxRepoImpl();
            recordCrud = new RecordCardRepoImpl();
            recordCardValidation = new RecordCardValidation();

            DatePicker.Text = DateTime.Today.ToString("yyyy-MM-dd");

            comboBoxRepo.insertDivisionsIntoComboBox(DivisionCombo);
            comboBoxRepo.insertEmployeesIntoComboBox(EmployeeCombo);

            ProductCombo.Items.Add(product);
            ProductCombo.SelectedIndex = 0;
            QuantityBox.Text = quantity;
            MorningBox.Text = morning;
            MiddayBox.Text = midday;
            EveningBox.Text = evening;

            DatePicker.Text = date;
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxEntity division = (ComboBoxEntity)DivisionCombo.SelectedItem;
            ComboBoxEntity employee = (ComboBoxEntity)EmployeeCombo.SelectedItem;

            string quantity = QuantityBox.Text;
            string morning = MorningBox.Text;
            string midday = MiddayBox.Text;
            string evening = EveningBox.Text;

            try
            {
                if (division != null && employee != null)
                {
                    string date = DatePicker.SelectedDate.Value.ToString("yyyy-MM-dd");

                    RecordCardEntity recordCard = new RecordCardEntity(recordCardId, 0, division.id, employee.id, date, quantity, morning, midday, evening);
                    if (recordCardValidation.isRecordCardValid(recordCard))
                    {
                        recordCrud.update(recordCard);
                        recordCrud.fetchToGrid(dataGrid);

                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Заполните все поля!");
                }
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("Выберите дату!");
            }
        }
    }
}