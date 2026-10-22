using FluentValidation;
using backend.Api.UserDtos;
namespace backend.Api.User_Validator;

public class UserRegistrationValidator : AbstractValidator<UserPostDTO>
{
    public UserRegistrationValidator()
    {
        RuleFor(x => x.Email).NotEmpty().WithMessage("Поле не може бути пустим!").MaximumLength(254).WithMessage("Email занадто довгий!").EmailAddress().WithMessage("Некоректний формат email!");
        RuleFor(x => x.Username).NotEmpty().WithMessage("Поле не може бути пустим!").Length(5,30).WithMessage("Поле повинно містити від 5 до 30символів!");
        RuleFor(x => x.Password).NotEmpty().WithMessage("Поле не може бути пустим!").Length(7,50).WithMessage("Поле повинно містити від 7 до 50символів!");
    }
}