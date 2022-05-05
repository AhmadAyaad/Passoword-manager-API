using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PasswordManager.Entities.IUnitofWork;
using PasswordManager.Presistance.Data;

namespace PasswordManager.Presistance.Extensions
{
    public static class PresistanceExtensions
    {
        public static void ConfigureServices(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<PasswordManagerContext>(options => options.UseSqlServer(connectionString));
            services.AddScoped<IUnitOfWork, UnitofWork.UnitofWork>();
        }
    }
}
