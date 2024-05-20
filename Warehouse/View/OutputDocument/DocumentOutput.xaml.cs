using System.Windows;
using Warehouse.View.OutputDocument;

namespace Warehouse.View
{
    public partial class DocumentOutput : Window
    {
        public DocumentOutput()
        {
            InitializeComponent();
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void RecordCardButton_Click(object sender, RoutedEventArgs e)
        {
            RecordCardView record = new RecordCardView();
            record.ShowDialog();
        }

        private void StatementButton_Click(object sender, RoutedEventArgs e)
        {
            StatementView statement = new StatementView();
            statement.ShowDialog();
        }

        private void WaybillButton_Click(object sender, RoutedEventArgs e)
        {
            WaybillView waybill = new WaybillView();
            waybill.ShowDialog();
        }

        private void DeliveryNoteButton_Click(object sender, RoutedEventArgs e)
        {
            DeliveryNoteView deliveryNoteView = new DeliveryNoteView();
            deliveryNoteView.ShowDialog();
        }
    }
}