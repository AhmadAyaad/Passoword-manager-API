using PasswordManager.Common.Cross_cutting;
using PasswordManager.Common.DTOS;

namespace PasswordManager.Core.IServices
{
    public interface IUserLoginService
    {
        Task<Response> AddNewUserLoginAsync(UserLoginDTO userLoginDTO);
        Task<Response> UpdateUserLoginAsync(UserLoginDTOForUpdate userLoginDTO);
        Task<Response> DeleteUserLogin(string username);
        Task<Response<List<UserLoginList>>> GetUserLoginsAsync();
        Response<UserLoginDTO> GetUserLogin(string username);
        bool IsUsernameExists(string username);
    }
}
