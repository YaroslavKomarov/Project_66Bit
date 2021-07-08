using System;
using System.ComponentModel.DataAnnotations;
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

        [BindProperty]
        [Required(ErrorMessage ="Не указан Email")]
        public string Email { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [BindProperty]
        public bool RememberMe { get; set; }


        public EnterModel(ApplicationDbContext context)
        {
            db = context;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                User user = await db.Users.FirstOrDefaultAsync(u => u.Email == this.Email);

                if (user == null)
                {
                    return Redirect("/Enter");
                }

                string newHashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                        password: this.Password,
                        salt: Convert.FromBase64String(user.Salt),
                        prf: KeyDerivationPrf.HMACSHA1,
                        iterationCount: 10000,
                        numBytesRequested: 256 / 8
                ));

                if (newHashedPassword == user.Password)
                {
                    var id = Authentication.Authenticate(this.Email);
                    int daysToExpire = this.RememberMe ? 30 : 1;
                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(id),
                        new AuthenticationProperties
                        {
                            IsPersistent = true,
                            ExpiresUtc = DateTime.UtcNow.AddDays(daysToExpire)
                        });

                    return Redirect("/");
                }
            }

            return Redirect("/Enter");
        }
    }
}
