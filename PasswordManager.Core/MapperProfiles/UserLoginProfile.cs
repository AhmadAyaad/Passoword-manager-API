using AutoMapper;
using PasswordManager.Common.DTOS;
using PasswordManager.Core.IServices;
using PasswordManager.Entities.EntityModels;

namespace PasswordManager.Core.MapperProfiles
{
    public class UserLoginProfile : Profile
    {
        private readonly ICryptoService _cryptoService;

        public UserLoginProfile(ICryptoService cryptoService)
        {
            _cryptoService = cryptoService;
            CreateMap<UserLoginDTO, UserLogin>().ReverseMap();
            CreateMap<UserLogin, UserLoginList>().ForMember(dest => dest.Password, opt => opt.MapFrom(src => _cryptoService.DecryptPassword(src.Password)));
        }


    }
}
