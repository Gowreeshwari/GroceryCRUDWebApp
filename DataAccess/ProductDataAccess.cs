using GroceryCRUDWebApp.Helpers;
using GroceryCRUDWebApp.Models;
using System.Data.SqlClient;

namespace GroceryCRUDWebApp.DataAccess
{
    public class ProductDataAccess
    {
        public string ErrorMessage { get; private set; }
        //GetAll Department
        public List<ProductDataModel> GetAll()
        {
            try
            {
                // ErrorMessage = string.Empty;
                ErrorMessage = "";
                List<ProductDataModel> products = new List<ProductDataModel>();
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();

                    var sqlStmt = "Select ProductId,ProductName,Cost,CategoryId from dbo.Product ";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read() == true)
                            {
                                ProductDataModel product = new ProductDataModel();

                                product.ProductId = reader.GetInt32(0);
                                product.ProductName = reader.GetString(1);
                                product.Cost = reader.GetInt32(2);
                                product.CategoryId = reader.GetInt32(3);

                                products.Add(product);
                            }
                        }
                    }

                }
                return products;

            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;

            }
        }



        public ProductDataModel GetProductById(int id)
        {
            try
            {
                ErrorMessage = "";
                ProductDataModel product = null;
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    string SqlStmt = $"Select ProductId,ProductName,Cost,CategoryId from Product where ProductId={id}";
                    using (SqlCommand cmd = new SqlCommand(SqlStmt, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read() == true)
                            {
                                product = new ProductDataModel();
                                product.ProductId = reader.GetInt32(0);
                                product.ProductName = reader.GetString(1);
                                product.Cost = reader.GetInt32(2);
                                product.CategoryId = reader.GetInt32(3);


                               

                            }

                        }
                    }
                }
                return product;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }

        public List<ProductDataModel> GetProductByName(string name)
        {
            try
            {
                List<ProductDataModel> products = new List<ProductDataModel>();

                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    string SqlStmt = $"Select ProductId,ProductName,Cost,CategoryId from Product where ProductName like'%{name}%'";

                    using (SqlCommand cmd = new SqlCommand(SqlStmt, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read() == true)
                            {
                                ProductDataModel product = new ProductDataModel();
                                product.ProductId = reader.GetInt32(0);
                                product.ProductName=reader.GetString(1);
                                product.Cost = reader.GetInt32(2);
                                product.CategoryId = reader.GetInt32(3);


                               products.Add(product);

                            }

                        }
                    }
                }
                return products;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }

        //Insert

        public ProductDataModel Insert(ProductDataModel newProduct)
        {
            try
            {
                ErrorMessage = string.Empty;
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    string sqlStmt = $"INSERT INTO dbo.Product ( ProductName,Cost,CategoryId) VALUES ( '{newProduct.ProductName}','{newProduct.Cost}',{newProduct.CategoryId});SELECT SCOPE_IDENTITY();";

                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        int idInserted = Convert.ToInt32(cmd.ExecuteScalar());
                        if (idInserted > 0)
                        {
                            newProduct.ProductId = idInserted;
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

        //Update

        public ProductDataModel Update(ProductDataModel updProduct)
        {
            try
            {
                ErrorMessage = string.Empty;
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    string sqlStmt = $"UPDATE dbo.Product SET ProductName = '{updProduct.ProductName}', " +
                        $" Cost='{updProduct.Cost}'," +
                         $" CategoryId= {updProduct.CategoryId} " +
                        $" where ProductId = {updProduct.ProductId}";

                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        int numOfRows = cmd.ExecuteNonQuery();

                        if (numOfRows > 0)
                        {
                            return updProduct;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;

            }
            return null;
        }

       


    }
}
