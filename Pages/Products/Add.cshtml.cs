using GroceryCRUDWebApp.DataAccess;
using GroceryCRUDWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace GroceryCRUDWebApp.Pages.Products
{
    public class AddModel : PageModel
    {
       
        public int ProductId { get; set; }

        [BindProperty]
        [Display(Name = "ProductName")]
        [Required]
        public string ProductName { get; set; }
        [BindProperty]
        [Display(Name = "Cost")]
        [Required]
        public int Cost { get; set; }

        [BindProperty]
        [Required]
        public List<SelectListItem> CategoryList { get; set; }
        [BindProperty]
        [Display(Name = "Category")]
        public int SelectedCategoryId { get; set; }
         public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }
        public AddModel()
        {
            ProductName = "";
            Cost = 0;
            CategoryList = GetCategories();
        }

        public void OnGet()
        {
        }
        private List<SelectListItem> GetCategories()
        {
            var categoryDataaccess = new CategoryDataAccess();
            var categoryList = categoryDataaccess.GetAll();

            var categorySelectList = new List<SelectListItem>();
            foreach (var category in categoryList)
            {
                categorySelectList.Add(new SelectListItem
                {
                    Text = $"{category.CategoryName} - {category.StartingCost}",
                    Value = category.CategoryId.ToString(),
                });
            }
            return categorySelectList;
        }

        public void OnPost()
        {
            CategoryList = GetCategories();

            var productDataaccess = new ProductDataAccess();

            if (!ModelState.IsValid)
            {
                ErrorMessage = "Invalid data,Please Try Again";
                return;
            }
            var newProduct = new ProductDataModel { ProductName = ProductName, Cost = Cost, CategoryId = SelectedCategoryId };
            var insertedProduct = productDataaccess.Insert(newProduct);
            if (insertedProduct != null && insertedProduct.ProductId > 0)
            {
                SuccessMessage = $"Successfully Inserted Product {insertedProduct.ProductId}";
                ModelState.Clear();

            }
            else
            {
                ErrorMessage = $"Error! Add Failed.Please Try Again - {productDataaccess.ErrorMessage}";
            }


        }

    }
}
