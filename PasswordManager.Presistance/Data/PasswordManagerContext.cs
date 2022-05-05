using Microsoft.EntityFrameworkCore;
using PasswordManager.Entities.EntityModels;
using System.Reflection;

namespace PasswordManager.Presistance.Data
{
    public class PasswordManagerContext : DbContext
    {
        public PasswordManagerContext(DbContextOptions<PasswordManagerContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<UserLogin> UserLogins { get; set; }
    }
}
