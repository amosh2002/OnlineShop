using OnlineShop.Models;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace DAL
{
    public class BaseDbProxy
    {
        public BaseDbProxy()
        {
            connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=OnlineShopDb";
            connection = new SqlConnection(connectionString);
        }
        public BaseDbProxy(string con)
        {
            connectionString = con;
        }
        string connectionString = null;
        protected SqlConnection connection = null;
        protected void Open()
        {
            connection.Open();
        }
        protected void Close()
        {
            connection.Close();
        }
        protected void Dispose()
        {
            connection.Dispose();
        }

    }
}
