using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace GroceryCRUDWebApp.Pages
{
    public class PaymentModel : PageModel
    {
        [BindProperty]
        [Required]
       public string Name { get; set; }
        [BindProperty]
        [Required]
        public string Address { get; set; }
        [BindProperty]
        [Required]
        public string City { get; set; }

        [BindProperty]
        [Required]
        public string State { get; set; }
        [BindProperty]
        [Required]
        public string Pincode { get; set; }


        public string ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }




    }
}
