using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ObjectRanking.Data;
using ObjectRanking.Interfaces;
using ObjectRanking.Models.Dto;
using ObjectRanking.Models.Entities;

namespace ObjectRanking.Services;

public class UserService(ApplicationDbContext context, IPasswordHasher passwordHasher) : IUserService
{
    ApplicationDbContext _context = context;
    IPasswordHasher _passwordHasher = passwordHasher;

    public async Task<ApplicationUser> GetUserByEmailAsync(string email)
    {
        var user = await _context.ApplicationUsers.FirstOrDefaultAsync(u
            => u.Email.ToLower() == email.ToLower());
        return user;
    }
    public async Task<ApplicationUser> Authenticate(LoginRequest loginRequest)
    {
        var currentUser = await GetUserByEmailAsync(loginRequest.Email);
        
        if (currentUser != null)
        {
            var verify = _passwordHasher.Verify(loginRequest.Password, currentUser.PasswordHash);
            if (verify)
            {
                return currentUser;
            }
        }
        return null;
    }

    public async Task<ApplicationUser> Register(UserDto dto)
    {
        var newUser = new ApplicationUser()
        {
            Id = Guid.NewGuid(),
            Email = dto.Email,
            PasswordHash = _passwordHasher.Generate(dto.Password),
            Name = dto.Name,
        };
        _context.ApplicationUsers.Add(newUser);
        await _context.SaveChangesAsync();
        return newUser;
    }
    public ApplicationUser GetCurrentUser(ClaimsIdentity identity)
    {
        if (identity != null)
        {
            var userClaims = identity.Claims;

            return new ApplicationUser()
            {
                Email = userClaims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Email)).Value,
                Name = userClaims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.NameIdentifier)).Value,
                PasswordHash = "null"
            };
        }
        return null;
    }
}