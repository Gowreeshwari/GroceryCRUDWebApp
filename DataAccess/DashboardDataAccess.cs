using GroceryCRUDWebApp.Helpers;
using GroceryCRUDWebApp.Models;
using System.Data.SqlClient;

namespace GroceryCRUDWebApp.DataAccess
{
    public class DashboardDataAccess
    {
        public string ErrorMessage { get; set; }
        public DashboardDataModel GetAll()
        {
            try
            {
                ErrorMessage = String.Empty;
                var d = new DashboardDataModel();
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    var sqlStmt = "select count(*) as Count from Customer";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        d.CountCustomer = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                    sqlStmt = "select count(*) as CategoryCount from Category";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        d.CategoryCount = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                    sqlStmt = "select count(*) as ProductCount from Product";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        d.ProductCount = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                    sqlStmt = "select count(*) as  CartCount from Cart ";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        d.CartCount = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }
                return d;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }
    }
}
