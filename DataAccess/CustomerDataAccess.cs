using GroceryCRUDWebApp.Helpers;
using GroceryCRUDWebApp.Models;
using System.Data.SqlClient;

namespace GroceryCRUDWebApp.DataAccess
{
    public class CustomerDataAccess
    {
        public string ErrorMessage { get; private set; }
        //GetAll Customers
        public List<CustomerDataModel> GetAll()
        {
            try
            {
                // ErrorMessage = string.Empty;
                ErrorMessage = "";
                List<CustomerDataModel> customers = new List<CustomerDataModel>();
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();

                    var sqlStmt = "Select CustomerId,Name,Email,PhoneNo,Password,ConfirmPassword from dbo.Customer ";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read() == true)
                            {
                                CustomerDataModel customer = new CustomerDataModel();

                                customer.CustomerId = reader.GetInt32(0);
                                customer.Name = reader.GetString(1);
                                customer.Email = reader.GetString(2);
                                customer.PhoneNo=reader.GetString(3);
                                customer.Password=reader.GetString(4);
                                customer.ConfirmPassword=reader.GetString(5);

                                customers.Add(customer);
                            }
                        }
                    }

                }
                return customers;

            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;

            }
        }

        //Get Department By Id
        public CustomerDataModel GetCustomersById(int id)
        {
            try
            {
                ErrorMessage = "";
                CustomerDataModel customer = null;
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    string SqlStmt = $"Select CustomerId,Name,Email,PhoneNo,Password,ConfirmPassword from dbo.Customer where Id={id}";
                    using (SqlCommand cmd = new SqlCommand(SqlStmt, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read() == true)
                            {
                                customer = new CustomerDataModel();
                                customer.CustomerId = reader.GetInt32(0);
                                customer.Name = reader.GetString(1);
                                customer.Email = reader.GetString(2);
                                customer.PhoneNo = reader.GetString(3);
                                customer.Password = reader.GetString(4);
                                customer.ConfirmPassword = reader.GetString(5);
                            }

                        }
                    }
                }
                return customer;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }

        //GetElementByName

        public List<CustomerDataModel> GetCustomersByName(string name, string email)
        {
            try
            {
                List<CustomerDataModel> customers = new List<CustomerDataModel>();
                ErrorMessage = "";
                CustomerDataModel customer = null;
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    string SqlStmt = $"Select CustomerId,Name,Email,PhoneNo,Password,ConfirmPassword from dbo.Customer where Name like'%{name}%' OR Email like '%{email}%'";

                    using (SqlCommand cmd = new SqlCommand(SqlStmt, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read() == true)
                            {
                               customer = new CustomerDataModel();
                                customer.CustomerId = reader.GetInt32(0);
                                customer.Name = reader.GetString(1);
                                customer.Email = reader.GetString(2);
                                customer.PhoneNo = reader.GetString(3);
                                customer.Password = reader.GetString(4);
                                customer.ConfirmPassword = reader.GetString(5);

                               customers.Add(customer);

                            }

                        }
                    }
                }
                return customers;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }

       
        //Update

        public CustomerDataModel Update(CustomerDataModel updCustomer)
        {
            try
            {
                ErrorMessage = string.Empty;
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    string sqlStmt = $"UPDATE dbo.Customer SET Name = '{updCustomer.Name}', " +
                        $"Email = '{updCustomer.Email}' ," +
                        $"PhoneNo='{updCustomer.PhoneNo}',"+
                        $"Password='{updCustomer.Password}',"+
                        $"ConfirmPassword='{updCustomer.ConfirmPassword}'"+
                        $"where CustomerId = {updCustomer.CustomerId}";

                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        int numOfRows = cmd.ExecuteNonQuery();

                        if (numOfRows > 0)
                        {
                            return updCustomer;
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


        //Delete Department

        public int Delete(int id)
        {
            try
            {
                ErrorMessage = string.Empty;
                int numOfRows = 0;
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    string sqlStmt = $"DELETE FROM Customer Where CustomerId={id}";
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
    }


}


