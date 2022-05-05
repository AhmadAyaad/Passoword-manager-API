using FluentValidation;
using PasswordManager.Common.DTOS;

namespace PasswordManager.API.Validator
{
    public class UserLoginValidator : AbstractValidator<UserLoginDTO>
    {
        public UserLoginValidator()
        {
            RuleFor(userLoginDTO => userLoginDTO.Username)
            .NotEmpty()
            .MaximumLength(50);
            RuleFor(userLoginDTO => userLoginDTO.Password)
            .NotEmpty()
            .Matches(@"\d").WithMessage("'{PropertyName}' must contain one or more digits.")
            .Matches(@"[][""!@$%^&*(){}:;<>,.?/+_=|'~\\-]").WithMessage("'{PropertyName}' must contain one or more special characters.");
        }
    }
}
