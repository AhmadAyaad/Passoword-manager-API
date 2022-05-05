using PasswordManager.Entities.IRepository;

namespace PasswordManager.Entities.IUnitofWork
{
    public interface IUnitOfWork
    {
        IUserLoginRepository UserLoginRepo { get; }
        Task<int> SaveChangesAsync();
    }
}
