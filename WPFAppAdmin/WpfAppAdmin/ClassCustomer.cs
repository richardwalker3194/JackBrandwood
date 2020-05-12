using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace WpfAppAdmin
{
    class ClassCustomer
    {
        SqlConnection cnn;
        SqlDataAdapter da;
        SqlCommand cmd;
        SqlDataReader dr;
        public ClassCustomer()
        {
            cnn = new SqlConnection(@"Server=RichardWalker\SQLExpress;Database=ITDCRM;Trusted_Connection=True");
            cnn.Open();
            cmd = new SqlCommand();
            cmd.Connection = cnn;
            cmd.CommandType = CommandType.StoredProcedure;
            da = new SqlDataAdapter(cmd);
            da.SelectCommand = cmd;
            Customer = new DataTable();
            Customers = new DataTable();
            dtSchema();

        }
        public int CustomerID { get; set; }
        public int CountryID { get; set; }
        public int CustomerTypeID { get; set; }
        public string CustomerName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Town { get; set; }
        public string Postcode { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Web { get; set; }
        public string Skype { get; set; }
        public DataTable Customer { get; set; }
        public DataTable Customers { get; set; }
        public DataTable dtCountries { get; set; }
        public DataTable dtCustomerTypes { get; set; }

        private void dtSchema() 
        {
            Customers.Columns.Add("CustomerID", typeof(int));
            Customers.Columns.Add("CustomerName", typeof(string));
            Customers.Columns.Add("CustomerTypeID", typeof(int));
            Customers.Columns.Add("Address1", typeof(string));
            Customers.Columns.Add("Address2", typeof(string));
            Customers.Columns.Add("Town", typeof(string));
            Customers.Columns.Add("Postcode", typeof(string));
            Customers.Columns.Add("CountryID", typeof(int));
            Customers.Columns.Add("Phone", typeof(string));
            Customers.Columns.Add("Mobile", typeof(string));
            Customers.Columns.Add("Email", typeof(string));
            Customers.Columns.Add("Skype", typeof(string));
            Customers.Columns.Add("Web", typeof(string));

            Customer.Columns.Add("CustomerID", typeof(int));
            Customer.Columns.Add("CustomerName", typeof(string));
            Customer.Columns.Add("CustomerTypeID", typeof(int));
            Customer.Columns.Add("Address1", typeof(string));
            Customer.Columns.Add("Address2", typeof(string));
            Customer.Columns.Add("Town", typeof(string));
            Customer.Columns.Add("Postcode", typeof(string));
            Customer.Columns.Add("CountryID", typeof(int));
            Customer.Columns.Add("Phone", typeof(string));
            Customer.Columns.Add("Mobile", typeof(string));
            Customer.Columns.Add("Email", typeof(string));
            Customer.Columns.Add("Skype", typeof(string));
            Customer.Columns.Add("Web", typeof(string));
        }

        public void GetCountries()
        {

            cmd.CommandText = "CountryGetAll";
            dtCountries = new DataTable();
            cmd.Parameters.Clear();
            da.Fill(dtCountries);
        }

        public void GetCustomerTypes()
        {

            cmd.CommandText = "CustomerTypeGetAll";
            dtCustomerTypes = new DataTable();
            cmd.Parameters.Clear();
            da.Fill(dtCustomerTypes);

        }

        public void CustomerGetByMultiple()
        {

            cmd.CommandText = "CustomerGetByMultiple";
            SqlParameter paramCustomerName = new SqlParameter("CustomerName", CustomerName);
            SqlParameter paramTown = new SqlParameter("Town", Town);
            SqlParameter paramPostcode = new SqlParameter("Postcode", Postcode);
            SqlParameter paramCountryID = new SqlParameter("CountryID", CountryID);
            cmd.Parameters.Clear();
            cmd.Parameters.Add(paramCustomerName);
            cmd.Parameters.Add(paramTown);
            cmd.Parameters.Add(paramPostcode);
            cmd.Parameters.Add(paramCountryID);
            
            Customers.Rows.Clear();
            da.Fill(Customers);
        }

        public void CustomerGetByCustomerID()
        {
            cmd.CommandText = "CustomerGetByCustomerID";
            SqlParameter paramCustomerID = new SqlParameter("CustomerID", CustomerID);
            cmd.Parameters.Clear();
            cmd.Parameters.Add(paramCustomerID);
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                string value;
                while (dr.Read())
                {
                    value = dr["CustomerName"].ToString();
                    if (value == null) { CustomerName = ""; } else { CustomerName = value; }
                    value = dr["CustomerTypeID"].ToString();
                    if (value == null) { CustomerTypeID = 0; } else { CustomerTypeID = Convert.ToInt32(value); }
                    value = dr["CountryID"].ToString();
                    if (value == null) { CountryID = 0; } else { CountryID = Convert.ToInt32(value); }
                    value = dr["Address1"].ToString();
                    if (value == null) { Address1 = ""; } else { Address1 = value; }
                    value = dr["Address2"].ToString();
                    if (value == null) { Address2 = ""; } else { Address2 = value; }
                    value = dr["Town"].ToString();
                    if (value == null) { Town = ""; } else { Town = value; }
                    value = dr["Postcode"].ToString();
                    if (value == null) { Postcode = ""; } else { Postcode = value; }
                    value = dr["Phone"].ToString();
                    if (value == null) { Phone = ""; } else { Phone = value; }
                    value = dr["Mobile"].ToString();
                    if (value == null) { Mobile = ""; } else { Mobile = value; }
                    value = dr["Email"].ToString();
                    if (value == null) { Email = ""; } else { Email = value; }
                    value = dr["Web"].ToString();
                    if (value == null) { Web = ""; } else { Web = value; }
                    value = dr["Skype"].ToString();
                    if (value == null) { Skype = ""; } else { Skype = value; }
                }
                dr.Close();

            }
        }

        public void CustomerInsert()
        {

            SqlParameter paramCustomerID = new SqlParameter("CustomerID", CustomerID);
            paramCustomerID.Direction = ParameterDirection.Output;
            SqlParameter paramCountryID = new SqlParameter("CountryID", CountryID);
            SqlParameter paramCustomerTypeID = new SqlParameter("CustomerTypeID", CustomerTypeID);
            SqlParameter paramCustomerName = new SqlParameter("CustomerName", CustomerName);
            cmd.Parameters.Clear();
            cmd.Parameters.Add(paramCustomerID);
            cmd.Parameters.Add(paramCountryID);
            cmd.Parameters.Add(paramCustomerTypeID);
            cmd.Parameters.Add(paramCustomerName);
            cmd.CommandText = "CustomerInsert";
            cmd.ExecuteNonQuery();
            CustomerID = Convert.ToInt32(paramCustomerID.Value);
            DataRow dr = Customers.Rows.Add();
            dr["CustomerID"] = CustomerID;
            dr["CustomerName"] = CustomerName;
            dr["CountryID"] = CountryID;
            dr["CustomerTypeID"] = CustomerTypeID;
        }
        public void CustomerUpdate()
        {
            SqlParameter paramCustomerID = new SqlParameter("CustomerID", CustomerID);
            SqlParameter paramCountryID = new SqlParameter("CountryID", CountryID);
            SqlParameter paramCustomerTypeID = new SqlParameter("CustomerTypeID", CustomerTypeID);
            SqlParameter paramCustomerName = new SqlParameter("CustomerName", CustomerName);
            SqlParameter paramAddress1 = new SqlParameter("Address1", Address1);
            SqlParameter paramAddress2 = new SqlParameter("Address2", Address2);
            SqlParameter paramTown = new SqlParameter("Town", Town);
            SqlParameter paramPostcode = new SqlParameter("Postcode", Postcode);
            SqlParameter paramPhone = new SqlParameter("Phone", Phone);
            SqlParameter paramMobile = new SqlParameter("Mobile", Mobile);
            SqlParameter paramEmail = new SqlParameter("Email", Email);
            SqlParameter paramWeb = new SqlParameter("Web", Web);
            SqlParameter paramSkype = new SqlParameter("Skype", Skype);
            cmd.Parameters.Clear();
            cmd.Parameters.Add(paramCustomerID);
            cmd.Parameters.Add(paramCountryID);
            cmd.Parameters.Add(paramCustomerTypeID);
            cmd.Parameters.Add(paramCustomerName);
            cmd.Parameters.Add(paramAddress1);
            cmd.Parameters.Add(paramAddress2);
            cmd.Parameters.Add(paramTown);
            cmd.Parameters.Add(paramPostcode);
            cmd.Parameters.Add(paramPhone);
            cmd.Parameters.Add(paramMobile);
            cmd.Parameters.Add(paramEmail);
            cmd.Parameters.Add(paramWeb);
            cmd.Parameters.Add(paramSkype);
            cmd.CommandText = "CustomerUpdate";
            cmd.ExecuteNonQuery();
        }
        public void CustomerDelete()
        {

            string strCustomerID = CustomerID.ToString();
            cmd.CommandText = "CustomerDelete";
            cmd.Parameters.Clear();
            SqlParameter paramCustomerID = new SqlParameter("CustomerID", CustomerID);
            cmd.Parameters.Add(paramCustomerID);
            cmd.ExecuteNonQuery();
            DataRow[] foundRows = Customers.Select("CustomerID = " + strCustomerID);
            Customers.Rows.Remove(foundRows[0]);
            Customers.AcceptChanges();

        }

        public void SearchClear()
        {
            Customers.Rows.Clear();
        }
    }
}
