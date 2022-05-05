using FluentValidation;
using PasswordManager.Common.DTOS;

namespace PasswordManager.Core.Validators
{
    public class UserLoginDTOForUpdateValidator : AbstractValidator<UserLoginDTOForUpdate>
    {
        public UserLoginDTOForUpdateValidator()
        {
            RuleFor(userLoginDTOForUpdate => userLoginDTOForUpdate.OldUsername).NotEmpty().MaximumLength(50);
            RuleFor(userLoginDTO => userLoginDTO.Username).NotEmpty().MaximumLength(50);
        }
    }


}
