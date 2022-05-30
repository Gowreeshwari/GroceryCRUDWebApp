using GroceryCRUDWebApp.DataAccess;
using GroceryCRUDWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace GroceryCRUDWebApp.Pages.Orders
{
    public class AddModel : PageModel
    {
        public int OrderId { get; set; }
        [BindProperty]
        [Display(Name = "OrderDate")]
        [Required]
        [DataType(DataType.Date)]
        public DateTime OrderDate { get; set; }
        [BindProperty]
        [Required]
        public List<SelectListItem> CustomerList { get; set; }
        [BindProperty]
        [Display(Name = "Customer")]
        public int SelectedCustomerId { get; set; }
        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }
        public AddModel()
        {
            
            OrderDate = DateTime.Now;
            CustomerList = GetCustomers();
        }
        private List<SelectListItem> GetCustomers()
        {
            var customerDataaccess = new CustomerDataAccess();
            var customerList = customerDataaccess.GetAll();

            var customerSelectList = new List<SelectListItem>();
            foreach (var customer in customerList)
            {
                customerSelectList.Add(new SelectListItem
                {
                    Text = $"{customer.CustomerId}-{customer.Name}",
                    Value = customer.CustomerId.ToString(),
                });
            }
            return customerSelectList;
        }
        public void OnPost()
        {
            CustomerList = GetCustomers();

            var orderDataaccess = new OrderDataAccess();

            if (!ModelState.IsValid)
            {
                ErrorMessage = "Invalid data,Please Try Again";
                return;
            }
            var newProduct = new OrderDataModel { CustomerId=SelectedCustomerId, OrderDate=OrderDate };
            var insertedProduct = orderDataaccess.Insert(newProduct);
            if (insertedProduct != null && insertedProduct.OrderId > 0)
            {
                SuccessMessage = $"Successfully Ordered {insertedProduct.OrderId}";
                ModelState.Clear();

            }
            else
            {
                ErrorMessage = $"Error! Add Failed.Please Try Again - {orderDataaccess.ErrorMessage}";
            }


        }


    }
}
