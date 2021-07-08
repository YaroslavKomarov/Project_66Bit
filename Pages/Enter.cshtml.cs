using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
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
                User user = await db.Users.FirstOrDefaultAsync(u => u.Email == email);

                if (user == null)
                {
                    return Redirect("/Enter");
                }

                string newHashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                        password: password,
                        salt: Convert.FromBase64String(user.Salt),
                        prf: KeyDerivationPrf.HMACSHA1,
                        iterationCount: 10000,
                        numBytesRequested: 256 / 8
                ));

                if (newHashedPassword == user.Password)
                {
                    var id = Authentication.Authenticate(email);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));

                    return Redirect("/");
                }
            }

            return Redirect("/Enter");
        }
    }
}
