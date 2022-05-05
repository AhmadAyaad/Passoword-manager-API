using Microsoft.AspNetCore.Mvc;
using PasswordManager.Common.Cross_cutting;
using PasswordManager.Common.DTOS;
using PasswordManager.Core.IServices;

namespace PasswordManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserLoginService _userLoginService;
        private readonly IAuthService _authService;

        public AuthController(IUserLoginService userLoginService, IAuthService authService)
        {
            _userLoginService = userLoginService;
            _authService = authService;
        }
        [HttpPost("login")]
        public IActionResult Login(UserLoginDTO userLoginDTO)
        {
            var loginDTOesponse = _authService.Login(userLoginDTO);
            if (loginDTOesponse.Status != ResponseStatus.Succeeded)
                return Unauthorized(loginDTOesponse.GetMessages());
            return Ok(loginDTOesponse.Data);
        }
    }
}
