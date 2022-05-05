using Microsoft.Extensions.DependencyInjection;
using PasswordManager.Core.IServices;
using PasswordManager.Core.Services;

namespace PasswordManager.Core.Extensions
{
    public static class CoreExtensions
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IUserLoginService, UserLoginService>();
            services.AddSingleton<ICryptoService, CryptoService>();
            services.AddScoped<IAuthService, AuthService>();
        }
    }
}
