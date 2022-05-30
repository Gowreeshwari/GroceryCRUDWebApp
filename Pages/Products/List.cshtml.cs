using GroceryCRUDWebApp.DataAccess;
using GroceryCRUDWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GroceryCRUDWebApp.Pages.Products
{
    public class ListModel : PageModel
    {
        public List<ProductDataModel> Products { get; set; }

        [BindProperty]
        public string SearchText { get; set; }

        public string ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }
        public ListModel()
        {
            SearchText = "";
            ErrorMessage = "";
            SuccessMessage = "";
            Products = new List<ProductDataModel>();

        }


        public void OnGet()
        {
            var productDataaccess = new ProductDataAccess();
            Products = productDataaccess.GetAll();
        }
        public void OnPostSearch()
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



            ProductDataAccess productDataaccess = new ProductDataAccess();
            Products = productDataaccess.GetProductByName(SearchText);

            if (Products != null)
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
            var productDataaccess = new ProductDataAccess();
            Products = productDataaccess.GetAll();
        }
    }
}