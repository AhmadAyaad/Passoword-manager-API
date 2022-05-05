using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PasswordManager.Common.Cross_cutting;
using PasswordManager.Common.DTOS;
using PasswordManager.Core.IServices;
using PasswordManager.Core.Validators;
using System.Net;

namespace PasswordManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserLoginsController : ControllerBase
    {
        private readonly IUserLoginService _userLoginService;

        public UserLoginsController(IUserLoginService userLoginService)
        {
            _userLoginService = userLoginService;
        }
        [HttpPost]
        public async Task<IActionResult> AddNewUserLogin(UserLoginDTO userLoginDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            try
            {
                var response = await _userLoginService.AddNewUserLoginAsync(userLoginDTO);
                if (response.Status != ResponseStatus.Succeeded)
                    return BadRequest(response.GetMessages());
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Exception has been thrown while adding new users.");
            }
        }
        [HttpPut]
        public async Task<IActionResult> UpdateUserLogin(UserLoginDTOForUpdate userLoginDTOForUpdate)
        {
            var validationResult = new UserLoginDTOForUpdateValidator().Validate(userLoginDTOForUpdate);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);
            try
            {
                var response = await _userLoginService.UpdateUserLoginAsync(userLoginDTOForUpdate);
                if (response.Status != ResponseStatus.Succeeded)
                    return BadRequest(response.GetMessages());
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Exception has been thrown while updateing user login.");
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetUserLogins()
        {
            var userLoginListResponse = await _userLoginService.GetUserLoginsAsync();
            return Ok(userLoginListResponse.Data);
        }
        [HttpGet("{username}")]
        public IActionResult GetUserLogins([FromRoute] string username)
        {
            var userLoginListResponse = _userLoginService.GetUserLogin(username);
            return Ok(userLoginListResponse.Data);
        }
        [HttpDelete("{username}")]
        public IActionResult DeleteUserLogin([FromRoute] string username)
        {
            try
            {
                _userLoginService.DeleteUserLogin(username);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Exception has been thrown while deleting user login.");
            }
        }
    }

}
