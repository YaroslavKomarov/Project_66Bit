using System.Collections.Generic;
using System.Security.Claims;

namespace Project_66_bit.Services.Auth
{
    public class Authentication
    {
        public ClaimsIdentity Authenticate(string email)
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