namespace backend.Api.PasswordHasher;

public class Password_Hasher
{
    public string Generate(string password)
    {
        var hashPass = BCrypt.Net.BCrypt.EnhancedHashPassword(password);
        return hashPass;
    }

    public void Verify()
    {
        
    }
}