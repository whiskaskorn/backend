using backend.Api.UserModels;
using Microsoft.EntityFrameworkCore;

namespace backend.Api.AppDbContext;

public class UserContext(DbContextOptions<UserContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
}