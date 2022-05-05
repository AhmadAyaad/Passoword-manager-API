using PasswordManager.Common.Cross_cutting;
using PasswordManager.Common.DTOS;

namespace PasswordManager.Core.IServices
{
    public interface IAuthService
    {
        Response<LoginDTO> Login(UserLoginDTO userLoginDTO);
    }

}
