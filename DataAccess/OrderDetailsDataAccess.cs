using GroceryCRUDWebApp.Helpers;
using GroceryCRUDWebApp.Models;
using System.Data.SqlClient;

namespace GroceryCRUDWebApp.DataAccess
{
    public class OrderDetailsDataAccess
    {
        public string ErrorMessage { get; private set; }

        public bool Insert(int OrderId, int ProductId,int Quantity)
        {
            try
            {
                ErrorMessage = string.Empty;
                int idInserted = 0;
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    string sqlStmt = $"INSERT INTO dbo.OrderDetails (OrderId,ProductId,Quantity) VALUES ({OrderId}, {ProductId},{Quantity}); SELECT SCOPE_IDENTITY();";

                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        idInserted = Convert.ToInt32(cmd.ExecuteScalar());
                        if (idInserted > 0)
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.Message;
                return false;
            }
        }

        //Get By Id

        public OrderDetailsDataModel GetOrderById(int id)
        {
            try
            {
                ErrorMessage = "";
                OrderDetailsDataModel order = null;
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    string SqlStmt = $"Select OrderId,ProductId,Quantity from AddCart where CustomerId={id}";
                    using (SqlCommand cmd = new SqlCommand(SqlStmt, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read() == true)
                            {
                                order = new OrderDetailsDataModel();

                                order.Id = reader.GetInt32(0);
                                order.OrderId = reader.GetInt32(1);
                                order.ProductId = reader.GetInt32(2);
                                order.Quantity = reader.GetInt32(3);

                            }

                        }
                    }
                }
                return order;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }
    }
}
