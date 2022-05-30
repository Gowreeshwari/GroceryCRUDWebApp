using GroceryCRUDWebApp.DataAccess;
using GroceryCRUDWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace GroceryCRUDWebApp.Pages.Customers
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int CustomerId { get; set; }

        [BindProperty]
        [Display(Name = "Name")]
        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string Name { get; set; }

        [BindProperty]
        [Display(Name = "Email")]
        [Required]
        
        public string Email { get; set; }
        [BindProperty]
        [Display(Name ="PhoneNo")]
        [Required]
        [MaxLength(10)]
       public string PhoneNo { get; set; }
        [BindProperty]
        [Display(Name ="Password")]
        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        [BindProperty]
        [Display(Name = "ConfirmPassword")]
        [Required]
        [MinLength(6)]
        public string ConfirmPassword { get; set; }

        public string SuccessMessage { get; set; }

        public string ErrorMessage { get; set; }

        public EditModel()
        {
            Name = "";
            Email = "";
            PhoneNo = "";
            Password = "";
            ConfirmPassword = "";
            SuccessMessage = "";
            ErrorMessage = "";
        }

        public void OnGet(int id)
        {
            CustomerId = id;
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
               Email =cus.Email;
                PhoneNo = cus.PhoneNo;
                Password = cus.Password;
                ConfirmPassword = cus.ConfirmPassword;
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
                ErrorMessage = "Invalid Data.Please correct and Try again";
                return;
            }
            var customerDataaccess = new CustomerDataAccess();
            var cusToUpdate = new CustomerDataModel {CustomerId=CustomerId, Name = Name,Email=Email,PhoneNo=PhoneNo,Password=Password,ConfirmPassword=ConfirmPassword};
            var updatedCustomer = customerDataaccess.Update(cusToUpdate);
            if (updatedCustomer != null)
            {
                SuccessMessage = $"Customer {updatedCustomer.CustomerId} updated successfully";
            }
            else
            {
                ErrorMessage = $"Error! Updating Customer";
            }
        }
    }
}
