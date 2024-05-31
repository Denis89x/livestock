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
            RecordOutView recordOut = new RecordOutView();
            recordOut.ShowDialog();
        }

        private void StatementButton_Click(object sender, RoutedEventArgs e)
        {
            StatementOutView statementOutView = new StatementOutView();
            statementOutView.ShowDialog();
        }

        private void WaybillButton_Click(object sender, RoutedEventArgs e)
        {
            WaybillOutView waybillOutView = new WaybillOutView();
            waybillOutView.ShowDialog();
        }

        private void DeliveryNoteButton_Click(object sender, RoutedEventArgs e)
        {
            TotalOutView totalOutView = new TotalOutView();
            totalOutView.ShowDialog();
        }
    }
}