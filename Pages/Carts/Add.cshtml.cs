using GroceryCRUDWebApp.DataAccess;
using GroceryCRUDWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace GroceryCRUDWebApp.Pages.Carts
{
    [Authorize(Roles = "Admin,User")]

    public class AddModel : PageModel
    {
        public int CartId { get; set; }

        [BindProperty]
        [Display(Name = "Customer")]
        public int CustomerId { get; set; }
        public List<SelectListItem> CustomerList { get; set; }
        [BindProperty]
        [Display(Name = "Customer")]
        public int SelectedCustomerId { get; set; }

        [BindProperty]
        [Display(Name = "Product")]

        public int ProductId { get; set; }
        public List<SelectListItem> ProductList { get; set; }
        [BindProperty]
        [Display(Name = "Product")]
        public int SelectedProductId { get; set; }


       /* [BindProperty]
        [Display(Name = "Coupon")]

        public int CouponNumber { get; set; }


        [BindProperty]
        [Display(Name = "Cost")]
        [Required]
        public int Cost { get; set; }*/

        [BindProperty]
        [Display(Name = "NoofProducts")]
        [Required]
        public int NoofProducts { get; set; }
        [BindProperty]
        [Display(Name = "TotalAmount")]
        public int TotalAmount { get; set; }

        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }


        public AddModel()
        {
           
            NoofProducts = 0;
            TotalAmount = 0;
            CustomerList = GetCustomers();
            ProductList = GetProducts();

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
                    Text = $"{customer.Name}",
                    Value = customer.CustomerId.ToString(),
                });
            }
            return customerSelectList;
        }
        private List<SelectListItem> GetProducts()
        {
            var productDataaccess = new ProductDataAccess();
            var productList = productDataaccess.GetAll();

            var productSelectList = new List<SelectListItem>();
            foreach (var product in productList)
            {
                productSelectList.Add(new SelectListItem
                {
                    Text = $"{product.ProductName} / {product.Cost}",
                    Value = product.ProductId.ToString(),
                });
            }
            return productSelectList;
        }

        public void OnPost()
        {
            CustomerList = GetCustomers();
            ProductList = GetProducts();

            if (!ModelState.IsValid)
            {
                ErrorMessage = "Invalid Data";
                return;
            }


            /* //Data Access
             var cartDataaccess = new CartDataAccess();
             var result = cartDataaccess.Insert(SelectedCustomerId,ProductId,CouponId,Cost,NoofProducts, TotalAmount);
 */
            var productDataAccess = new ProductDataAccess();
            var product = productDataAccess.GetProductById(SelectedProductId);
            var cartDataaccess = new CartDataAccess();
            var newCart = new CartDataModel
            {
                CustomerId = SelectedCustomerId,
                ProductId = SelectedProductId,
                CouponId =  null,
                Cost = product.Cost,
                NoofProducts = NoofProducts,
                TotalAmount = product.Cost * NoofProducts
            };
            var result = cartDataaccess.Insert(newCart);
            //Check Result
            if (result)
            {
                SuccessMessage = "Successfully Inserted!";
                ErrorMessage = "";
               // Response.Redirect("Carts/List");
            }
            else
            {
                ErrorMessage = $"Error adding Product - {cartDataaccess.ErrorMessage}";
                SuccessMessage = "";
            }
        }

    }
}
