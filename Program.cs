using backend.Api.AppDbContext;
using backend.Api.AuthExtensions;
using backend.Api.JwtService;
using backend.Api.PasswordHasher;
using backend.Api.User_Endpoints;
using backend.Api.UserService;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOpenApi();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddDbContext<UserContext>(o => o.UseNpgsql("Host=localhost; Port=5432; Database=backend; Username=postgres; Password=whiskas0212"));
builder.Services.AddScoped<User_Services>();
builder.Services.AddScoped<Password_Hasher>();
builder.Services.AddScoped<Jwt_Service>();

builder.Services.AddAuth(builder.Configuration);
var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

UserEndpoints.MapUserEndpoints(app);
app.Run();
