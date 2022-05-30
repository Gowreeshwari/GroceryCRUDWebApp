using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace GroceryCRUDWebApp.Pages
{
    public class RegistrationModel : PageModel
    {
        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }
        [BindProperty]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [BindProperty]
        [Display(Name="Email")]
        public string Email { get; set; }
        [BindProperty]
        [Display(Name ="PhoneNo")]
        public string PhoneNo { get; set; }

    }
}
