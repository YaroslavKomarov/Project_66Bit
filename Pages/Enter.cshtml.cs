using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Project_66_bit.Models;

namespace RazorProject.Pages
{
    public class EnterModel : PageModel
    {
        public ApplicationDbContext db;

        public string Email { get; set; }
        public string Password { get; set; }
        
        public EnterModel(ApplicationDbContext context)
        {
            db = context;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync(string email, string password)
        {
            if (ModelState.IsValid)
            {
                User user = await db.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
                if (user != null)
                {
                    await Authenticate(email);
 
                    return Redirect("/");
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }

            return Redirect("/Enter");
        }

        private async Task Authenticate(string userName)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }
}
