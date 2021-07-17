using System;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Project_66_bit.Models;
using Project_66_bit.Services.Auth;

namespace RazorProject.Pages
{
    public class RegisterModel : PageModel
    {
        public ApplicationDbContext db;
        public Authentication auth;

        [BindProperty]
        private string Name { get; set; }
        [BindProperty]
        private string Email { get; set; }
        [BindProperty]
        private string Password { get; set; }
        [BindProperty]
        private string ConfirmPassword { get; set; }
        
        public RegisterModel(ApplicationDbContext context, Authentication auth)
        {
            db = context;
            this.auth = auth;
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
                    byte[] salt = new byte[128 / 8];
                    using (var rng = RandomNumberGenerator.Create())
                    {
                        rng.GetBytes(salt);
                    }

                    string hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                        password: this.Password,
                        salt: salt,
                        prf: KeyDerivationPrf.HMACSHA1,
                        iterationCount: 10000,
                        numBytesRequested: 256 / 8
                    ));

                    db.Users.Add(new User 
                    { 
                        Name = this.Name,
                        Email = this.Email,
                        Password = hashedPassword,
                        Salt = Convert.ToBase64String(salt)
                    });
                    await db.SaveChangesAsync();
 
                    var id = auth.Authenticate(this.Email);
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
