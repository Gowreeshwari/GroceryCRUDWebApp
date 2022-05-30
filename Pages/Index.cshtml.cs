using GroceryCRUDWebApp.DataAccess;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GroceryCRUDWebApp.Pages
{
    public class IndexModel : PageModel
    {
        public int CountCustomer { get; set; }
        public int ProductCount { get; set; }
        public int CartCount { get; set; }
        public int CategoryCount { get; set; }

        
        private readonly ILogger<IndexModel> _logger;

        public string ErrorMessage { get; set; }
        [FromQuery(Name = "action")]
        public string Action { get; set; }
        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
            ErrorMessage = "";
            CategoryCount = 0;
            CartCount = 0;
            CountCustomer = 0;
            ProductCount = 0;
        }

        public void OnGet()
        {

            if (!String.IsNullOrEmpty(Action) && Action.ToLower() == "logout")
            {
                Logout();
                return;
            }
            var dashboardDataaccess = new DashboardDataAccess();
            var dashboard = dashboardDataaccess.GetAll();
            if (dashboard != null)
            {
                CountCustomer = dashboard.CountCustomer;
                CategoryCount = dashboard.CategoryCount;
                ProductCount = dashboard.ProductCount;
                CartCount = dashboard.CartCount;
                
            }
            else
            {
                ErrorMessage = $"No Dashboard Data Available - {dashboardDataaccess.ErrorMessage}";
            }    
        }
      
        public void OnPost()
        {
            Logout();
        }
        private void Logout()
        {
            HttpContext.SignOutAsync();
            Response.Redirect("/Index");
        }
    }
}