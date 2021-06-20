using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Project_66_bit.Models;
using Project_66_bit.Pages.Auth;

namespace RazorProject.Pages
{
    public class RegisterModel : PageModel
    {
        public ApplicationDbContext db;
        
        public RegisterModel(ApplicationDbContext context)
        {
            db = context;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync(
            string name,
            string email,
            string password,
            string confirmPassword
            )
        {
            if (ModelState.IsValid)
            {
                User user = await db.Users.FirstOrDefaultAsync(u => u.Email == email);
                if (user == null)
                {
                    db.Users.Add(new User 
                    { 
                        Name = name,
                        Email = email,
                        Password = password 
                    });
                    await db.SaveChangesAsync();
 
                    var id = Authentication.Authenticate(email);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));

                    return Redirect("/");
                }
                else
                {
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
                }
            }
            
            return Redirect("/Register");
        }
    }
}
