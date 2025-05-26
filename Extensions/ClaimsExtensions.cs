using System;
using System.Linq;
using System.Security.Claims;

namespace api.Extensions
{
    public static class ClaimsExtensions
    {
        public static string GetUsername(this ClaimsPrincipal user)
        {
            var claim = user?.Claims?.SingleOrDefault(x =>
                x.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname"));

            if (claim == null || string.IsNullOrEmpty(claim.Value))
            {
                throw new Exception("User identity claim 'givenname' not found. Ensure you are authenticated and your JWT token is valid.");
            }

            return claim.Value;
        }
    }
}
