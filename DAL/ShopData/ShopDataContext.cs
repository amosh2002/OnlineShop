using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading.Tasks;

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

        public void DeleteProduct(int productId)
        {
            string query = "DELETE FROM dbo.BaseTable WHERE Id = @productId";
            try
            {
                using (SqlCommand cmd = new SqlCommand(query, this.connection))
                {
                    this.Open();
                    cmd.Parameters.AddWithValue("@productId", productId);
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

        public bool ProductExists(int id)
        {
            bool prd = false;
            var query = "SELECT * FROM dbo.BaseTable WHERE Id = @id";
            using (SqlCommand cmd = new SqlCommand(query, this.connection))
            {
                this.Open();
                if (cmd.Parameters != null)
                {
                    prd = true;
                }
                cmd.ExecuteNonQuery();
                this.Close();
            }

            return prd;
        }

        public ProductModel FindProduct(int id)
        {
            List<ProductModel> prods = null;
            SqlDataReader reader = null;

            var query = "SELECT * FROM dbo.BaseTable WHERE Id = @id";
            try
            {
                using (SqlCommand cmd = new SqlCommand(query, this.connection))
                {
                    this.Open();
                    cmd.Parameters.AddWithValue("@id", id);
                    reader = cmd.ExecuteReader();
                    prods = reader.DataReaderMapToList<ProductModel>();
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

            return prods[0];
        }

        public void SaveUpdatedProduct(ProductModel product)
        {
            int ekacId = product.Id;

            string query = "UPDATE dbo.BaseTable SET ProductName = @ProductName, ProductPrice = @ProductPrice, ProductPhotoURL = @ProductPhotoURL WHERE Id = @Id";

            try
            {
                using (SqlCommand cmd = new SqlCommand(query, this.connection))
                {

                    this.Open();
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = ekacId;
                    cmd.Parameters.Add("@ProductName", SqlDbType.NVarChar, 50).Value = product.ProductName;
                    cmd.Parameters.Add("@ProductPrice", SqlDbType.Decimal, 20).Value = product.ProductPrice;
                    cmd.Parameters.Add("@ProductPhotoURL", SqlDbType.NChar, 1000).Value = product.ProductPhotoURL;

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
