using GroceryCRUDWebApp.DataAccess;
using GroceryCRUDWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace GroceryCRUDWebApp.Pages.Products
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

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
        [Display(Name = "CategoryId")]
        public int SelectedCategoryId { get; set; }
        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }
        public EditModel()
        {
            ProductName = "";
            Cost = 0;
            CategoryList = GetCategories();
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

        public void OnGet(int id)
        {
            Id = id;
            if (Id <= 0)
            {
                ErrorMessage = "Invalid Id";
                return;
            }
            var productDataaccess = new ProductDataAccess();
            var emp = productDataaccess.GetProductById(id);
            if (emp != null)
            {
                ProductName = emp.ProductName;
                Cost = emp.Cost;
                SelectedCategoryId = emp.CategoryId;
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
            CategoryList = GetCategories();

            var productDataaccess = new ProductDataAccess();
            var empToUpdate = new ProductDataModel { ProductId = Id, ProductName = ProductName, Cost = Cost, CategoryId = SelectedCategoryId };
            var updatedProduct = productDataaccess.Update(empToUpdate);
            if (updatedProduct != null)
            {
                SuccessMessage = $"Product{updatedProduct.ProductId} updated successfully";
            }
            else
            {
                ErrorMessage = $"Error! Updating Product";
            }
        }
    }
}



