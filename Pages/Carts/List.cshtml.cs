using GroceryCRUDWebApp.DataAccess;
using GroceryCRUDWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GroceryCRUDWebApp.Pages.Carts
{
    [Authorize(Roles = "Admin,User")]
    public class ListModel : PageModel
    {

        public List<CartDataModel> CartRecords { get; set; }
        public void OnGet()
        {
            var cartDataaccess = new CartDataAccess();
            CartRecords = cartDataaccess.GetAll();
        }
    }
}
