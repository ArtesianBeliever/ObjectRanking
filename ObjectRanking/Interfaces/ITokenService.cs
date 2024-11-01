using ObjectRanking.Models.Entities;

namespace ObjectRanking.Interfaces;

public interface ITokenService
{
    string Generate(ApplicationUser user);
    
}