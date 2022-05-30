using GroceryCRUDWebApp.Helpers;
using GroceryCRUDWebApp.Models;
using System.Data.SqlClient;

namespace GroceryCRUDWebApp.DataAccess
{
    public class OrderDataAccess
    {
        public string ErrorMessage { get; private set; }
        //GetAll Department
        public List<OrderDataModel> GetAll()
        {
            try
            {
                // ErrorMessage = string.Empty;
                ErrorMessage = "";
                List<OrderDataModel> orders = new List<OrderDataModel>();
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();

                    var sqlStmt = "Select OrderId,CustomerId,OrderDate from [dbo].[Order]";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read() == true)
                            {
                                OrderDataModel order = new OrderDataModel();

                                order.OrderId = reader.GetInt32(0);
                                order.CustomerId = reader.GetInt32(1);
                                order.OrderDate = reader.GetDateTime(2);
                               

                                orders.Add(order);
                            }
                        }
                    }

                }
                return orders;

            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;

            }
        }

        public OrderDataModel Insert(OrderDataModel newProduct)
        {
            try
            {
                ErrorMessage = string.Empty;
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    string sqlStmt = $"INSERT INTO [dbo].[Order] (CustomerId,OrderDate) VALUES ( '{newProduct.CustomerId}','{newProduct.OrderDate.ToString("yyyy-MM-dd")}');SELECT SCOPE_IDENTITY();";

                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        int idInserted = Convert.ToInt32(cmd.ExecuteScalar());
                        if (idInserted > 0)
                        {
                            newProduct.OrderId = idInserted;
                            return newProduct;
                        }

                    }
                }
                return null;

            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.Message;
                return null;
            }
        }
    }
}
