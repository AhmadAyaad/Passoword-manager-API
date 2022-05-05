using FluentValidation;
using PasswordManager.Common.DTOS;

namespace PasswordManager.Core.Validators
{
    public class UserLoginValidator : AbstractValidator<UserLoginDTO>
    {
        public UserLoginValidator()
        {
            RuleFor(userLoginDTO => userLoginDTO.Username).NotEmpty().MaximumLength(50);
            RuleFor(userLoginDTO => userLoginDTO.Password).NotEmpty().Matches("^[A-Za-z0-9]+$");
        }
    }
}
