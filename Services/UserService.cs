using backend.Api.AppDbContext;
using backend.Api.JwtService;
using backend.Api.PasswordHasher;
using backend.Api.UserModels;
using Microsoft.EntityFrameworkCore;

namespace backend.Api.UserService;

public class User_Services
{
    private UserContext _db;
    private Password_Hasher _password_Hasher;
    private Jwt_Service _jwt;
    public User_Services(UserContext db,Password_Hasher password_Hasher, Jwt_Service jwt)
    {
        _db = db;
        _password_Hasher = password_Hasher;
        _jwt = jwt;

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

    public async Task<string?> Login(string? username,string? password)
    {
        var user = await _db.Users.FirstOrDefaultAsync(u => u.Username == username);
        if(user == null)
        {
            return null;
        }

        if (!_password_Hasher.Verify(password!, user.PasswordHash!))
        {
            return null;
        }
        return _jwt.GenerateJwtToken(user);
    }
}