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


namespace WpfAppAdmin
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ClassCustomer Customer = new ClassCustomer();
        Int32 CustomerID;
        string strCustomerID;
        DataTable dtCountries;
        DataTable dtCustomerTypes;
        Boolean blnInsertMode;
        public MainWindow()
        {
            InitializeComponent();
            comboboxPopulate();
            dgCustomer.ItemsSource = Customer.Customers.DefaultView;


        }

        #region Events
        private void buttonSearchClear_Click(object sender, RoutedEventArgs e)
        {
            SearchClear();
        }
        private void SelectionChanged(object sender, SelectionChangedEventArgs args)
        {
            CustomerGetByMultiple();

        }
        private void RowSelection(object sender, SelectionChangedEventArgs args)
        {
            if (dgCustomer.SelectedItem != null)
            {
                string strCustomerID = "";
                DataRowView drv = (DataRowView)dgCustomer.SelectedItem;
                strCustomerID = drv["CustomerID"].ToString();
                CustomerID = Convert.ToInt32(strCustomerID);
                Customer.CustomerID = CustomerID;
                Customer.CustomerGetByCustomerID();
                TextboxFillFromField();
                ComboboxSet();
            }
        }
        private void ButtonCustomerUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (blnInsertMode) { CustomerInsert(); } else {CustomerUpdate(); }
            
        }
        private void ButtonNew_Click(object sender, RoutedEventArgs e)
        {
            CustomerInsertOpenWindow();

        }
        private void textChangedSelect(object sender, TextChangedEventArgs args)
        {
            CustomerGetByMultiple();

        }
        private void ButtonCustomerInsertOK_Click(object sender, RoutedEventArgs e)
        {
            CustomerInsert();

        }
        private void ButtonCustomerDelete_Click(object sender, RoutedEventArgs e)
        {
            CustomerDelete();
        }
        #endregion

        #region Fills

        private void SearchClear()
        {

            textboxCustomerNameSelect.Text = "";
            textboxTownSelect.Text = "";
            textboxPostcodeSelect.Text = "";
            TextboxClear();
            ComboboxClear();
            Customer.SearchClear();
      }
        private void TextboxClear()
        {
            textboxCustomerName.Text = "";
            textboxAddress1.Text = "";
            textboxAddress2.Text = "";
            textboxTown.Text = "";
            textboxPostcode.Text = "";
            textboxPhone.Text = "";
            textboxMobile.Text = "";
            textboxEmail.Text = "";
        }
        private void TextboxFillFromField()
        {

            textboxCustomerName.Text = Customer.CustomerName;
            textboxAddress1.Text = Customer.Address1;
            textboxAddress2.Text = Customer.Address2;
            textboxTown.Text = Customer.Town;
            textboxPostcode.Text = Customer.Postcode;
            textboxPhone.Text = Customer.Phone;
            textboxMobile.Text = Customer.Mobile;
            textboxEmail.Text = Customer.Email;
        }
        private void ComboboxClear()
        {
            comboboxCountryID.SelectedIndex = -1;
            comboboxCustomerTypeID.SelectedIndex = -1;
            comboboxCountryIDSearch.SelectedIndex = -1;
        }
        private void comboboxPopulate()
        {
            Customer.GetCountries();
            dtCountries = Customer.dtCountries;
            comboboxCountryID.ItemsSource = dtCountries.DefaultView;
            comboboxCountryID.DisplayMemberPath = "CountryName";
            comboboxCountryID.SelectedValuePath = "CountryID";
            comboboxCountryID.SelectedValue = Customer.CountryID;
            
            comboboxCountryIDSearch.ItemsSource = dtCountries.DefaultView;
            comboboxCountryIDSearch.DisplayMemberPath = "CountryName";
            comboboxCountryIDSearch.SelectedValuePath = "CountryID";

            Customer.GetCustomerTypes();
            dtCustomerTypes = Customer.dtCustomerTypes;
            comboboxCustomerTypeID.ItemsSource = dtCustomerTypes.DefaultView;
            comboboxCustomerTypeID.DisplayMemberPath = "CustomerTypeName";
            comboboxCustomerTypeID.SelectedValuePath = "CustomerTypeID";


        }
        private void ComboboxSet()
        {
            comboboxCountryID.SelectedValue = Customer.CountryID;
            comboboxCustomerTypeID.SelectedValue = Customer.CustomerTypeID;
        }
        private void ColumnFillFromField()
        {

            DataRowView drv = (DataRowView)dgCustomer.SelectedItem;
            drv["CustomerName"] = Customer.CustomerName;
            drv["Town"] = Customer.Town;
            drv["Postcode"] = Customer.Postcode;
            drv["Phone"] = Customer.Phone;
            drv["Mobile"] = Customer.Mobile;
            drv["Email"] = Customer.Email;
        }
        private void CustomerFieldFillFromTextbox()
        {
            Customer.CustomerName = textboxCustomerName.Text;
            Customer.CountryID = Convert.ToInt32(comboboxCountryID.SelectedValue);
            Customer.CustomerTypeID = Convert.ToInt32(comboboxCustomerTypeID.SelectedValue);
            Customer.Address1 = textboxAddress1.Text;
            Customer.Address2 = textboxAddress2.Text;
            Customer.Town = textboxTown.Text;
            Customer.Postcode = textboxPostcode.Text;
            Customer.Phone = textboxPhone.Text;
            Customer.Mobile = textboxMobile.Text;
            Customer.Email = textboxEmail.Text;
        }
        private void CustomerInsertOpenWindow()
        {

            if (blnInsertMode)
            {

                textboxCustomerName.IsEnabled = true;
                textboxAddress1.IsEnabled = true;
                textboxAddress2.IsEnabled = true;
                textboxTown.IsEnabled = true;
                textboxPostcode.IsEnabled = true;
                comboboxCountryID.IsEnabled = true;
                comboboxCustomerTypeID.IsEnabled = true;

                textboxCustomerName.Visibility = Visibility.Visible;
                textboxAddress1.Visibility = Visibility.Visible;
                textboxAddress2.Visibility = Visibility.Visible;
                textboxTown.Visibility = Visibility.Visible;
                textboxPostcode.Visibility = Visibility.Visible;
                comboboxCountryID.Visibility = Visibility.Visible;
                comboboxCustomerTypeID.Visibility = Visibility.Visible;
                buttonNew.Content = "New";
                blnInsertMode = false;

            }
            else
            {
 
                textboxCustomerName.IsEnabled = true;
                textboxAddress1.IsEnabled = false;
                textboxAddress2.IsEnabled = false;
                textboxTown.IsEnabled = false;
                textboxPostcode.IsEnabled = false;
                comboboxCountryID.IsEnabled = true;
                comboboxCustomerTypeID.IsEnabled = true;

                textboxCustomerName.Visibility = Visibility.Visible;
                textboxAddress1.Visibility = Visibility.Hidden;
                textboxAddress2.Visibility = Visibility.Hidden;
                textboxTown.Visibility = Visibility.Hidden;
                textboxPostcode.Visibility = Visibility.Hidden;
                comboboxCountryID.Visibility = Visibility.Visible;
                comboboxCustomerTypeID.Visibility = Visibility.Visible;
                buttonNew.Content = "Cancel";
                blnInsertMode = true;
            }
        }

        #endregion

        #region Gets

      private void CustomerGetByMultiple()
        {
            Customer.CustomerName = textboxCustomerNameSelect.Text;
            Customer.Town = textboxTownSelect.Text;
            Customer.Postcode = textboxPostcodeSelect.Text;
            int CountryIndex = Convert.ToInt32(comboboxCountryIDSearch.SelectedIndex);
            if (CountryIndex == -1) { Customer.CountryID = 0; } else { Customer.CountryID = Convert.ToInt32(comboboxCountryIDSearch.SelectedValue);}
            Customer.CustomerGetByMultiple();

        }
        private void RowFind()
        {
            int IDToFind = CustomerID;
            if (IDToFind > -1 && dgCustomer.ItemsSource is DataView)
            {
                foreach (DataRowView drv in (DataView)dgCustomer.ItemsSource)
                    if ((int)drv["CustomerID"] == IDToFind)
                    {

                        dgCustomer.SelectedItem = drv;
                        dgCustomer.ScrollIntoView(drv);

                    }
            }

        }
        #endregion

        #region Insert Update Delete

       private void CustomerInsert()
        {
            Customer.CustomerName = textboxCustomerName.Text;
            Customer.CountryID = Convert.ToInt32(comboboxCountryID.SelectedValue);
            Customer.CustomerTypeID = Convert.ToInt32(comboboxCustomerTypeID.SelectedValue);
            Customer.CustomerInsert();
            CustomerID = Customer.CustomerID;
            strCustomerID = CustomerID.ToString();
            RowFind();
            CustomerInsertOpenWindow();
        }

        private void CustomerUpdate() {

            CustomerFieldFillFromTextbox();
            Customer.CustomerUpdate();
            ColumnFillFromField();
        }

        private void CustomerDelete()
        {

            if (dgCustomer.SelectedItem != null) { Customer.CustomerDelete(); }
        }
        #endregion
 

 






    }
}
