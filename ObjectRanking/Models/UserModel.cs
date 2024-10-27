namespace ObjectRanking.Models;

public class UserModel
{
    public UserModel(Guid id, string userName, string email, string password)
    {
        Id = id;
        Name = userName;
        Email = email;
        PasswordHash = password;
    }
    
    public Guid Id { get; set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }
    

    public static UserModel Create(Guid id, string name, string email, string password)
    {
        return new UserModel(id, name, email, password);
    }
}