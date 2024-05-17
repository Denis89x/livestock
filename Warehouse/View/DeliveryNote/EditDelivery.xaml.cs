﻿using System.Windows;
using System.Windows.Controls;
using Warehouse.Entity;
using Warehouse.storage1;
using Warehouse.Validation;

namespace Warehouse.View.DeliveryNote
{
    public partial class EditDelivery : Window
    {
        long deliveryId;
        DataGrid dataGrid;
        ComboBoxRepo comboBoxRepo;
        DeliveryNoteRepo deliveryNoteRepo;
        DeliveryNoteValidation validation;

        public EditDelivery(long deliveryId, string division, string date, string assignment, DataGrid dataGrid)
        {
            InitializeComponent();

            this.dataGrid = dataGrid;
            this.deliveryId = deliveryId;

            validation = new DeliveryNoteValidation();
            comboBoxRepo = new ComboBoxRepoImpl();
            deliveryNoteRepo = new DeliveryNoteRepoImpl();

            DatePicker.Text = date;
            AssignmentBox.Text = assignment;

            comboBoxRepo.insertDivisionsIntoComboBox(DivisionComboBox);
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxEntity division = (ComboBoxEntity)DivisionComboBox.SelectedItem;

            string date = DatePicker.SelectedDate.Value.ToString("yyyy-MM-dd");
            string assignment = AssignmentBox.Text;

            if (division != null)
            {
                DeliveryNoteEntity deliveryNote = new DeliveryNoteEntity(deliveryId, division.id, date, assignment);

                if (validation.isDeliveryNoteValid(deliveryNote))
                {
                    deliveryNoteRepo.updateDeliveryNote(deliveryNote);
                    deliveryNoteRepo.fetchDeliveryNoteToGrid(dataGrid);

                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Выберите подразделение!");
            }
        }
    }
}