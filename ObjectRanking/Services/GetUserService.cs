using System.Security.Claims;
using ObjectRanking.Models.Entities;

namespace ObjectRanking.Services;

public class GetUserService
{
    public ApplicationUser GetCurrentUser(HttpContext httpContext)
    {
        var identity = httpContext.User.Identity as ClaimsIdentity;
        if (identity != null)
        {
            var userClaims = identity.Claims;

            return new ApplicationUser()
            {
                Email = userClaims.First(c => c.Type == "Email").Value,
                Name = userClaims.First(c => c.Type == "Name").Value,
                PasswordHash = "null"
            };
        }
        return null;
    }
}