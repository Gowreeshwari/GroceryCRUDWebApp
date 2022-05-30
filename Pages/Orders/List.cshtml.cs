using GroceryCRUDWebApp.DataAccess;
using GroceryCRUDWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GroceryCRUDWebApp.Pages.Orders
{
    public class ListModel : PageModel
    {
        public List<OrderDataModel> Orders { get; set; }

        [BindProperty]
        public string SearchText { get; set; }

        public string ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }
        public ListModel()
        {
            SearchText = "";
            ErrorMessage = "";
            SuccessMessage = "";
            Orders = new List<OrderDataModel>();

        }


        public void OnGet()
        {
            var orderDataaccess = new OrderDataAccess();
            Orders = orderDataaccess.GetAll();
        }

        public void OnPostClear()
        {
            SearchText = "";
            ModelState.Clear();
            var orderDataaccess = new OrderDataAccess();
            Orders = orderDataaccess.GetAll();
        }





    }
}
