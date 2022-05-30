using GroceryCRUDWebApp.Helpers;
using GroceryCRUDWebApp.Models;
using System.Data.SqlClient;

namespace GroceryCRUDWebApp.DataAccess
{
    public class CategoryDataAccess
    {
        public string ErrorMessage { get; private set; }
        //GetAll Department
        public List<CategoryDataModel> GetAll()
        {
            try
            {
                // ErrorMessage = string.Empty;
                ErrorMessage = "";
                List<CategoryDataModel> categories = new List<CategoryDataModel>();
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();

                    var sqlStmt = "Select CategoryId,CategoryName,StartingCost from dbo.Category ";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read() == true)
                            {
                                CategoryDataModel category = new CategoryDataModel();

                                category.CategoryId = reader.GetInt32(0);
                                category.CategoryName = reader.GetString(1);
                                category.StartingCost = reader.GetInt32(2);

                                categories.Add(category);
                            }
                        }
                    }

                }
                return categories;

            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;

            }
        }

    }
}
