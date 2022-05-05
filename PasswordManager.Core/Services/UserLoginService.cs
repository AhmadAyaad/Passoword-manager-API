using AutoMapper;
using PasswordManager.Common.Cross_cutting;
using PasswordManager.Common.DTOS;
using PasswordManager.Core.IServices;
using PasswordManager.Entities.EntityModels;
using PasswordManager.Entities.IUnitofWork;

namespace PasswordManager.Core.Services
{
    public class UserLoginService : IUserLoginService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICryptoService _cryptoService;

        public UserLoginService(IUnitOfWork unitOfWork, IMapper mapper, ICryptoService cryptoService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cryptoService = cryptoService;
        }
        public async Task<Response> AddNewUserLoginAsync(UserLoginDTO userLoginDTO)
        {
            if (IsUsernameExists(userLoginDTO.Username))
                return new Response(ResponseStatus.BadRequest, "Username already exists");
            var userLogin = _mapper.Map<UserLogin>(userLoginDTO);
            _cryptoService.EncryptPassword(userLogin.Password);
            userLogin.Password = _cryptoService.EncryptedPassword;
            await _unitOfWork.UserLoginRepo.AddAsync(userLogin);
            await _unitOfWork.SaveChangesAsync();
            return new Response();
        }

        public async Task<Response> DeleteUserLogin(string username)
        {
            var userLogin = _unitOfWork.UserLoginRepo.Find(userLogin => userLogin.Username == username).SingleOrDefault();
            if (userLogin is null)
                return new Response<UserLoginDTO>(null, ResponseStatus.NotFound, "Invalid username");
            _unitOfWork.UserLoginRepo.Remove(userLogin);
            await _unitOfWork.SaveChangesAsync();
            return new Response();
        }

        public Response<UserLoginDTO> GetUserLogin(string username)
        {
            var userLogin = _unitOfWork.UserLoginRepo.Find(userLogin => userLogin.Username == username.Trim()).SingleOrDefault();
            if (userLogin is null)
                return new Response<UserLoginDTO>(null, ResponseStatus.NotFound, "Invalid username");
            var userLoginDTO = _mapper.Map<UserLoginDTO>(userLogin);
            userLoginDTO.Password = _cryptoService.DecryptPassword(userLogin.Password);
            return new Response<UserLoginDTO>(userLoginDTO);
        }

        public async Task<Response<List<UserLoginList>>> GetUserLoginsAsync()
        {
            var userLogins = await _unitOfWork.UserLoginRepo.GetAllAsync();
            var userLoginList = _mapper.Map<List<UserLoginList>>(userLogins);
            return new Response<List<UserLoginList>>(userLoginList);
        }

        public bool IsUsernameExists(string username)
        {
            return _unitOfWork.UserLoginRepo.Find(userLogin => userLogin.Username == username).Any();
        }

        public async Task<Response> UpdateUserLoginAsync(UserLoginDTOForUpdate userLoginDTOForUpdate)
        {
            var userLogin = _unitOfWork.UserLoginRepo.Find(userLogin => userLoginDTOForUpdate.OldUsername == userLogin.Username).SingleOrDefault();
            if (userLogin is null)
                return new Response(ResponseStatus.NotFound, "Invalid username");
            var newUserLogin = _mapper.Map<UserLogin>(userLoginDTOForUpdate);
            userLogin.Username = newUserLogin.Username;
            _cryptoService.EncryptPassword(newUserLogin.Password);
            userLogin.Password = _cryptoService.EncryptedPassword;
            await _unitOfWork.SaveChangesAsync();
            return new Response();
        }
    }
}
