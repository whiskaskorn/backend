using backend.Api.UserDtos;
using backend.Api.UserModels;
using backend.Api.User_Validator;
using backend.Api.UserService;
using FluentValidation;
using backend.Api.AppDbContext;
using Microsoft.EntityFrameworkCore;
using backend.Api.PasswordHasher;


namespace backend.Api.User_Endpoints;

public static class UserEndpoints
{
    public static void MapUserEndpoints(WebApplication app)
    {
        app.MapGet("/users", async (UserContext db) =>
        {
            var users = await db.Users.ToListAsync();
            return users;
        }).RequireAuthorization();
        app.MapGet("/users/{id}", async (int id, UserContext db) =>
        {
            var user = await db.Users.FindAsync(id);
            if(user == null)
            {
                return Results.NotFound("Користувач не знайдений!");
            }

            return Results.Ok(user);
        });

        app.MapPost("/users/registration", async (UserPostRegistrationDTO dto, IValidator<UserPostRegistrationDTO> validator, User_Services users, UserContext _db) =>
        {
            if(dto == null)
            {
                return Results.BadRequest("Нема даних!");
            }

            var userExsists = await _db.Users.FirstOrDefaultAsync(u => u.Username == dto.Username || u.Email == dto.Email);
            if (userExsists != null)
            {
                return Results.Conflict("Користувач з таким Username або Email вже існує!");
            }

            var validationResult = validator.Validate(dto);
            if (!validationResult.IsValid)
            {
                return Results.ValidationProblem(validationResult.ToDictionary());
            }
            
            var user = await users.Registration(dto.Username,dto.Email,dto.Password);
            return Results.Created();
        });
        app.MapPost("/users/login", async (UserPostLoginDTO dto,IValidator<UserPostLoginDTO> validator, User_Services users) =>
        {
            if(dto == null)
            {
                return Results.BadRequest("Нема даних!");
            }

            var validationResult = validator.Validate(dto);
            if (!validationResult.IsValid)
            {
                return Results.ValidationProblem(validationResult.ToDictionary());
            }

            var token = await users.Login(dto.Username,dto.Password);
            if(token == null)
            {
                return Results.Unauthorized();
            }
            return Results.Ok(token);
        });
        app.MapPut("/users/{id}", async (UserPutDTO dto,int id, IValidator<UserPutDTO> validator,UserContext db, Password_Hasher passwordHasher) =>
        {
            var user = await db.Users.FindAsync(id);
            if(user == null)
            {
                return Results.NotFound("Користувач не знайдений!");
            }

            if(dto == null)
            {
                return Results.BadRequest("Нема даних!");
            }

            var validationResult = validator.Validate(dto);
            if (!validationResult.IsValid)
            {
                return Results.ValidationProblem(validationResult.ToDictionary());
            }

            if (!passwordHasher.Verify(dto.OldPassword!, user.PasswordHash!))
            {
                return Results.Conflict("Невірний пароль!");
            }

            user.Username = dto.Username;
            user.Email = dto.Email;
            user.PasswordHash = passwordHasher.Generate(dto.Password!);
            await db.SaveChangesAsync();
            return Results.Ok(user);
        });
        app.MapDelete("/users/{id}", async (int id, UserContext db) =>
        {
            var user = await db.Users.FindAsync(id);
            if(user == null)
            {
                return Results.NotFound("Користувач не знайдений!");
            }
            db.Users.Remove(user);
            await db.SaveChangesAsync();
            return Results.NoContent();
        });
    }
}