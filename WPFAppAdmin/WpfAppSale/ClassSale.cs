using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace WpfAppSale
{
    class ClassSale
    {

        SqlConnection cnn;
        SqlDataAdapter da;
        SqlCommand cmd;

        public ClassSale() {

            cnn = new SqlConnection(@"Server=RichardWalker\SQLExpress;Database=ITDCRM;Trusted_Connection=True");
            cnn.Open();
            cmd = new SqlCommand();
            cmd.Connection = cnn;
            cmd.CommandType = CommandType.StoredProcedure;
            da = new SqlDataAdapter(cmd);
            da.SelectCommand = cmd;
            dtSale = new DataTable();
            dtSales = new DataTable();
            dtSaleItem = new DataTable();
            dtSaleItems = new DataTable();
            dtProduct = new DataTable();
            dtProducts = new DataTable();
            dtCustomer = new DataTable();
            dtVAT = new DataTable();
            dtSchema();
            CustomerGetAll();
            ProductGetAll();
        }

        public void dtSchema() {
            dtSale.Columns.Add("SaleID");
            dtSale.Columns.Add("CustomerID");
            dtSale.Columns.Add("CustomerName");
            dtSale.Columns.Add("SaleDate");

            dtSales.Columns.Add("SaleID");
            dtSales.Columns.Add("CustomerID");
            dtSales.Columns.Add("CustomerName");
            dtSales.Columns.Add("SaleDate");

            dtSaleItem.Columns.Add("SaleItemID");
            dtSaleItem.Columns.Add("SaleID");
            dtSaleItem.Columns.Add("ProductID");
            dtSaleItem.Columns.Add("ProductName");
            dtSaleItem.Columns.Add("Quantity");
            dtSaleItem.Columns.Add("UnitPrice");
            dtSaleItem.Columns.Add("TotalPrice");
            dtSaleItem.Columns.Add("VATRate");
            dtSaleItem.Columns.Add("VAT");
            dtSaleItem.Columns.Add("TotalPriceInclVAT");

            dtSaleItems.Columns.Add("SaleItemID");
            dtSaleItems.Columns.Add("SaleID");
            dtSaleItems.Columns.Add("ProductID");
            dtSaleItems.Columns.Add("ProductName");
            dtSaleItems.Columns.Add("Quantity");
            dtSaleItems.Columns.Add("UnitPrice");
            dtSaleItems.Columns.Add("TotalPrice");
            dtSaleItems.Columns.Add("VATRate");
            dtSaleItems.Columns.Add("VAT");
            dtSaleItems.Columns.Add("TotalPriceInclVAT");

            dtProduct.Columns.Add("ProductID");
            dtProduct.Columns.Add("ProductName");

            dtProducts.Columns.Add("ProductID");
            dtProducts.Columns.Add("ProductName");




        }

        public int SaleID { get; set; }
        public int SaleItemID { get; set; }
        public int CustomerID { get; set; }
        public int ProductID { get; set; }
        public string CustomerName { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public int VATID { get; set; }
        public decimal VATRate { get; set; }
        public decimal VAT { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal TotalPriceInclVAT { get; set; }
        public DateTime SaleDate { get; set; }

        public DataTable dtSale { get; set; }
        public DataTable dtSales { get; set; }
        public DataTable dtProduct { get; set; }
        public DataTable dtProducts { get; set; }
        public DataTable dtSaleItem { get; set; }
        public DataTable dtSaleItems { get; set; }
        public DataTable dtVAT {get;set;}
        public DataTable dtCustomer { get; set; }

        private void CustomerGetAll() 
        
        {
 
            if (cnn.State != ConnectionState.Open) { cnn.Open(); }
            cmd.CommandText = "CustomerGetAll";
            try {da.Fill(dtCustomer);}
            catch { }
            finally { cnn.Close(); }
        }

        private void ProductGetAll() {

            cmd.CommandText = "ProductGetAll";
            try
            {
                if (cnn.State != ConnectionState.Open) { cnn.Open(); }
                da.Fill(dtProducts);
            }
            catch { }
            finally { cnn.Close(); }
        }

        
        public void SaleGetByCustomerID() 
        {

            cmd.CommandText = "SaleGetByCustomerID";
            SqlParameter paramCustomerID = new SqlParameter("CustomerID", CustomerID);
            cmd.Parameters.Clear();
            cmd.Parameters.Add(paramCustomerID);
            dtSales.Rows.Clear();
            try {if (cnn.State != ConnectionState.Open){ cnn.Open(); }
                da.Fill(dtSales); }
            catch { }
            finally {cnn.Close();}
        


        }

        public void SaleGetBySaleID()
        {

            try
            {
                if (cnn.State != ConnectionState.Open) cnn.Open();
                cmd.CommandText = "SaleGetBySaleID";
                SqlParameter paramSaleID = new SqlParameter("SaleID", SaleID);
                cmd.Parameters.Clear();
                cmd.Parameters.Add(paramSaleID);
                dtSale.Rows.Clear();
                da.Fill(dtSale);

            }
            catch { }

            finally { cnn.Close(); }
            if (dtSale.Rows.Count > 0) {
                DataRow dr = dtSale.Rows[0];
 
            SaleDate = Convert.ToDateTime(dr["SaleDate"]);
            CustomerName = dr["CustomerName"].ToString();
            //Amount = Convert.ToInt32(dr["Amount"]);
        }          
        }

        public void SaleItemGetBySaleID() 
        
        {
            cmd.CommandText = "SaleItemGetBySaleID";
            SqlParameter paramSaleID = new SqlParameter("SaleID", SaleID);
            cmd.Parameters.Clear();
            cmd.Parameters.Add(paramSaleID);
            try
            {
                if (cnn.State != ConnectionState.Open) { cnn.Open(); }
                dtSaleItems.Rows.Clear();
                da.Fill(dtSaleItems);
            }
            catch { }
            finally { cnn.Close(); }
        }

        public void SaleItemGetBySaleItemID() 
        
        {
            cmd.CommandText = "SaleItemGetBySaleItemID";
            SqlParameter paramSaleItemID = new SqlParameter("SaleItemID", SaleItemID);
            cmd.Parameters.Clear();
            cmd.Parameters.Add(paramSaleItemID);

        }

        public void SaleInsert() {

            cmd.CommandText = "SaleInsert";
            SqlParameter paramSaleID = new SqlParameter("SaleID",0);
            paramSaleID.Direction = ParameterDirection.Output;
            SqlParameter paramCustomerID = new SqlParameter("CustomerID",CustomerID);
            SqlParameter paramSaleDate = new SqlParameter("SaleDate",SaleDate);
            cmd.Parameters.Clear();
            cmd.Parameters.Add(paramSaleID);
            cmd.Parameters.Add(paramCustomerID);
            cmd.Parameters.Add(paramSaleDate);
            try { if (cnn.State != ConnectionState.Open) cnn.Open();
                cmd.ExecuteNonQuery();
                SaleID = Convert.ToInt32(paramSaleID.Value);
                SaleGetBySaleID();
            }
            catch { }
            finally { cnn.Close(); }

            DataRow dr = dtSales.Rows.Add();
            dr["SaleID"] = SaleID;
            dr["SaleDate"] = SaleDate;
            dr["CustomerName"] = CustomerName;
        
        }

        public void SaleItemInsert() {
            ProductGetByProductID();
            cmd.CommandText = "SaleItemInsert";
            SqlParameter paramSaleItemID = new SqlParameter("SaleItemID",0);
            paramSaleItemID.Direction = ParameterDirection.Output;
            SqlParameter paramSaleID = new SqlParameter("SaleID", SaleID);
            SqlParameter paramProductID = new SqlParameter("ProductID", ProductID);
            SqlParameter paramQuantity = new SqlParameter("Quantity", Quantity);
            SqlParameter paramUnitPrice = new SqlParameter("UnitPrice", UnitPrice);
            TotalPrice = UnitPrice * Quantity;
            SqlParameter paramTotalPrice = new SqlParameter("TotalPrice", TotalPrice);
            SqlParameter paramVATRate = new SqlParameter("VATRate", VATRate);
            VAT = TotalPrice * VATRate;
            SqlParameter paramVAT = new SqlParameter("VAT", VAT);
            TotalPriceInclVAT = TotalPrice + VAT;
            SqlParameter paramTotalPriceInclVAT = new SqlParameter("TotalPriceInclVAT", TotalPriceInclVAT);
            cmd.Parameters.Clear();
            cmd.Parameters.Add(paramSaleID);
            cmd.Parameters.Add(paramProductID);
            cmd.Parameters.Add(paramSaleItemID);
            cmd.Parameters.Add(paramQuantity);
            cmd.Parameters.Add(paramUnitPrice);
            cmd.Parameters.Add(paramTotalPrice);
            cmd.Parameters.Add(paramVATRate);
            cmd.Parameters.Add(paramVAT);
            cmd.Parameters.Add(paramTotalPriceInclVAT);
            //try
            //{
                if (cnn.State != ConnectionState.Open) cnn.Open();
                cmd.ExecuteNonQuery();
                SaleItemID = Convert.ToInt32(paramSaleItemID.Value);
                SaleItemGetBySaleID();
            //}
            //catch { }
            //finally { cnn.Close(); }
        }

        public void SaleItemDelete() {

            string strSaleItemID = SaleItemID.ToString();
            //try
            //{
                if (cnn.State != ConnectionState.Open) cnn.Open();
                cmd.CommandText = "SaleItemDelete";
                SqlParameter paramSaleItemID = new SqlParameter("SaleItemID", SaleItemID);
                cmd.Parameters.Clear();
                cmd.Parameters.Add(paramSaleItemID);
                cmd.ExecuteNonQuery();
                DataRow[] foundRows = dtSaleItems.Select("SaleItemID = " + strSaleItemID);
                if (foundRows.GetLength(0) > 0) {
                if (foundRows[0] != null) {dtSaleItems.Rows.Remove(foundRows[0]); 
                dtSaleItems.AcceptChanges();}}           
            //}
            //catch { }

            //finally { cnn.Close(); }

 
        }

        private void ProductGetByProductID()
        
        {
            cmd.CommandText = "ProductGetByProductID";
            SqlParameter paramProductID = new SqlParameter("ProductID", ProductID);
            cmd.Parameters.Clear();
            cmd.Parameters.Add(paramProductID);
            try
            {
                if (cnn.State != ConnectionState.Open) cnn.Open();
                da.Fill(dtProduct);
                DataRow rowProduct = dtProduct.Rows[0];
                if (rowProduct["UnitPrice"]== null) UnitPrice = 0; else UnitPrice = Convert.ToDecimal(rowProduct["UnitPrice"]);
                if (rowProduct["VATID"] == null) VATID = 0; else VATID = Convert.ToInt32(rowProduct["VATID"]);
                VATGetByVATID();
                DataRow rowVAT = dtVAT.Rows[0];
                VATRate = Convert.ToDecimal(rowVAT["VATRate"]);

            }
            catch { }
            finally { cnn.Close(); }

        }

        private void VATGetByVATID() 
        
        {
            cmd.CommandText = "VATGetByVATID";
            SqlParameter paramVATID = new SqlParameter("VATID", VATID);
            cmd.Parameters.Clear();
            cmd.Parameters.Add(paramVATID);
            try
            {
                if (cnn.State != ConnectionState.Open) cnn.Open();
                da.Fill(dtVAT);
            }
            catch { }
            finally { cnn.Close(); }
        }

        public void SaleUpdate() {
            cmd.CommandText = "SaleUpdate";
            SqlParameter paramSaleID = new SqlParameter("SaleID", SaleID);
            SqlParameter paramAmount = new SqlParameter("Amount", 0);
            SqlParameter paramSaleDate = new SqlParameter("SaleDate", SaleDate);
            cmd.Parameters.Clear();
            cmd.Parameters.Add(paramSaleID);
            cmd.Parameters.Add(paramAmount);
            cmd.Parameters.Add(paramSaleDate);
            try
            {
                if (cnn.State != ConnectionState.Open) cnn.Open();
                cmd.ExecuteNonQuery();
                SaleGetBySaleID();
            }
            catch { }
            finally { cnn.Close(); }
        }

        public void SaleDelete() 
        {
            cmd.CommandText = "SaleDelete";
            SqlParameter paramSaleID = new SqlParameter("SaleID", SaleID);
            cmd.Parameters.Clear();
            cmd.Parameters.Add(paramSaleID);
            try 
            {
                if (cnn.State != ConnectionState.Open) cnn.Open();
                cmd.ExecuteNonQuery();
                string strSaleID = SaleID.ToString();
                DataRow[] foundRows = dtSales.Select("SaleID = " + strSaleID);
                dtSales.Rows.Remove(foundRows[0]);
                dtSales.AcceptChanges();
            }
            catch 
            { 
            
            }
            finally 
            {
                cnn.Close();
            }
        
        }
 
    }
}
