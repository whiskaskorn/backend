using backend.Api.AppDbContext;
using backend.Api.PasswordHasher;
using backend.Api.UserModels;

namespace backend.Api.UserService;

public class User_Services
{
    UserContext _db;
    Password_Hasher _password_Hasher;
    public User_Services(UserContext db,Password_Hasher password_Hasher)
    {
        _db = db;
        _password_Hasher = password_Hasher;
    }
    public async Task<User?> Registration(string? username,string? email, string? password)
    {
        var User = new User
        {
            Username = username,
            Email = email,
            PasswordHash = _password_Hasher.Generate(password!)
        };

        _db.Users.Add(User);
        await _db.SaveChangesAsync();
        return User;
    }
}