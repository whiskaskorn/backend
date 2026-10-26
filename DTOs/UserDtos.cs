namespace backend.Api.UserDtos;

public class UserPostRegistrationDTO
{
    public string? Username { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
}

public class UserPostLoginDTO
{
    public string? Username { get; set; }
    public string? Password { get; set; }
}

public class UserPutDTO
{
    public string? Username { get; set; }
    public string? Email { get; set; }
    public string? OldPassword { get; set; }
    public string? Password { get; set; }
}