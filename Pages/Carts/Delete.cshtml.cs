using GroceryCRUDWebApp.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;


namespace GroceryCRUDWebApp.Pages.Carts
{
    [Authorize(Roles = "Admin,User")]

    public class DeleteModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        public bool ShowButton { get; set; }


       
        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }

        public DeleteModel()
        {
            
            SuccessMessage = "";
            ErrorMessage = "";
            ShowButton = true;
        }

        public void OnGet(int id)
        {
            Id = Id;

            if (Id <= 0)
            {
                ErrorMessage = "Invalid Id";
                return;
            }

            var cartDataaccess = new CartDataAccess();
            var emp = cartDataaccess.GetCartById(id);

            
        }

        public void OnPost()
        {
            if (!ModelState.IsValid)
            {
                ErrorMessage = "Invalid Data";
                return;
            }

            var cartDataaccess = new CartDataAccess();
            var numOfRows = cartDataaccess.Delete(Id);
            if (numOfRows > 0)
            {
                SuccessMessage = $"Product {Id} deleted successfully!";
                ShowButton = false;
            }
            else
            {
                ErrorMessage = $"Error! Unable to delete Product {Id}";
            }
        }
    }
}


  
