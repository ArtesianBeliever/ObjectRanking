using System.Security.Claims;
using ObjectRanking.Models.Dto;
using ObjectRanking.Models.Entities;

namespace ObjectRanking.Interfaces;

public interface IUserService
{
    Task<ApplicationUser> GetUserByEmailAsync(string email);
    Task<ApplicationUser> Authenticate(LoginRequest loginRequest);
    Task<ApplicationUser> Register(UserDto dto);

    ApplicationUser GetCurrentUser(ClaimsIdentity identity);
}