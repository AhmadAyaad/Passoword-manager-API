using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PasswordManager.Entities.EntityModels;

namespace PasswordManager.Presistance.EntityConfiguration
{
    public class UserLoginEntityConfiguration : IEntityTypeConfiguration<UserLogin>
    {
        public void Configure(EntityTypeBuilder<UserLogin> builder)
        {
            builder.HasKey(userLogin => userLogin.Id);
            builder.Property(userLogin => userLogin.Username)
                   .IsRequired()
                   .HasMaxLength(50);
            builder.Property(userLogin => userLogin.Password)
                   .IsRequired();
        }
    }
}
