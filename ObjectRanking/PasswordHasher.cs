using ObjectRanking.Interfaces;

namespace ObjectRanking;

public class PasswordHasher : IPasswordHasher
{
    public PasswordHasher()
    {
        
    }
    public string Generate(string password) => BCrypt.Net.BCrypt.HashPassword(password);
    
    public bool Verify (string password, string hash) => BCrypt.Net.BCrypt.Verify(password, hash);
}