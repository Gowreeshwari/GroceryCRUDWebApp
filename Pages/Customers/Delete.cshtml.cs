using GroceryCRUDWebApp.DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GroceryCRUDWebApp.Pages.Customers
{
    [Authorize(Roles = "Admin")]
    public class DeleteModel : PageModel
    {
     
            [BindProperty(SupportsGet = true)]
            public int CustomerId { get; set; }

            public bool ShowButton { get; set; }

            public string Name { get; set; }

            public string SuccessMessage { get; set; }
            public string ErrorMessage { get; set; }

            public DeleteModel()
            {
                Name = "";
                SuccessMessage = "";
                ErrorMessage = "";
                ShowButton = true;
            }

            public void OnGet(int id)
            {
                CustomerId = CustomerId;

                if (CustomerId <= 0)
                {
                    ErrorMessage = "Invalid Id";
                    return;
                }

                var customerDataaccess = new CustomerDataAccess();
                var cus = customerDataaccess.GetCustomersById(id);

                if (cus != null)
                {
                    Name = cus.Name;
                }
                else
                {
                    ErrorMessage = "No Record found with that Id";
                }
            }

            public void OnPost()
            {
                if (!ModelState.IsValid)
                {
                    ErrorMessage = "Invalid Data";
                    return;
                }

                var customerDataaccess = new CustomerDataAccess();
                var numOfRows = customerDataaccess.Delete(CustomerId);
                if (numOfRows > 0)
                {
                    SuccessMessage = $"Customer {CustomerId} deleted successfully!";
                    ShowButton = false;
                }
                else
                {
                    ErrorMessage = $"Error! Unable to delete Customer {CustomerId}";
                }
            }
        }
}
