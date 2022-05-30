using GroceryCRUDWebApp.Helpers;
using GroceryCRUDWebApp.Models;
using System.Data.SqlClient;

namespace GroceryCRUDWebApp.DataAccess
{
    public class CartDataAccess
    {
        public string ErrorMessage { get; set; }

        public CartDataAccess()
        {
            ErrorMessage = "";
        }

        public List<CartDataModel> GetAll()
        {
            try
            {
                ErrorMessage = string.Empty;
                List<CartDataModel> cartRecords = new List<CartDataModel>();
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    var sqlStmt = "select C.CartId as CartId,Cu.CustomerId as CustomerId,P.ProductId as ProductId,C.CouponId, C.Cost, C.NoofProducts,C.TotalAmount From dbo.Cart as C" +
                                    $" INNER JOIN[dbo].Customer AS Cu ON C.CustomerId = Cu.CustomerId " +
                                    $" INNER JOIN[dbo].Product AS P ON C.ProductId = P.ProductId ";

                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read() == true)
                            {
                                CartDataModel cartRecord = new CartDataModel();
                                cartRecord.CartId = reader.GetInt32(0);
                                cartRecord.CustomerId = reader.GetInt32(1);
                                cartRecord.ProductId = reader.GetInt32(2);
                                cartRecord.CouponId = reader.IsDBNull(3) ? null : reader.GetInt32(3);
                                cartRecord.Cost = reader.GetInt32(4);
                                cartRecord.NoofProducts = reader.GetInt32(5);
                                cartRecord.TotalAmount = reader.GetInt32(6);

                                cartRecords.Add(cartRecord);
                            }
                        }
                    }
                }
                return cartRecords;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }

        public bool Insert(CartDataModel newCart)
        {
            try
            {
                ErrorMessage = string.Empty;
                int idInserted = 0;
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    var couponId = newCart.CouponId == null ? "NULL" : newCart.CouponId.ToString();
                    string sqlStmt = $"INSERT INTO dbo.Cart (CustomerId, ProductId,CouponId, Cost, NoofProducts,TotalAmount) VALUES ({newCart.CustomerId}, {newCart.ProductId},{couponId}, {newCart.Cost}, {newCart.NoofProducts},{newCart.TotalAmount}); SELECT SCOPE_IDENTITY();";

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

        //Delete
        public int Delete(int id)
        {
            try
            {
                ErrorMessage = string.Empty;
                int numOfRows = 0;
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    string sqlStmt = $"DELETE FROM dbo.Cart Where CartId='{id}'";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        numOfRows = cmd.ExecuteNonQuery();
                    }
                }
                return numOfRows;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return 0;
            }

        }

        public int DeleteCart(int customerId)
        {
            try
            {
                ErrorMessage = string.Empty;
                int numOfRows = 0;
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    string sqlStmt = $"DELETE FROM dbo.Cart Where CustomerId='{customerId}'";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        numOfRows = cmd.ExecuteNonQuery();
                    }
                }
                return numOfRows;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return 0;
            }

        }

        public CartDataModel GetCartById(int id)
        {
            try
            {
                ErrorMessage = "";
                CartDataModel employee = null;
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    string SqlStmt = $"Select CartId,CustomerId,ProductId,CouponId, Cost, NoofProducts,TotalAmount from Cart where Id={id}";
                    using (SqlCommand cmd = new SqlCommand(SqlStmt, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read() == true)
                            {
                                employee = new CartDataModel();

                                employee.CartId = reader.GetInt32(0);
                                employee.CustomerId=reader.GetInt32(1);
                                employee.ProductId=reader.GetInt32(2);
                                employee.CouponId = reader.GetInt32(3);
                                employee.Cost = reader.GetInt32(4);
                                employee.NoofProducts=reader.GetInt32(5);
                                employee.TotalAmount=reader.GetInt32(6);
                             

                            }

                        }
                    }
                }
                return employee;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }


    }
}


