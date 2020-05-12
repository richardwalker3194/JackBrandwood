using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;

namespace WpfAppSale
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ClassSale Sale;
        DataTable dtCustomer;
        int SaleID;
        int SaleItemID;
        int ProductID;

        public MainWindow()
        {
            InitializeComponent();
            Sale = new ClassSale();
            dgSale.ItemsSource = Sale.dtSales.DefaultView;
            dgSaleItem.ItemsSource = Sale.dtSaleItems.DefaultView;
            dgProduct.ItemsSource = Sale.dtProducts.DefaultView;
            dtCustomer = new DataTable();
            ComboboxPopulate();
        }
        private void ComboboxPopulate() {
            dtCustomer = Sale.dtCustomer;
            comboboxCustomerSearch.ItemsSource = dtCustomer.DefaultView;
            comboboxCustomerSearch.DisplayMemberPath = "CustomerName";
            comboboxCustomerSearch.SelectedValuePath = "CustomerID";

            comboboxCustomer.ItemsSource = dtCustomer.DefaultView;
            comboboxCustomer.DisplayMemberPath = "CustomerName";
            comboboxCustomer.SelectedValuePath = "CustomerID";


        }

        private void ComboboxCustomerSearch_SelectionChanged(object sender, SelectionChangedEventArgs args) {SaleGetByCustomerID();}
        private void SaleRowSelectionChanged(object sender, SelectionChangedEventArgs args){ SaleGetBySaleID(); }
        private void SaleItemRowSelectionChanged(object sender, SelectionChangedEventArgs args) { SaleItemGetBySaleItemID(); }
        
        private void buttonSaleUpdate_Click(object sender, RoutedEventArgs e)
        {
            SaleUpdate();
        }
        private void buttonSaleInsert_Click(object sender, RoutedEventArgs e)
        {
            SaleInsert();
        }
        private void buttonSaleItemInsert_Click(object sender, RoutedEventArgs e) { SaleItemInsert(); }
        private void ButtonSaleItemDelete_Click(object sender, RoutedEventArgs e){ SaleItemDelete(); }
        
        private void TextboxFillSaleItem() 
        { 
        
        }
        private void TextboxFillSale() {
            calendarSaleDate.DisplayDate = Sale.SaleDate;
            calendarSaleDate.SelectedDate = Sale.SaleDate;
        }              
        private void ColumnFillFromField()
        {

            DataRowView drv = (DataRowView)dgSale.SelectedItem;
            drv["SaleDate"] = Sale.SaleDate;

        }

        private void SaleGetBySaleID() 
        { 

            if (dgSale.SelectedItem != null) 
                {
                DataRowView drv = (DataRowView)dgSale.SelectedItem;
                string strSaleID = drv["SaleID"].ToString();
                SaleID = Convert.ToInt32(strSaleID);
                Sale.SaleID = SaleID;
                Sale.SaleGetBySaleID();
                TextboxFillSale();
                Sale.SaleItemGetBySaleID();
                } 
        }
       
        private void SaleItemGetBySaleItemID() 
        
        {

            if (dgSaleItem.SelectedItem != null)
            {
                DataRowView drv = (DataRowView)dgSaleItem.SelectedItem;
                string strSaleItemID = drv["SaleItemID"].ToString();
                SaleItemID = Convert.ToInt32(strSaleItemID);
                Sale.SaleItemID = SaleItemID;
                Sale.SaleItemGetBySaleItemID();
                TextboxFillSaleItem();
            }
        }
        private void SaleGetByCustomerID() {

           Sale.CustomerID = Convert.ToInt32(comboboxCustomerSearch.SelectedValue);
           Sale.SaleGetByCustomerID();
        
        }


        private void SaleUpdate() {
            Sale.SaleDate = Convert.ToDateTime(calendarSaleDate.SelectedDate);
            Sale.SaleUpdate();
            ColumnFillFromField();
        }

        private void SaleInsert() {
            Sale.CustomerID = Convert.ToInt32(comboboxCustomer.SelectedValue);
            Sale.SaleDate = Convert.ToDateTime(calendarSaleDate.SelectedDate);
            Sale.SaleInsert();
            SaleID = Sale.SaleID;
            InsertedRowSelectAndHighlight();
        }
        private void InsertedRowSelectAndHighlight()
        {
            string IDToFind = SaleID.ToString();
            if (dgSale.ItemsSource is DataView)
            {
                foreach (DataRowView drv in (DataView)dgSale.ItemsSource)
                { 
                    if (drv["SaleID"].ToString() == IDToFind)
                    {

                        dgSale.SelectedItem = drv;
                        dgSale.ScrollIntoView(drv);

                    }
                }
            }

        }

        private void SaleItemInsert() 
        
        {
            if (dgProduct.SelectedItem != null)
            {
                DataRowView drv = (DataRowView)dgProduct.SelectedItem;
                string strUnitPrice = drv["UnitPrice"].ToString();
                string strProductID = drv["ProductID"].ToString();
                ProductID = Convert.ToInt32(strProductID);
                Sale.ProductID = ProductID;
                if (int.TryParse(textboxQuantity.Text, out _)) {Sale.Quantity = Convert.ToDecimal(textboxQuantity.Text); } else { Sale.Quantity = 0; }
                Sale.SaleItemInsert();
            }
        }

        private void SaleItemDelete() 
        {
            Sale.SaleItemID = SaleItemID;
            Sale.SaleItemDelete();
        }

  
    }
}
