using System.Data.SqlClient;
using WebApplication2.Models;
namespace WebApplication2.Services
{
    public class ProductService
    {
        public static string db_source = "serveruage.database.windows.net";
        public static string db_user = "azureuser";
        public static string db_password = "user@1234";
        public static string db_database = "WebApp1";

        private SqlConnection GetConnection()
        {
             var _builder =new SqlConnectionStringBuilder();
            _builder.DataSource = db_source;
            _builder.UserID = db_user;
            _builder.Password = db_password;
            _builder.InitialCatalog = db_database;
            return new SqlConnection(_builder.ConnectionString);
        }
        public List<Product> GetProducts() 
        {
            SqlConnection conn=GetConnection();
            List<Product> _product_lst = new List<Product>();
            string statement = "SELECT ProductID,ProductName,Quantity From Products";
            conn.Open();
            SqlCommand cmd = new SqlCommand(statement, conn);
            using SqlDataReader reader = cmd.ExecuteReader();
            {
                while (reader.Read())
                {
                    Product _product = new Product()
                    {
                        ProductID = reader.GetInt32(0),
                        ProductName = reader.GetString(1),
                        Quantity = reader.GetInt32(2),
                    };
                    _product_lst.Add(_product);
                }
            }
            conn.Close();
            return _product_lst;

        }
    }
}
