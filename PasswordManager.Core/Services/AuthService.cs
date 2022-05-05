using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PasswordManager.Common.Cross_cutting;
using PasswordManager.Common.DTOS;
using PasswordManager.Core.IServices;
using PasswordManager.Entities.EntityModels;
using PasswordManager.Entities.IUnitofWork;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PasswordManager.Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICryptoService _cryptoService;
        private readonly JWT _jwt;
        public AuthService(IUnitOfWork unitOfWork, ICryptoService cryptoService, IOptions<JWT> jwt)
        {
            _unitOfWork = unitOfWork;
            _cryptoService = cryptoService;
            _jwt = jwt.Value;
        }
        public Response<LoginDTO> Login(UserLoginDTO userLoginDTO)
        {
            var existingUserLogin = _unitOfWork.UserLoginRepo.Find(userLogin => userLogin.Username == userLoginDTO.Username).SingleOrDefault();
            if (existingUserLogin is null)
                return new Response<LoginDTO>(null, ResponseStatus.Unauthorized, "Cannot find this email or password");
            if (_cryptoService.DecryptPassword(existingUserLogin.Password) == userLoginDTO.Password)
            {
                JwtSecurityToken jwtSecurityToken = CreateJwtToken(existingUserLogin);
                LoginDTO loginDTO = new();
                loginDTO.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
                loginDTO.Username = existingUserLogin.Username;
                return new Response<LoginDTO>(loginDTO);
            }
            return new Response<LoginDTO>(null, ResponseStatus.Unauthorized, "Cannot find this email or password");
        }
        private JwtSecurityToken CreateJwtToken(UserLogin userLogin)
        {
            var roleClaims = new List<Claim>();
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, userLogin.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.NameId, userLogin.Id.ToString())
            }
            .Union(roleClaims);
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwt.DurationInMinutes),
                signingCredentials: signingCredentials);
            return jwtSecurityToken;
        }
    }
}
