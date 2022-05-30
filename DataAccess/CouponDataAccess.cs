using GroceryCRUDWebApp.Helpers;
using GroceryCRUDWebApp.Models;
using System.Data.SqlClient;

namespace GroceryCRUDWebApp.DataAccess
{
    public class CouponDataAccess
    {
        public string ErrorMessage { get; private set; }
        //GetAll Department
        public List<CouponDataModel> GetAll()
        {
            try
            {
                // ErrorMessage = string.Empty;
                ErrorMessage = "";
                List<CouponDataModel> coupons = new List<CouponDataModel>();
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();

                    var sqlStmt = "Select CouponId,CouponNumber,Discount from dbo.Coupon ";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read() == true)
                            {
                                CouponDataModel product = new CouponDataModel();

                                product.CouponId = reader.GetInt32(0);
                                product.CouponNumber = reader.GetInt32(1);
                                product.Discount= reader.GetInt32(2);
                               

                                coupons.Add(product);
                            }
                        }
                    }

                }
                return coupons;

            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;

            }
        }


    }
}
