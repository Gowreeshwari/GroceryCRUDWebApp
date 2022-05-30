using System.Data.SqlClient;

namespace GroceryCRUDWebApp.Helpers
{
    public class Database
    {
        public static SqlConnection GetConnection()
        {
            string connectionString = "Data Source=.\\SQLExpress; Initial Catalog=Grocery;Integrated Security=True;";
            return new SqlConnection(connectionString);
        }
    }
}
