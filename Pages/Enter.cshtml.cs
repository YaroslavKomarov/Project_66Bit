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
    public class EnterModel : PageModel
    {
        public ApplicationDbContext db;

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
                    var id = Authentication.Authenticate(email);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));

                    return Redirect("/");
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }

            return Redirect("/Enter");
        }
    }
}
