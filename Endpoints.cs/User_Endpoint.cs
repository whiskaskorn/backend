using backend.Api.UserDtos;
using backend.Api.UserModels;
using backend.Api.User_Validator;
using backend.Api.UserService;
using FluentValidation;


namespace backend.Api.User_Endpoints;

public static class UserEndpoints
{
    public static void MapUserEndpoints(WebApplication app)
    {

        app.MapGet("/users/{id}", () =>
        {
            
        });

        app.MapPost("/users/registration", async (UserPostDTO dto, IValidator<UserPostDTO> validator, User_Services users) =>
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
            
            var user = await users.Registration(dto.Username,dto.Email,dto.Password);
            return Results.Created();
        });
    }
}