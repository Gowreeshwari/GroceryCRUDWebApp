using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;


namespace GroceryCRUDWebApp.Pages.Carts
{
    [Authorize(Roles = "Admin,User")]

    public class EditModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
