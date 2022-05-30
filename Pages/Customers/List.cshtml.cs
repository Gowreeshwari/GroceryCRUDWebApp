using GroceryCRUDWebApp.DataAccess;
using GroceryCRUDWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GroceryCRUDWebApp.Pages.Customers
{
    public class ListModel : PageModel
    {
        public List<CustomerDataModel> Customers { get; set; }

        [BindProperty]
        public string SearchText { get; set; }

        public string ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }

        public ListModel()
        {
            SearchText = "";
            ErrorMessage = "";
            SuccessMessage = "";
            Customers = new List<CustomerDataModel>();
        }
        public void OnGet()
        {
            var customerDataaccess = new CustomerDataAccess();
            Customers = customerDataaccess.GetAll();
        }

        public void OnPost()
        {
            if (!ModelState.IsValid)
            {
                ErrorMessage = "Invalid Data";
                return;
            }
            if (string.IsNullOrEmpty(SearchText) || SearchText.Length < 3)
            {
                ErrorMessage = "Please Input more than 1 character";
                return;
            }
            CustomerDataAccess departmentData = new CustomerDataAccess();
            Customers = departmentData.GetCustomersByName(SearchText, SearchText);

            if (Customers != null)
            {
                SuccessMessage = "Searched Data Found";
                return;
            }


            else
            {
                ErrorMessage = "Error";
            }
        }



        public void OnPostClear()
        {
            SearchText = "";
            ModelState.Clear();

            var customerDataaccess = new CustomerDataAccess();
            Customers = customerDataaccess.GetAll();
        }
    }
}

