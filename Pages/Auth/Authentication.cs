using System.Collections.Generic;
using System.Security.Claims;

namespace Project_66_bit.Pages.Auth
{
    public static class Authentication
    {
        public static ClaimsIdentity Authenticate(string email)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, email)
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            
            return id;
        }
    }
}