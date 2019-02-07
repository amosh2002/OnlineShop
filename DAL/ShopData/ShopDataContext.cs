using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace DAL
{
    public class ShopDataContext : BaseDbProxy
    {
        public ShopDataContext() : base()
        {

        }

        public List<ProductModel> GetProducts()
        {
            List<ProductModel> products = new List<ProductModel>();
            string queryString = "SELECT Id, ProductName, ProductPrice, ProductPhotoURL from dbo.BaseTable";
            SqlCommand command = null;
            SqlDataReader reader = null;
            try
            {
                this.Open();
                command = new SqlCommand(queryString, this.connection);
                reader = command.ExecuteReader();
                products = reader.DataReaderMapToList<ProductModel>();
                reader.Close();
                command.Dispose();
                this.Close();
            }
            catch (Exception ex)
            {
                reader.Close();
                command.Dispose();
                this.Close();
                //log it
                Debug.WriteLine(ex.Message);
            }
            finally
            {
                this.Dispose();
            }
            return products;
        }

        public void InsertProduct(ProductModel product)
        {
            string query = "INSERT INTO dbo.BaseTable (ProductName, ProductPrice, ProductPhotoURL) VALUES (@ProductName, @ProductPrice, @ProductPhotoURL)";

            try
            {
                using (SqlCommand cmd = new SqlCommand(query, this.connection))
                {
                    cmd.Parameters.Add("@ProductName", SqlDbType.NVarChar, 50).Value = product.ProductName;
                    cmd.Parameters.Add("@ProductPrice", SqlDbType.Decimal, 20).Value = product.ProductPrice;
                    cmd.Parameters.Add("@ProductPhotoURL", SqlDbType.NChar, 1000).Value = product.ProductPhotoURL;
                    this.Open();
                    cmd.ExecuteNonQuery();
                    this.Close();
                }
            }
            catch (Exception ex)
            {

                Debug.Write(ex.Message);
                this.Close();
            }
            finally
            {
                this.Dispose();
            }

        }
    }
}
