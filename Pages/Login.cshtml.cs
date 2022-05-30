using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace GroceryCRUDWebApp.Pages
{
    public class LoginModel : PageModel
    {
        

        [BindProperty]

        public string UserName { get; set; }

        [BindProperty]
        [DataType(DataType.Password)]

        public string Password { get; set; }

        public string ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }
        public void OnGet()
        {
        }
        public async void OnPost()
        {
            if (!ModelState.IsValid)
            {
                ErrorMessage = "Invalid Login or Password";
                return;
            }
            if (UserName == "user1" && Password == "123456")
            {
                var userClaims = new List<Claim>()
                    {
                    new Claim("UserId","1"),
                    new Claim(ClaimTypes.Name,"User 1"),
                    new Claim(ClaimTypes.Role,"User")

                };
                var userIdentity = new ClaimsIdentity(userClaims, "User Identity");
                var userPrincipal = new ClaimsPrincipal(new[] { userIdentity });
                await HttpContext.SignInAsync(userPrincipal);


                Response.Redirect("/Index");
                return;
            }
            else if (UserName == "admin" && Password == "123456")
            {
                var userClaims = new List<Claim>()
                    {
                    new Claim("UserId","2"),
                    new Claim(ClaimTypes.Name,"Administrator"),
                    new Claim(ClaimTypes.Role,"Admin")

                };
                var userIdentity = new ClaimsIdentity(userClaims, "User Identity");
                var userPrincipal = new ClaimsPrincipal(new[] { userIdentity });
                await HttpContext.SignInAsync(userPrincipal);


                Response.Redirect("/Index");
                return;
            }

            ErrorMessage = "Invalid Login or Password";
        }
    }
}
