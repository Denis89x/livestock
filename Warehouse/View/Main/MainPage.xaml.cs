using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using Warehouse.Entity;
using Warehouse.Service;
using Warehouse.Storage;
using Warehouse.View.Auth;
using Warehouse.View.Cattle;
using Warehouse.View.Contractor;
using Warehouse.View.DeliveryNote;
using Warehouse.View.Division;
using Warehouse.View.Employee;
using Warehouse.View.OutputDocument;
using Warehouse.View.Product;
using Warehouse.View.RecordCard;
using Warehouse.View.Statement;
using Warehouse.View.Waybill;

namespace Warehouse.View.Main
{
    public partial class MainPage : Window
    {
        private GridUtility gridUtility;

        string userRole;

        private WaybillDelete waybillDelete;
        private DeliveryDelete deliveryDelete;

        private CrudRepo<CattleEntity> cattleCrud;
        private CrudRepo<ProductEntity> productCrud;
        private CrudRepo<WaybillEntity> waybillCrud;
        private CrudRepo<DivisionEntity> divisionCrud;
        private CrudRepo<EmployeeEntity> employeeCrud;
        private CrudRepo<StatementEntity> statementCrud;
        private CrudRepo<ContractorEntity> contractorCrud;
        private CrudRepo<RecordCardEntity> recordCardCrud;
        private CrudRepo<DeliveryNoteEntity> deliveryNoteCrud;

        public MainPage(string userRole)
        {
            InitializeComponent();

            gridUtility = new GridUtilityImpl();

            this.userRole = userRole;

            cattleCrud = new CattleRepoImpl();
            productCrud = new ProductRepoImpl();
            waybillCrud = new WaybillRepoImpl();
            divisionCrud = new DivisionRepoImpl();
            employeeCrud = new EmployeeRepoImpl();
            statementCrud = new StatementRepoImpl();
            contractorCrud = new ContractorRepoImpl();
            recordCardCrud = new RecordCardRepoImpl();
            deliveryNoteCrud = new DeliveryNoteRepoImpl();

            if (userRole.Equals("ROLE_ADMIN"))
            {
                AdminRegistration.Visibility = Visibility.Visible;
            } else if (userRole.Equals("ROLE_ACCOUNTANT"))
            {
                AddContractor.Visibility = Visibility.Collapsed;
                EditContractor.Visibility = Visibility.Collapsed;
                DeleteContractor.Visibility = Visibility.Collapsed;

                AddEmployee.Visibility = Visibility.Collapsed;
                EditEmployee.Visibility = Visibility.Collapsed;
                DeleteEmployee.Visibility = Visibility.Collapsed;

                AddCattle.Visibility = Visibility.Collapsed;
                EditCattle.Visibility = Visibility.Collapsed;
                DeleteCattle.Visibility = Visibility.Collapsed;

                AddDivision.Visibility = Visibility.Collapsed;
                EditDivision.Visibility = Visibility.Collapsed;
                DeleteDivision.Visibility = Visibility.Collapsed;

                AddProduct.Visibility = Visibility.Collapsed;
                EditProduct.Visibility = Visibility.Collapsed;
                DeleteProduct.Visibility = Visibility.Collapsed;

                AddStatement.Visibility = Visibility.Collapsed;
                EditStatement.Visibility = Visibility.Collapsed;
                DeleteStatement.Visibility = Visibility.Collapsed;

                AddWaybill.Visibility = Visibility.Collapsed;
                EditWaybill.Visibility = Visibility.Collapsed;
                DeleteWaybill.Visibility = Visibility.Collapsed;

                AddDeliveryNote.Visibility = Visibility.Collapsed;
                EditDeliveryNote.Visibility = Visibility.Collapsed;
                DeleteDeliveryNote.Visibility = Visibility.Collapsed;

                AddRecordCard.Visibility = Visibility.Collapsed;
                EditRecordCard.Visibility = Visibility.Collapsed;
                DeleteRecordCard.Visibility = Visibility.Collapsed;

                AddContractorBtn.Visibility = Visibility.Collapsed;
                EditContractorBtn.Visibility = Visibility.Collapsed;
                DeleteContractorBtn.Visibility = Visibility.Collapsed;
                AddEmployeeBtn.Visibility = Visibility.Collapsed;
                EditEmployeeBtn.Visibility = Visibility.Collapsed;
                DeleteEmployeeBtn.Visibility = Visibility.Collapsed;
                AddDivisionBtn.Visibility = Visibility.Collapsed;
                EditDivisionBtn.Visibility = Visibility.Collapsed;
                DeleteDivisionBtn.Visibility = Visibility.Collapsed;
                AddCattleBtn.Visibility = Visibility.Collapsed;
                EditCattleBtn.Visibility = Visibility.Collapsed;
                DeleteCattleBtn.Visibility = Visibility.Collapsed;
                AddProductBtn.Visibility = Visibility.Collapsed;
                EditProductBtn.Visibility = Visibility.Collapsed;
                DeleteProductBtn.Visibility = Visibility.Collapsed;
                AddStatementBtn.Visibility = Visibility.Collapsed;
                EditStatementBtn.Visibility = Visibility.Collapsed;
                DeleteStatementBtn.Visibility = Visibility.Collapsed;
                AddWaybillBtn.Visibility = Visibility.Collapsed;
                EditWaybillBtn.Visibility = Visibility.Collapsed;
                DeleteWaybillBtn.Visibility = Visibility.Collapsed;
                AddDeliveryBtn.Visibility = Visibility.Collapsed;
                EditDeliveryBtn.Visibility = Visibility.Collapsed;
                DeleteDeliveryBtn.Visibility = Visibility.Collapsed;
                AddRecordBtn.Visibility = Visibility.Collapsed;
                EditRecordBtn.Visibility = Visibility.Collapsed;
                DeleteRecordBtn.Visibility = Visibility.Collapsed;
                WordDeliveryBtn.Visibility = Visibility.Collapsed;
                WordWaybillBtn.Visibility = Visibility.Collapsed;
            }

            string userPosition = "";

            if (userRole.Equals("ROLE_ADMIN"))
            {
                userPosition = "Администратор";
            } else if (userRole.Equals("ROLE_ACCOUNTANT"))
            {
                userPosition = "Бухгалтер";
            } else if (userRole.Equals("ROLE_OWNER"))
            {
                userPosition = "Заведующий фермой";
            }

            contractorCrud.fetchToGrid(ContractorGrid);

            UserInfoLabel.Content = $"Должность: {userPosition}";
        }

        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
            ButtonCloseMenu.Visibility = Visibility.Visible;
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Visible;
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Contractor_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            contractorCrud.fetchToGrid(ContractorGrid);

            GridName.Content = "Справочник: Контрагенты";

            ContractorGrid.Visibility = Visibility.Visible;

            if (!userRole.Equals("ROLE_ACCOUNTANT"))
            {
                AddContractorBtn.Visibility = Visibility.Visible;
                EditContractorBtn.Visibility = Visibility.Visible;
                DeleteContractorBtn.Visibility = Visibility.Visible;
            }

            EmployeeGrid.Visibility = Visibility.Collapsed;
            DivisionGrid.Visibility = Visibility.Collapsed;
            CattleGrid.Visibility = Visibility.Collapsed;
            ProductGrid.Visibility = Visibility.Collapsed;
            StatementGrid.Visibility = Visibility.Collapsed;
            WaybillGrid.Visibility = Visibility.Collapsed;
            DeliveryNoteGrid.Visibility = Visibility.Collapsed;
            RecordCardGrid.Visibility = Visibility.Collapsed;

            AddEmployeeBtn.Visibility = Visibility.Collapsed;
            EditEmployeeBtn.Visibility = Visibility.Collapsed;
            DeleteEmployeeBtn.Visibility = Visibility.Collapsed;
            AddDivisionBtn.Visibility = Visibility.Collapsed;
            EditDivisionBtn.Visibility = Visibility.Collapsed;
            DeleteDivisionBtn.Visibility = Visibility.Collapsed;
            AddCattleBtn.Visibility = Visibility.Collapsed;
            EditCattleBtn.Visibility = Visibility.Collapsed;
            DeleteCattleBtn.Visibility = Visibility.Collapsed;
            AddProductBtn.Visibility = Visibility.Collapsed;
            EditProductBtn.Visibility = Visibility.Collapsed;
            DeleteProductBtn.Visibility = Visibility.Collapsed;
            AddStatementBtn.Visibility = Visibility.Collapsed;
            EditStatementBtn.Visibility = Visibility.Collapsed;
            DeleteStatementBtn.Visibility = Visibility.Collapsed;
            AddWaybillBtn.Visibility = Visibility.Collapsed;
            EditWaybillBtn.Visibility = Visibility.Collapsed;
            DeleteWaybillBtn.Visibility = Visibility.Collapsed;
            AddDeliveryBtn.Visibility = Visibility.Collapsed;
            EditDeliveryBtn.Visibility = Visibility.Collapsed;
            DeleteDeliveryBtn.Visibility = Visibility.Collapsed;
            AddRecordBtn.Visibility = Visibility.Collapsed;
            EditRecordBtn.Visibility = Visibility.Collapsed;
            DeleteRecordBtn.Visibility = Visibility.Collapsed;
            WordDeliveryBtn.Visibility = Visibility.Collapsed;
            WordWaybillBtn.Visibility = Visibility.Collapsed;
        }

        private void Employee_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            employeeCrud.fetchToGrid(EmployeeGrid);

            GridName.Content = "Справочник: Сотрудники";

            EmployeeGrid.Visibility = Visibility.Visible;

            if (!userRole.Equals("ROLE_ACCOUNTANT"))
            {
                AddEmployeeBtn.Visibility = Visibility.Visible;
                EditEmployeeBtn.Visibility = Visibility.Visible;
                DeleteEmployeeBtn.Visibility = Visibility.Visible;
            }

            ContractorGrid.Visibility = Visibility.Collapsed;
            DivisionGrid.Visibility = Visibility.Collapsed;
            CattleGrid.Visibility = Visibility.Collapsed;
            ProductGrid.Visibility = Visibility.Collapsed;
            StatementGrid.Visibility = Visibility.Collapsed;
            WaybillGrid.Visibility = Visibility.Collapsed;
            DeliveryNoteGrid.Visibility = Visibility.Collapsed;
            RecordCardGrid.Visibility = Visibility.Collapsed;

            AddContractorBtn.Visibility = Visibility.Collapsed;
            EditContractorBtn.Visibility = Visibility.Collapsed;
            DeleteContractorBtn.Visibility = Visibility.Collapsed;
            AddDivisionBtn.Visibility = Visibility.Collapsed;
            EditDivisionBtn.Visibility = Visibility.Collapsed;
            DeleteDivisionBtn.Visibility = Visibility.Collapsed;
            AddCattleBtn.Visibility = Visibility.Collapsed;
            EditCattleBtn.Visibility = Visibility.Collapsed;
            DeleteCattleBtn.Visibility = Visibility.Collapsed;
            AddProductBtn.Visibility = Visibility.Collapsed;
            EditProductBtn.Visibility = Visibility.Collapsed;
            DeleteProductBtn.Visibility = Visibility.Collapsed;
            AddStatementBtn.Visibility = Visibility.Collapsed;
            EditStatementBtn.Visibility = Visibility.Collapsed;
            DeleteStatementBtn.Visibility = Visibility.Collapsed;
            AddWaybillBtn.Visibility = Visibility.Collapsed;
            EditWaybillBtn.Visibility = Visibility.Collapsed;
            DeleteWaybillBtn.Visibility = Visibility.Collapsed;
            AddDeliveryBtn.Visibility = Visibility.Collapsed;
            EditDeliveryBtn.Visibility = Visibility.Collapsed;
            DeleteDeliveryBtn.Visibility = Visibility.Collapsed;
            AddRecordBtn.Visibility = Visibility.Collapsed;
            EditRecordBtn.Visibility = Visibility.Collapsed;
            DeleteRecordBtn.Visibility = Visibility.Collapsed;
            WordDeliveryBtn.Visibility = Visibility.Collapsed;
            WordWaybillBtn.Visibility = Visibility.Collapsed;
        }

        private void Division_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            divisionCrud.fetchToGrid(DivisionGrid);

            GridName.Content = "Справочник: Подразделение";

            DivisionGrid.Visibility = Visibility.Visible;

            if (!userRole.Equals("ROLE_ACCOUNTANT"))
            {
                AddDivisionBtn.Visibility = Visibility.Visible;
                EditDivisionBtn.Visibility = Visibility.Visible;
                DeleteDivisionBtn.Visibility = Visibility.Visible;
            }

            ContractorGrid.Visibility = Visibility.Collapsed;
            EmployeeGrid.Visibility = Visibility.Collapsed;
            CattleGrid.Visibility = Visibility.Collapsed;
            ProductGrid.Visibility = Visibility.Collapsed;
            StatementGrid.Visibility = Visibility.Collapsed;
            WaybillGrid.Visibility = Visibility.Collapsed;
            DeliveryNoteGrid.Visibility = Visibility.Collapsed;
            RecordCardGrid.Visibility = Visibility.Collapsed;

            AddContractorBtn.Visibility = Visibility.Collapsed;
            EditContractorBtn.Visibility = Visibility.Collapsed;
            DeleteContractorBtn.Visibility = Visibility.Collapsed;
            AddEmployeeBtn.Visibility = Visibility.Collapsed;
            EditEmployeeBtn.Visibility = Visibility.Collapsed;
            DeleteEmployeeBtn.Visibility = Visibility.Collapsed;
            AddCattleBtn.Visibility = Visibility.Collapsed;
            EditCattleBtn.Visibility = Visibility.Collapsed;
            DeleteCattleBtn.Visibility = Visibility.Collapsed;
            AddProductBtn.Visibility = Visibility.Collapsed;
            EditProductBtn.Visibility = Visibility.Collapsed;
            DeleteProductBtn.Visibility = Visibility.Collapsed;
            AddStatementBtn.Visibility = Visibility.Collapsed;
            EditStatementBtn.Visibility = Visibility.Collapsed;
            DeleteStatementBtn.Visibility = Visibility.Collapsed;
            AddWaybillBtn.Visibility = Visibility.Collapsed;
            EditWaybillBtn.Visibility = Visibility.Collapsed;
            DeleteWaybillBtn.Visibility = Visibility.Collapsed;
            AddDeliveryBtn.Visibility = Visibility.Collapsed;
            EditDeliveryBtn.Visibility = Visibility.Collapsed;
            DeleteDeliveryBtn.Visibility = Visibility.Collapsed;
            AddRecordBtn.Visibility = Visibility.Collapsed;
            EditRecordBtn.Visibility = Visibility.Collapsed;
            DeleteRecordBtn.Visibility = Visibility.Collapsed;
            WordDeliveryBtn.Visibility = Visibility.Collapsed;
            WordWaybillBtn.Visibility = Visibility.Collapsed;
        }

        private void Cettle_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            cattleCrud.fetchToGrid(CattleGrid);

            GridName.Content = "Справочник: Группа животных";

            CattleGrid.Visibility = Visibility.Visible;

            if (!userRole.Equals("ROLE_ACCOUNTANT"))
            {
                AddCattleBtn.Visibility = Visibility.Visible;
                EditCattleBtn.Visibility = Visibility.Visible;
                DeleteCattleBtn.Visibility = Visibility.Visible;
            }

            ContractorGrid.Visibility = Visibility.Collapsed;
            EmployeeGrid.Visibility = Visibility.Collapsed;
            DivisionGrid.Visibility = Visibility.Collapsed;
            ProductGrid.Visibility = Visibility.Collapsed;
            StatementGrid.Visibility = Visibility.Collapsed;
            WaybillGrid.Visibility = Visibility.Collapsed;
            DeliveryNoteGrid.Visibility = Visibility.Collapsed;
            RecordCardGrid.Visibility = Visibility.Collapsed;

            AddContractorBtn.Visibility = Visibility.Collapsed;
            EditContractorBtn.Visibility = Visibility.Collapsed;
            DeleteContractorBtn.Visibility = Visibility.Collapsed;
            AddEmployeeBtn.Visibility = Visibility.Collapsed;
            EditEmployeeBtn.Visibility = Visibility.Collapsed;
            DeleteEmployeeBtn.Visibility = Visibility.Collapsed;
            AddDivisionBtn.Visibility = Visibility.Collapsed;
            EditDivisionBtn.Visibility = Visibility.Collapsed;
            DeleteDivisionBtn.Visibility = Visibility.Collapsed;
            AddProductBtn.Visibility = Visibility.Collapsed;
            EditProductBtn.Visibility = Visibility.Collapsed;
            DeleteProductBtn.Visibility = Visibility.Collapsed;
            AddStatementBtn.Visibility = Visibility.Collapsed;
            EditStatementBtn.Visibility = Visibility.Collapsed;
            DeleteStatementBtn.Visibility = Visibility.Collapsed;
            AddWaybillBtn.Visibility = Visibility.Collapsed;
            EditWaybillBtn.Visibility = Visibility.Collapsed;
            DeleteWaybillBtn.Visibility = Visibility.Collapsed;
            AddDeliveryBtn.Visibility = Visibility.Collapsed;
            EditDeliveryBtn.Visibility = Visibility.Collapsed;
            DeleteDeliveryBtn.Visibility = Visibility.Collapsed;
            AddRecordBtn.Visibility = Visibility.Collapsed;
            EditRecordBtn.Visibility = Visibility.Collapsed;
            DeleteRecordBtn.Visibility = Visibility.Collapsed;
            WordDeliveryBtn.Visibility = Visibility.Collapsed;
            WordWaybillBtn.Visibility = Visibility.Collapsed;
        }

        private void Product_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            productCrud.fetchToGrid(ProductGrid);

            GridName.Content = "Справочник: Готовая продукция";

            ProductGrid.Visibility = Visibility.Visible;

            if (!userRole.Equals("ROLE_ACCOUNTANT"))
            {
                AddProductBtn.Visibility = Visibility.Visible;
                EditProductBtn.Visibility = Visibility.Visible;
                DeleteProductBtn.Visibility = Visibility.Visible;
            }

            ContractorGrid.Visibility = Visibility.Collapsed;
            EmployeeGrid.Visibility = Visibility.Collapsed;
            DivisionGrid.Visibility = Visibility.Collapsed;
            CattleGrid.Visibility = Visibility.Collapsed;
            StatementGrid.Visibility = Visibility.Collapsed;
            WaybillGrid.Visibility = Visibility.Collapsed;
            DeliveryNoteGrid.Visibility = Visibility.Collapsed;
            RecordCardGrid.Visibility = Visibility.Collapsed;

            AddContractorBtn.Visibility = Visibility.Collapsed;
            EditContractorBtn.Visibility = Visibility.Collapsed;
            DeleteContractorBtn.Visibility = Visibility.Collapsed;
            AddEmployeeBtn.Visibility = Visibility.Collapsed;
            EditEmployeeBtn.Visibility = Visibility.Collapsed;
            DeleteEmployeeBtn.Visibility = Visibility.Collapsed;
            AddDivisionBtn.Visibility = Visibility.Collapsed;
            EditDivisionBtn.Visibility = Visibility.Collapsed;
            DeleteDivisionBtn.Visibility = Visibility.Collapsed;
            AddCattleBtn.Visibility = Visibility.Collapsed;
            EditCattleBtn.Visibility = Visibility.Collapsed;
            DeleteCattleBtn.Visibility = Visibility.Collapsed;
            AddStatementBtn.Visibility = Visibility.Collapsed;
            EditStatementBtn.Visibility = Visibility.Collapsed;
            DeleteStatementBtn.Visibility = Visibility.Collapsed;
            AddWaybillBtn.Visibility = Visibility.Collapsed;
            EditWaybillBtn.Visibility = Visibility.Collapsed;
            DeleteWaybillBtn.Visibility = Visibility.Collapsed;
            AddDeliveryBtn.Visibility = Visibility.Collapsed;
            EditDeliveryBtn.Visibility = Visibility.Collapsed;
            DeleteDeliveryBtn.Visibility = Visibility.Collapsed;
            AddRecordBtn.Visibility = Visibility.Collapsed;
            EditRecordBtn.Visibility = Visibility.Collapsed;
            DeleteRecordBtn.Visibility = Visibility.Collapsed;
            WordDeliveryBtn.Visibility = Visibility.Collapsed;
            WordWaybillBtn.Visibility = Visibility.Collapsed;
        }

        private void Statement_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            statementCrud.fetchToGrid(StatementGrid);

            GridName.Content = "Ведомость учёта расхода кормов";

            StatementGrid.Visibility = Visibility.Visible;

            if (!userRole.Equals("ROLE_ACCOUNTANT"))
            {
                AddStatementBtn.Visibility = Visibility.Visible;
                EditStatementBtn.Visibility = Visibility.Visible;
                DeleteStatementBtn.Visibility = Visibility.Visible;
            }

            ContractorGrid.Visibility = Visibility.Collapsed;
            EmployeeGrid.Visibility = Visibility.Collapsed;
            DivisionGrid.Visibility = Visibility.Collapsed;
            CattleGrid.Visibility = Visibility.Collapsed;
            ProductGrid.Visibility = Visibility.Collapsed;
            WaybillGrid.Visibility = Visibility.Collapsed;
            DeliveryNoteGrid.Visibility = Visibility.Collapsed;
            RecordCardGrid.Visibility = Visibility.Collapsed;

            AddContractorBtn.Visibility = Visibility.Collapsed;
            EditContractorBtn.Visibility = Visibility.Collapsed;
            DeleteContractorBtn.Visibility = Visibility.Collapsed;
            AddEmployeeBtn.Visibility = Visibility.Collapsed;
            EditEmployeeBtn.Visibility = Visibility.Collapsed;
            DeleteEmployeeBtn.Visibility = Visibility.Collapsed;
            AddDivisionBtn.Visibility = Visibility.Collapsed;
            EditDivisionBtn.Visibility = Visibility.Collapsed;
            DeleteDivisionBtn.Visibility = Visibility.Collapsed;
            AddCattleBtn.Visibility = Visibility.Collapsed;
            EditCattleBtn.Visibility = Visibility.Collapsed;
            DeleteCattleBtn.Visibility = Visibility.Collapsed;
            AddProductBtn.Visibility = Visibility.Collapsed;
            EditProductBtn.Visibility = Visibility.Collapsed;
            DeleteProductBtn.Visibility = Visibility.Collapsed;
            AddWaybillBtn.Visibility = Visibility.Collapsed;
            EditWaybillBtn.Visibility = Visibility.Collapsed;
            DeleteWaybillBtn.Visibility = Visibility.Collapsed;
            AddDeliveryBtn.Visibility = Visibility.Collapsed;
            EditDeliveryBtn.Visibility = Visibility.Collapsed;
            DeleteDeliveryBtn.Visibility = Visibility.Collapsed;
            AddRecordBtn.Visibility = Visibility.Collapsed;
            EditRecordBtn.Visibility = Visibility.Collapsed;
            DeleteRecordBtn.Visibility = Visibility.Collapsed;
            WordDeliveryBtn.Visibility = Visibility.Collapsed;
            WordWaybillBtn.Visibility = Visibility.Collapsed;
        }

        private void Waybill_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            waybillCrud.fetchToGrid(WaybillGrid);

            WaybillGrid.Visibility = Visibility.Visible;

            if (!userRole.Equals("ROLE_ACCOUNTANT"))
            {
                AddWaybillBtn.Visibility = Visibility.Visible;
                EditWaybillBtn.Visibility = Visibility.Visible;
                DeleteWaybillBtn.Visibility = Visibility.Visible;
                WordWaybillBtn.Visibility = Visibility.Visible;
            }

            GridName.Content = "Товарно-транспортная накладная";

            ContractorGrid.Visibility = Visibility.Collapsed;
            EmployeeGrid.Visibility = Visibility.Collapsed;
            DivisionGrid.Visibility = Visibility.Collapsed;
            CattleGrid.Visibility = Visibility.Collapsed;
            ProductGrid.Visibility = Visibility.Collapsed;
            StatementGrid.Visibility = Visibility.Collapsed;
            DeliveryNoteGrid.Visibility = Visibility.Collapsed;
            RecordCardGrid.Visibility = Visibility.Collapsed;

            AddContractorBtn.Visibility = Visibility.Collapsed;
            EditContractorBtn.Visibility = Visibility.Collapsed;
            DeleteContractorBtn.Visibility = Visibility.Collapsed;
            AddEmployeeBtn.Visibility = Visibility.Collapsed;
            EditEmployeeBtn.Visibility = Visibility.Collapsed;
            DeleteEmployeeBtn.Visibility = Visibility.Collapsed;
            AddDivisionBtn.Visibility = Visibility.Collapsed;
            EditDivisionBtn.Visibility = Visibility.Collapsed;
            DeleteDivisionBtn.Visibility = Visibility.Collapsed;
            AddCattleBtn.Visibility = Visibility.Collapsed;
            EditCattleBtn.Visibility = Visibility.Collapsed;
            DeleteCattleBtn.Visibility = Visibility.Collapsed;
            AddProductBtn.Visibility = Visibility.Collapsed;
            EditProductBtn.Visibility = Visibility.Collapsed;
            DeleteProductBtn.Visibility = Visibility.Collapsed;
            AddStatementBtn.Visibility = Visibility.Collapsed;
            EditStatementBtn.Visibility = Visibility.Collapsed;
            DeleteStatementBtn.Visibility = Visibility.Collapsed;
            AddDeliveryBtn.Visibility = Visibility.Collapsed;
            EditDeliveryBtn.Visibility = Visibility.Collapsed;
            DeleteDeliveryBtn.Visibility = Visibility.Collapsed;
            AddRecordBtn.Visibility = Visibility.Collapsed;
            EditRecordBtn.Visibility = Visibility.Collapsed;
            DeleteRecordBtn.Visibility = Visibility.Collapsed;
            WordDeliveryBtn.Visibility = Visibility.Collapsed;
        }

        private void DeliveryNote_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            deliveryNoteCrud.fetchToGrid(DeliveryNoteGrid);

            GridName.Content = "Требования накладная";

            DeliveryNoteGrid.Visibility = Visibility.Visible;

            if (!userRole.Equals("ROLE_ACCOUNTANT"))
            {
                AddDeliveryBtn.Visibility = Visibility.Visible;
                EditDeliveryBtn.Visibility = Visibility.Visible;
                DeleteDeliveryBtn.Visibility = Visibility.Visible;
                WordDeliveryBtn.Visibility = Visibility.Visible;
            }

            ContractorGrid.Visibility = Visibility.Collapsed;
            EmployeeGrid.Visibility = Visibility.Collapsed;
            DivisionGrid.Visibility = Visibility.Collapsed;
            CattleGrid.Visibility = Visibility.Collapsed;
            ProductGrid.Visibility = Visibility.Collapsed;
            StatementGrid.Visibility = Visibility.Collapsed;
            WaybillGrid.Visibility = Visibility.Collapsed;
            RecordCardGrid.Visibility = Visibility.Collapsed;

            AddContractorBtn.Visibility = Visibility.Collapsed;
            EditContractorBtn.Visibility = Visibility.Collapsed;
            DeleteContractorBtn.Visibility = Visibility.Collapsed;
            AddEmployeeBtn.Visibility = Visibility.Collapsed;
            EditEmployeeBtn.Visibility = Visibility.Collapsed;
            DeleteEmployeeBtn.Visibility = Visibility.Collapsed;
            AddDivisionBtn.Visibility = Visibility.Collapsed;
            EditDivisionBtn.Visibility = Visibility.Collapsed;
            DeleteDivisionBtn.Visibility = Visibility.Collapsed;
            AddCattleBtn.Visibility = Visibility.Collapsed;
            EditCattleBtn.Visibility = Visibility.Collapsed;
            DeleteCattleBtn.Visibility = Visibility.Collapsed;
            AddProductBtn.Visibility = Visibility.Collapsed;
            EditProductBtn.Visibility = Visibility.Collapsed;
            DeleteProductBtn.Visibility = Visibility.Collapsed;
            AddStatementBtn.Visibility = Visibility.Collapsed;
            EditStatementBtn.Visibility = Visibility.Collapsed;
            DeleteStatementBtn.Visibility = Visibility.Collapsed;
            AddWaybillBtn.Visibility = Visibility.Collapsed;
            EditWaybillBtn.Visibility = Visibility.Collapsed;
            DeleteWaybillBtn.Visibility = Visibility.Collapsed;
            AddRecordBtn.Visibility = Visibility.Collapsed;
            EditRecordBtn.Visibility = Visibility.Collapsed;
            DeleteRecordBtn.Visibility = Visibility.Collapsed;
            WordWaybillBtn.Visibility = Visibility.Collapsed;
        }

        private void RecordCard_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            recordCardCrud.fetchToGrid(RecordCardGrid);

            GridName.Content = "Карточка учёта надоя молока";

            RecordCardGrid.Visibility = Visibility.Visible;

            if (!userRole.Equals("ROLE_ACCOUNTANT"))
            {
                AddRecordBtn.Visibility = Visibility.Visible;
                EditRecordBtn.Visibility = Visibility.Visible;
                DeleteRecordBtn.Visibility = Visibility.Visible;
            }

            DeliveryNoteGrid.Visibility = Visibility.Collapsed;
            ContractorGrid.Visibility = Visibility.Collapsed;
            EmployeeGrid.Visibility = Visibility.Collapsed;
            DivisionGrid.Visibility = Visibility.Collapsed;
            CattleGrid.Visibility = Visibility.Collapsed;
            ProductGrid.Visibility = Visibility.Collapsed;
            StatementGrid.Visibility = Visibility.Collapsed;
            WaybillGrid.Visibility = Visibility.Collapsed;

            AddContractorBtn.Visibility = Visibility.Collapsed;
            EditContractorBtn.Visibility = Visibility.Collapsed;
            DeleteContractorBtn.Visibility = Visibility.Collapsed;
            AddEmployeeBtn.Visibility = Visibility.Collapsed;
            EditEmployeeBtn.Visibility = Visibility.Collapsed;
            DeleteEmployeeBtn.Visibility = Visibility.Collapsed;
            AddDivisionBtn.Visibility = Visibility.Collapsed;
            EditDivisionBtn.Visibility = Visibility.Collapsed;
            DeleteDivisionBtn.Visibility = Visibility.Collapsed;
            AddCattleBtn.Visibility = Visibility.Collapsed;
            EditCattleBtn.Visibility = Visibility.Collapsed;
            DeleteCattleBtn.Visibility = Visibility.Collapsed;
            AddProductBtn.Visibility = Visibility.Collapsed;
            EditProductBtn.Visibility = Visibility.Collapsed;
            DeleteProductBtn.Visibility = Visibility.Collapsed;
            AddStatementBtn.Visibility = Visibility.Collapsed;
            EditStatementBtn.Visibility = Visibility.Collapsed;
            DeleteStatementBtn.Visibility = Visibility.Collapsed;
            AddWaybillBtn.Visibility = Visibility.Collapsed;
            EditWaybillBtn.Visibility = Visibility.Collapsed;
            DeleteWaybillBtn.Visibility = Visibility.Collapsed;
            AddDeliveryBtn.Visibility = Visibility.Collapsed;
            EditDeliveryBtn.Visibility = Visibility.Collapsed;
            DeleteDeliveryBtn.Visibility = Visibility.Collapsed;
            WordDeliveryBtn.Visibility = Visibility.Collapsed;
            WordWaybillBtn.Visibility = Visibility.Collapsed;
        }

        private void DocumentOutput_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DocumentOutput document = new DocumentOutput();
            document.ShowDialog();
        }

        private void AdminRegistration_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            RegistrationView registration = new RegistrationView();
            registration.ShowDialog();
        }

        private void AddContractor_Click(object sender, RoutedEventArgs e)
        {
            CreateContractor contractor = new CreateContractor(ContractorGrid);
            contractor.ShowDialog();
        }

        private void EditContractor_Click(object sender, RoutedEventArgs e)
        {
            var selectedRow = ContractorGrid.SelectedItem as DataRowView;

            if (selectedRow != null)
            {
                EditContractor contractor = new EditContractor(Convert.ToInt64(selectedRow.Row.ItemArray[0]), Convert.ToString(selectedRow.Row.ItemArray[1]), Convert.ToString(selectedRow.Row.ItemArray[2]), Convert.ToString(selectedRow.Row.ItemArray[3]), Convert.ToString(selectedRow.Row.ItemArray[4]), Convert.ToString(selectedRow.Row.ItemArray[5]), ContractorGrid);
                contractor.ShowDialog();
            }
            else
            {
                MessageBox.Show("Не выбрана строка для редактирования", "Ошибка", MessageBoxButton.OK);
            }
        }

        private void DeleteContractor_Click(object sender, RoutedEventArgs e)
        {
            var selectedRow = ContractorGrid.SelectedItem as DataRowView;

            if (selectedRow != null)
            {
                    long contractorId = Convert.ToInt64(selectedRow.Row.ItemArray[0]);

                    contractorCrud.delete(contractorId);
                    contractorCrud.fetchToGrid(ContractorGrid);
            }
            else
            {
                MessageBox.Show("Выберите поле для удаления!");
            }
        }

        private void AddEmployee_Click(object sender, RoutedEventArgs e)
        {
            CreateEmployee employee = new CreateEmployee(EmployeeGrid);
            employee.ShowDialog();
        }

        private void EditEmployee_Click(object sender, RoutedEventArgs e)
        {
            var selectedRow = EmployeeGrid.SelectedItem as DataRowView;

            if (selectedRow != null)
            {
                EditEmployee employee = new EditEmployee(Convert.ToInt64(selectedRow.Row.ItemArray[0]), Convert.ToString(selectedRow.Row.ItemArray[1]), Convert.ToString(selectedRow.Row.ItemArray[2]), Convert.ToString(selectedRow.Row.ItemArray[3]), Convert.ToString(selectedRow.Row.ItemArray[4]), EmployeeGrid);
                employee.ShowDialog();
            }
            else
            {
                MessageBox.Show("Не выбрана строка для редактирования", "Ошибка", MessageBoxButton.OK);
            }
        }

        private void DeleteEmployee_Click(object sender, RoutedEventArgs e)
        {
            var selectedRow = EmployeeGrid.SelectedItem as DataRowView;

            if (selectedRow != null)
            {
                
                    long employeeId = Convert.ToInt64(selectedRow.Row.ItemArray[0]);

                    employeeCrud.delete(employeeId);
               
            }
            else
            {
                MessageBox.Show("Выберите поле для удаления!");
            }
        }

        private void AddDivision_Click(object sender, RoutedEventArgs e)
        {
            AddDivision division = new AddDivision(DivisionGrid);
            division.ShowDialog();
        }

        private void EditDivision_Click(object sender, RoutedEventArgs e)
        {
            var selectedRow = DivisionGrid.SelectedItem as DataRowView;

            if (selectedRow != null)
            {
                EditDivision division = new EditDivision(Convert.ToInt64(selectedRow.Row.ItemArray[0]), Convert.ToString(selectedRow.Row.ItemArray[1]), DivisionGrid);
                division.ShowDialog();
            }
            else
            {
                MessageBox.Show("Не выбрана строка для редактирования", "Ошибка", MessageBoxButton.OK);
            }
        }

        private void DeleteDivision_Click(object sender, RoutedEventArgs e)
        {
            var selectedRow = DivisionGrid.SelectedItem as DataRowView;

            if (selectedRow != null)
            {
               long divisionId = Convert.ToInt64(selectedRow.Row.ItemArray[0]);

                    divisionCrud.delete(divisionId);
                    divisionCrud.fetchToGrid(DivisionGrid);
                
            }
            else
            {
                MessageBox.Show("Выберите поле для удаления!");
            }
        }

        private void AddCattle_Click(object sender, RoutedEventArgs e)
        {
            CreateCattle cattle = new CreateCattle(CattleGrid);
            cattle.ShowDialog();
        }

        private void EditCattle_Click(object sender, RoutedEventArgs e)
        {
            var selectedRow = CattleGrid.SelectedItem as DataRowView;

            if (selectedRow != null)
            {
                EditCattle cattle = new EditCattle(Convert.ToInt64(selectedRow.Row.ItemArray[0]), Convert.ToString(selectedRow.Row.ItemArray[1]), CattleGrid);
                cattle.ShowDialog();
            }
            else
            {
                MessageBox.Show("Не выбрана строка для редактирования", "Ошибка", MessageBoxButton.OK);
            }
        }

        private void DeleteCattle_Click(object sender, RoutedEventArgs e)
        {
            var selectedRow = CattleGrid.SelectedItem as DataRowView;

            if (selectedRow != null)
            {
               
                    long cattleId = Convert.ToInt64(selectedRow.Row.ItemArray[0]);

                    cattleCrud.delete(cattleId);
                    cattleCrud.fetchToGrid(CattleGrid);
                
            }
            else
            {
                MessageBox.Show("Выберите поле для удаления!");
            }
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            CreateProduct product = new CreateProduct(ProductGrid);
            product.ShowDialog();
        }

        private void EditProduct_Click(object sender, RoutedEventArgs e)
        {
            var selectedRow = ProductGrid.SelectedItem as DataRowView;

            if (selectedRow != null)
            {
                EditProduct product = new EditProduct(Convert.ToInt64(selectedRow.Row.ItemArray[0]), Convert.ToString(selectedRow.Row.ItemArray[1]), Convert.ToString(selectedRow.Row.ItemArray[2]), Convert.ToString(selectedRow.Row.ItemArray[3]), ProductGrid);
                product.ShowDialog();
            }
            else
            {
                MessageBox.Show("Не выбрана строка для редактирования", "Ошибка", MessageBoxButton.OK);
            }
        }

        private void DeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            var selectedRow = ProductGrid.SelectedItem as DataRowView;

            if (selectedRow != null)
            {
                long productId = Convert.ToInt64(selectedRow.Row.ItemArray[0]);

                productCrud.delete(productId);
                productCrud.fetchToGrid(ProductGrid);
            }
            else
            {
                MessageBox.Show("Выберите поле для удаления!");
            }
        }

        private void AddStatement_Click(object sender, RoutedEventArgs e)
        {
            CreateStatement statement = new CreateStatement(StatementGrid);
            statement.ShowDialog();
        }

        private void EditStatement_Click(object sender, RoutedEventArgs e)
        {
            var selectedRow = StatementGrid.SelectedItem as DataRowView;

            if (selectedRow != null)
            {
                EditStatement statement = new EditStatement(
                    Convert.ToInt64(selectedRow.Row.ItemArray[0]), Convert.ToString(selectedRow.Row.ItemArray[1]), Convert.ToString(selectedRow.Row.ItemArray[2]), 
                    Convert.ToString(selectedRow.Row.ItemArray[3]), Convert.ToString(selectedRow.Row.ItemArray[4]), Convert.ToString(selectedRow.Row.ItemArray[5]),
                    Convert.ToString(selectedRow.Row.ItemArray[7]), Convert.ToString(selectedRow.Row.ItemArray[8]), Convert.ToString(selectedRow.Row.ItemArray[10]),
                    StatementGrid);

                statement.ShowDialog();
            }
            else
            {
                MessageBox.Show("Не выбрана строка для редактирования", "Ошибка", MessageBoxButton.OK);
            }
        }

        private void DeleteStatement_Click(object sender, RoutedEventArgs e)
        {
            var selectedRow = StatementGrid.SelectedItem as DataRowView;

            if (selectedRow != null)
            {
                
                    long statementId = Convert.ToInt64(selectedRow.Row.ItemArray[0]);

                    statementCrud.delete(statementId);
                    statementCrud.fetchToGrid(StatementGrid);
                
            }
            else
            {
                MessageBox.Show("Выберите поле для удаления!");
            }
        }

        private void AddWaybill_Click(object sender, RoutedEventArgs e)
        {
            CreateWaybill waybill = new CreateWaybill(WaybillGrid);
            waybill.ShowDialog();
        }

        private void EditWaybill_Click(object sender, RoutedEventArgs e)
        {
            var selectedRow = WaybillGrid.SelectedItem as DataRowView;

            if (selectedRow != null)
            {
                EditWaybill waybill = new EditWaybill(Convert.ToInt64(selectedRow.Row.ItemArray[0]), WaybillGrid);

                waybill.ShowDialog();
            }
            else
            {
                MessageBox.Show("Не выбрана строка для редактирования", "Ошибка", MessageBoxButton.OK);
            }
        }

        private void DeleteWaybill_Click(object sender, RoutedEventArgs e)
        {
            var selectedRow = WaybillGrid.SelectedItem as DataRowView;

            if (selectedRow != null)
            {
                long waybillId = Convert.ToInt64(selectedRow.Row.ItemArray[0]);

                waybillDelete = new WaybillDelete(waybillId);

                waybillDelete.deleteWaybill();
                waybillCrud.fetchToGrid(WaybillGrid);
            }
            else
            {
                MessageBox.Show("Выберите поле для удаления!");
            }
        }

        private void AddDeliveryNote_Click(object sender, RoutedEventArgs e)
        {
            CreateDelivery delivery = new CreateDelivery(DeliveryNoteGrid);
            delivery.ShowDialog();
        }

        private void EditDeliveryNote_Click(object sender, RoutedEventArgs e)
        {
            var selectedRow = DeliveryNoteGrid.SelectedItem as DataRowView;

            if (selectedRow != null)
            {
                EditDelivery delivery = new EditDelivery(Convert.ToInt64(selectedRow.Row.ItemArray[0]), DeliveryNoteGrid);

                delivery.ShowDialog();
            }
            else
            {
                MessageBox.Show("Не выбрана строка для редактирования", "Ошибка", MessageBoxButton.OK);
            }
        }

        private void DeleteDeliveryNote_Click(object sender, RoutedEventArgs e)
        {
            var selectedRow = DeliveryNoteGrid.SelectedItem as DataRowView;

            if (selectedRow != null)
            {
                long id = Convert.ToInt64(selectedRow.Row.ItemArray[0]);

                deliveryDelete = new DeliveryDelete(id);

                deliveryDelete.deleteDelivery();
                deliveryNoteCrud.fetchToGrid(DeliveryNoteGrid);
            }
            else
            {
                MessageBox.Show("Выберите поле для удаления!");
            }
        }

        private void AddRecordCard_Click(object sender, RoutedEventArgs e)
        {
            CreateRecordCard recordCard = new CreateRecordCard(RecordCardGrid);
            recordCard.ShowDialog();
        }

        private void EditRecordCard_Click(object sender, RoutedEventArgs e)
        {
            var selectedRow = RecordCardGrid.SelectedItem as DataRowView;

            if (selectedRow != null)
            {
                EditRecordCard recordCard = new EditRecordCard(
                    Convert.ToInt64(selectedRow.Row.ItemArray[0]), Convert.ToString(selectedRow.Row.ItemArray[1]), Convert.ToString(selectedRow.Row.ItemArray[2]),
                    Convert.ToString(selectedRow.Row.ItemArray[3]), Convert.ToString(selectedRow.Row.ItemArray[4]), Convert.ToString(selectedRow.Row.ItemArray[5]),
                    Convert.ToString(selectedRow.Row.ItemArray[6]), Convert.ToString(selectedRow.Row.ItemArray[7]), Convert.ToString(selectedRow.Row.ItemArray[8]),
                    RecordCardGrid);

                recordCard.ShowDialog();
            }
            else
            {
                MessageBox.Show("Не выбрана строка для редактирования", "Ошибка", MessageBoxButton.OK);
            }
        }

        private void DeleteRecordCard_Click(object sender, RoutedEventArgs e)
        {
            var selectedRow = RecordCardGrid.SelectedItem as DataRowView;

            if (selectedRow != null)
            {
                long recordCardId = Convert.ToInt64(selectedRow.Row.ItemArray[0]);

                recordCardCrud.delete(recordCardId);
                recordCardCrud.fetchToGrid(RecordCardGrid);
            }
            else
            {
                MessageBox.Show("Выберите поле для удаления!");
            }
        }

        private string fetchFieldByView()
        {
            FetchFieldView fieldView = new FetchFieldView();
            fieldView.ShowDialog();

            return fieldView.field;
        }

        private void filtrateGrid(DataGrid dataGrid)
        {
            string field = fetchFieldByView();

            if (field != null)
            {
                gridUtility.applyFilter(field, dataGrid);
            }
        }

        private void searchGrid(DataGrid dataGrid)
        {
            string field = fetchFieldByView();

            if (field != null)
            {
                gridUtility.searchAndSort(field, dataGrid);
            }
        }

        private void FiltrationContractor_Click(object sender, RoutedEventArgs e)
        {
            filtrateGrid(ContractorGrid);
        }

        private void SearchContractor_Click(object sender, RoutedEventArgs e)
        {
            searchGrid(ContractorGrid);
        }

        private void CancelContractor_Click(object sender, RoutedEventArgs e)
        {
            contractorCrud.fetchToGrid(ContractorGrid);
        }

        private void FiltrationEmployee_Click(object sender, RoutedEventArgs e)
        {
            filtrateGrid(EmployeeGrid);
        }

        private void SearchEmployee_Click(object sender, RoutedEventArgs e)
        {
            searchGrid(EmployeeGrid);
        }

        private void CancelEmployee_Click(object sender, RoutedEventArgs e)
        {
            employeeCrud.fetchToGrid(EmployeeGrid);
        }

        private void FiltrationDivision_Click(object sender, RoutedEventArgs e)
        {
            filtrateGrid(DivisionGrid);
        }

        private void SearchDivision_Click(object sender, RoutedEventArgs e)
        {
            searchGrid(DivisionGrid);
        }

        private void CancelDivision_Click(object sender, RoutedEventArgs e)
        {
            divisionCrud.fetchToGrid(DivisionGrid);
        }

        private void FiltrationCattle_Click(object sender, RoutedEventArgs e)
        {
            filtrateGrid(CattleGrid);
        }

        private void SearchCattle_Click(object sender, RoutedEventArgs e)
        {
            searchGrid(CattleGrid);
        }

        private void CancelCattle_Click(object sender, RoutedEventArgs e)
        {
            cattleCrud.fetchToGrid(CattleGrid);
        }

        private void FiltrationProduct_Click(object sender, RoutedEventArgs e)
        {
            filtrateGrid(ProductGrid);
        }

        private void SearchProduct_Click(object sender, RoutedEventArgs e)
        {
            searchGrid(ProductGrid);
        }

        private void CancelProduct_Click(object sender, RoutedEventArgs e)
        {
            productCrud.fetchToGrid(ProductGrid);
        }

        private void FiltrationStatement_Click(object sender, RoutedEventArgs e)
        {
            filtrateGrid(StatementGrid);
        }

        private void SearchStatement_Click(object sender, RoutedEventArgs e)
        {
            searchGrid(StatementGrid);
        }

        private void CancelStatement_Click(object sender, RoutedEventArgs e)
        {
            statementCrud.fetchToGrid(StatementGrid);
        }

        private void FiltrationWaybill_Click(object sender, RoutedEventArgs e)
        {
            filtrateGrid(WaybillGrid);
        }

        private void SearchWaybill_Click(object sender, RoutedEventArgs e)
        {
            searchGrid(WaybillGrid);
        }

        private void CancelWaybill_Click(object sender, RoutedEventArgs e)
        {
            waybillCrud.fetchToGrid(WaybillGrid);
        }

        private void FiltrationDeliveryNote_Click(object sender, RoutedEventArgs e)
        {
            filtrateGrid(DeliveryNoteGrid);
        }

        private void SearchDeliveryNote_Click(object sender, RoutedEventArgs e)
        {
            searchGrid(DeliveryNoteGrid);
        }

        private void CancelDeliveryNote_Click(object sender, RoutedEventArgs e)
        {
            deliveryNoteCrud.fetchToGrid(DeliveryNoteGrid);
        }

        private void FiltrationRecordCard_Click(object sender, RoutedEventArgs e)
        {
            filtrateGrid(RecordCardGrid);
        }

        private void SearchRecordCard_Click(object sender, RoutedEventArgs e)
        {
            searchGrid(RecordCardGrid);
        }

        private void CancelRecordCard_Click(object sender, RoutedEventArgs e)
        {
            recordCardCrud.fetchToGrid(RecordCardGrid);
        }

        private void ViewWaybill_Click(object sender, RoutedEventArgs e)
        {
            var selectedRow = WaybillGrid.SelectedItem as DataRowView;

            if (selectedRow != null)
            {
                ViewWaybill waybill = new ViewWaybill(Convert.ToInt64(selectedRow.Row.ItemArray[0]));
                waybill.ShowDialog();
            }
            else
            {
                MessageBox.Show("Не выбрана строка!", "Ошибка", MessageBoxButton.OK);
            }
        }

        private void ViewDeliveryNote_Click(object sender, RoutedEventArgs e)
        {
            var selectedRow = DeliveryNoteGrid.SelectedItem as DataRowView;

            if (selectedRow != null)
            {
                DeliveryView delivery = new DeliveryView(Convert.ToInt64(selectedRow.Row.ItemArray[0]), userRole);
                delivery.ShowDialog();
            }
            else
            {
                MessageBox.Show("Не выбрана строка!", "Ошибка", MessageBoxButton.OK);
            }
        }

        private void WordDeliveryNote_Click(object sender, RoutedEventArgs e)
        {
            var selectedRow = DeliveryNoteGrid.SelectedItem as DataRowView;

            if (selectedRow != null)
            {
                DeliveryNoteOutput output = new DeliveryNoteOutput();
                output.generateWordDocument(Convert.ToInt64(selectedRow.Row.ItemArray[0]));
            }
            else
            {
                MessageBox.Show("Не выбрана строка!", "Ошибка", MessageBoxButton.OK);
            }
        }

        private void WordStatement_Click(object sender, RoutedEventArgs e)
        {
            StatementOutView statementOutView = new StatementOutView();
            statementOutView.ShowDialog();
        }

        private void WordWaybill_Click(object sender, RoutedEventArgs e)
        {
            var selectedRow = WaybillGrid.SelectedItem as DataRowView;

            if (selectedRow != null)
            {
                WaybillOutput output = new WaybillOutput();
                output.generateWordDocument(Convert.ToInt64(selectedRow.Row.ItemArray[0]));
            }
            else
            {
                MessageBox.Show("Не выбрана строка!", "Ошибка", MessageBoxButton.OK);
            }
        }

        private void WordRecordCard_Click(object sender, RoutedEventArgs e)
        {
            RecordOutView recordOut = new RecordOutView();
            recordOut.ShowDialog();
        }

        private void WordTotalWaybill_Click(object sender, RoutedEventArgs e)
        {
            WaybillOutView waybillOutView = new WaybillOutView();
            waybillOutView.ShowDialog();
        }

        private void AddContractorBtn_Click(object sender, RoutedEventArgs e)
        {
            CreateContractor contractor = new CreateContractor(ContractorGrid);
            contractor.ShowDialog();
        }

        private void EditContractorBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedRow = ContractorGrid.SelectedItem as DataRowView;

            if (selectedRow != null)
            {
                EditContractor contractor = new EditContractor(Convert.ToInt64(selectedRow.Row.ItemArray[0]), Convert.ToString(selectedRow.Row.ItemArray[1]), Convert.ToString(selectedRow.Row.ItemArray[2]), Convert.ToString(selectedRow.Row.ItemArray[3]), Convert.ToString(selectedRow.Row.ItemArray[4]), Convert.ToString(selectedRow.Row.ItemArray[5]), ContractorGrid);
                contractor.ShowDialog();
            }
            else
            {
                MessageBox.Show("Не выбрана строка для редактирования", "Ошибка", MessageBoxButton.OK);
            }
        }

        private void DeleteContractorBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedRow = ContractorGrid.SelectedItem as DataRowView;

            if (selectedRow != null)
            {
                long contractorId = Convert.ToInt64(selectedRow.Row.ItemArray[0]);

                contractorCrud.delete(contractorId);
                contractorCrud.fetchToGrid(ContractorGrid);
            }
            else
            {
                MessageBox.Show("Выберите поле для удаления!");
            }
        }

        private void AddEmployeeBtn_Click(object sender, RoutedEventArgs e)
        {
            CreateEmployee employee = new CreateEmployee(EmployeeGrid);
            employee.ShowDialog();
        }

        private void EditEmployeeBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedRow = EmployeeGrid.SelectedItem as DataRowView;

            if (selectedRow != null)
            {
                EditEmployee employee = new EditEmployee(Convert.ToInt64(selectedRow.Row.ItemArray[0]), Convert.ToString(selectedRow.Row.ItemArray[1]), Convert.ToString(selectedRow.Row.ItemArray[2]), Convert.ToString(selectedRow.Row.ItemArray[3]), Convert.ToString(selectedRow.Row.ItemArray[4]), EmployeeGrid);
                employee.ShowDialog();
            }
            else
            {
                MessageBox.Show("Не выбрана строка для редактирования", "Ошибка", MessageBoxButton.OK);
            }
        }

        private void DeleteEmployeeBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedRow = EmployeeGrid.SelectedItem as DataRowView;

            if (selectedRow != null)
            {

                long employeeId = Convert.ToInt64(selectedRow.Row.ItemArray[0]);

                employeeCrud.delete(employeeId);

            }
            else
            {
                MessageBox.Show("Выберите поле для удаления!");
            }
        }

        private void AddDivisionBtn_Click(object sender, RoutedEventArgs e)
        {
            AddDivision division = new AddDivision(DivisionGrid);
            division.ShowDialog();
        }

        private void EditDivisionBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedRow = DivisionGrid.SelectedItem as DataRowView;

            if (selectedRow != null)
            {
                EditDivision division = new EditDivision(Convert.ToInt64(selectedRow.Row.ItemArray[0]), Convert.ToString(selectedRow.Row.ItemArray[1]), DivisionGrid);
                division.ShowDialog();
            }
            else
            {
                MessageBox.Show("Не выбрана строка для редактирования", "Ошибка", MessageBoxButton.OK);
            }
        }

        private void DeleteDivisionBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedRow = DivisionGrid.SelectedItem as DataRowView;

            if (selectedRow != null)
            {
                long divisionId = Convert.ToInt64(selectedRow.Row.ItemArray[0]);

                divisionCrud.delete(divisionId);
                divisionCrud.fetchToGrid(DivisionGrid);

            }
            else
            {
                MessageBox.Show("Выберите поле для удаления!");
            }
        }

        private void AddCattleBtn_Click(object sender, RoutedEventArgs e)
        {
            CreateCattle cattle = new CreateCattle(CattleGrid);
            cattle.ShowDialog();
        }

        private void EditCattleBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedRow = CattleGrid.SelectedItem as DataRowView;

            if (selectedRow != null)
            {
                EditCattle cattle = new EditCattle(Convert.ToInt64(selectedRow.Row.ItemArray[0]), Convert.ToString(selectedRow.Row.ItemArray[1]), CattleGrid);
                cattle.ShowDialog();
            }
            else
            {
                MessageBox.Show("Не выбрана строка для редактирования", "Ошибка", MessageBoxButton.OK);
            }
        }

        private void DeleteCattleBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedRow = CattleGrid.SelectedItem as DataRowView;

            if (selectedRow != null)
            {

                long cattleId = Convert.ToInt64(selectedRow.Row.ItemArray[0]);

                cattleCrud.delete(cattleId);
                cattleCrud.fetchToGrid(CattleGrid);

            }
            else
            {
                MessageBox.Show("Выберите поле для удаления!");
            }
        }

        private void AddProductBtn_Click(object sender, RoutedEventArgs e)
        {
            CreateProduct product = new CreateProduct(ProductGrid);
            product.ShowDialog();
        }

        private void EditProductBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedRow = ProductGrid.SelectedItem as DataRowView;

            if (selectedRow != null)
            {
                EditProduct product = new EditProduct(Convert.ToInt64(selectedRow.Row.ItemArray[0]), Convert.ToString(selectedRow.Row.ItemArray[1]), Convert.ToString(selectedRow.Row.ItemArray[2]), Convert.ToString(selectedRow.Row.ItemArray[3]), ProductGrid);
                product.ShowDialog();
            }
            else
            {
                MessageBox.Show("Не выбрана строка для редактирования", "Ошибка", MessageBoxButton.OK);
            }
        }

        private void DeleteProductBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedRow = ProductGrid.SelectedItem as DataRowView;

            if (selectedRow != null)
            {
                long productId = Convert.ToInt64(selectedRow.Row.ItemArray[0]);

                productCrud.delete(productId);
                productCrud.fetchToGrid(ProductGrid);
            }
            else
            {
                MessageBox.Show("Выберите поле для удаления!");
            }
        }

        private void AddStatementBtn_Click(object sender, RoutedEventArgs e)
        {
            CreateStatement statement = new CreateStatement(StatementGrid);
            statement.ShowDialog();
        }

        private void EditStatementBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedRow = StatementGrid.SelectedItem as DataRowView;

            if (selectedRow != null)
            {
                EditStatement statement = new EditStatement(
                    Convert.ToInt64(selectedRow.Row.ItemArray[0]), Convert.ToString(selectedRow.Row.ItemArray[1]), Convert.ToString(selectedRow.Row.ItemArray[2]),
                    Convert.ToString(selectedRow.Row.ItemArray[3]), Convert.ToString(selectedRow.Row.ItemArray[4]), Convert.ToString(selectedRow.Row.ItemArray[5]),
                    Convert.ToString(selectedRow.Row.ItemArray[7]), Convert.ToString(selectedRow.Row.ItemArray[8]), Convert.ToString(selectedRow.Row.ItemArray[10]),
                    StatementGrid);

                statement.ShowDialog();
            }
            else
            {
                MessageBox.Show("Не выбрана строка для редактирования", "Ошибка", MessageBoxButton.OK);
            }
        }

        private void DeleteStatementBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedRow = StatementGrid.SelectedItem as DataRowView;

            if (selectedRow != null)
            {

                long statementId = Convert.ToInt64(selectedRow.Row.ItemArray[0]);

                statementCrud.delete(statementId);
                statementCrud.fetchToGrid(StatementGrid);

            }
            else
            {
                MessageBox.Show("Выберите поле для удаления!");
            }
        }

        private void AddWaybillBtn_Click(object sender, RoutedEventArgs e)
        {
            CreateWaybill waybill = new CreateWaybill(WaybillGrid);
            waybill.ShowDialog();
        }

        private void EditWaybillBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedRow = WaybillGrid.SelectedItem as DataRowView;

            if (selectedRow != null)
            {
                EditWaybill waybill = new EditWaybill(Convert.ToInt64(selectedRow.Row.ItemArray[0]), WaybillGrid);

                waybill.ShowDialog();
            }
            else
            {
                MessageBox.Show("Не выбрана строка для редактирования", "Ошибка", MessageBoxButton.OK);
            }
        }

        private void DeleteWaybillBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedRow = WaybillGrid.SelectedItem as DataRowView;

            if (selectedRow != null)
            {
                long waybillId = Convert.ToInt64(selectedRow.Row.ItemArray[0]);

                waybillDelete = new WaybillDelete(waybillId);

                waybillDelete.deleteWaybill();
                waybillCrud.fetchToGrid(WaybillGrid);
            }
            else
            {
                MessageBox.Show("Выберите поле для удаления!");
            }
        }

        private void AddDeliveryBtn_Click(object sender, RoutedEventArgs e)
        {
            CreateDelivery delivery = new CreateDelivery(DeliveryNoteGrid);
            delivery.ShowDialog();
        }

        private void EditDeliveryBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedRow = DeliveryNoteGrid.SelectedItem as DataRowView;

            if (selectedRow != null)
            {
                EditDelivery delivery = new EditDelivery(Convert.ToInt64(selectedRow.Row.ItemArray[0]), DeliveryNoteGrid);

                delivery.ShowDialog();
            }
            else
            {
                MessageBox.Show("Не выбрана строка для редактирования", "Ошибка", MessageBoxButton.OK);
            }
        }

        private void DeleteDeliveryBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedRow = DeliveryNoteGrid.SelectedItem as DataRowView;

            if (selectedRow != null)
            {
                long id = Convert.ToInt64(selectedRow.Row.ItemArray[0]);

                deliveryDelete = new DeliveryDelete(id);

                deliveryDelete.deleteDelivery();
                deliveryNoteCrud.fetchToGrid(DeliveryNoteGrid);
            }
            else
            {
                MessageBox.Show("Выберите поле для удаления!");
            }
        }

        private void AddRecordBtn_Click(object sender, RoutedEventArgs e)
        {
            CreateRecordCard recordCard = new CreateRecordCard(RecordCardGrid);
            recordCard.ShowDialog();
        }

        private void EditRecordBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedRow = RecordCardGrid.SelectedItem as DataRowView;

            if (selectedRow != null)
            {
                EditRecordCard recordCard = new EditRecordCard(
                    Convert.ToInt64(selectedRow.Row.ItemArray[0]), Convert.ToString(selectedRow.Row.ItemArray[1]), Convert.ToString(selectedRow.Row.ItemArray[2]),
                    Convert.ToString(selectedRow.Row.ItemArray[3]), Convert.ToString(selectedRow.Row.ItemArray[4]), Convert.ToString(selectedRow.Row.ItemArray[5]),
                    Convert.ToString(selectedRow.Row.ItemArray[6]), Convert.ToString(selectedRow.Row.ItemArray[7]), Convert.ToString(selectedRow.Row.ItemArray[8]),
                    RecordCardGrid);

                recordCard.ShowDialog();
            }
            else
            {
                MessageBox.Show("Не выбрана строка для редактирования", "Ошибка", MessageBoxButton.OK);
            }
        }

        private void DeleteRecordBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedRow = RecordCardGrid.SelectedItem as DataRowView;

            if (selectedRow != null)
            {
                long recordCardId = Convert.ToInt64(selectedRow.Row.ItemArray[0]);

                recordCardCrud.delete(recordCardId);
                recordCardCrud.fetchToGrid(RecordCardGrid);
            }
            else
            {
                MessageBox.Show("Выберите поле для удаления!");
            }
        }

        private void WordDeliveryBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedRow = DeliveryNoteGrid.SelectedItem as DataRowView;

            if (selectedRow != null)
            {
                DeliveryNoteOutput output = new DeliveryNoteOutput();
                output.generateWordDocument(Convert.ToInt64(selectedRow.Row.ItemArray[0]));
            }
            else
            {
                MessageBox.Show("Не выбрана строка!", "Ошибка", MessageBoxButton.OK);
            }
        }

        private void WordWaybillBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedRow = WaybillGrid.SelectedItem as DataRowView;

            if (selectedRow != null)
            {
                WaybillOutput output = new WaybillOutput();
                output.generateWordDocument(Convert.ToInt64(selectedRow.Row.ItemArray[0]));
            }
            else
            {
                MessageBox.Show("Не выбрана строка!", "Ошибка", MessageBoxButton.OK);
            }
        }
    }
}