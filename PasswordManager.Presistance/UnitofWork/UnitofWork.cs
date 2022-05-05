using PasswordManager.Entities.IRepository;
using PasswordManager.Entities.IUnitofWork;
using PasswordManager.Presistance.Data;
using PasswordManager.Presistance.Repository;

namespace PasswordManager.Presistance.UnitofWork
{
    public class UnitofWork : IUnitOfWork
    {
        private readonly PasswordManagerContext _context;
        private IUserLoginRepository _userLoginRepo;
        public UnitofWork(PasswordManagerContext passwordManagerContext)
        {
            _context = passwordManagerContext;
        }
        public IUserLoginRepository UserLoginRepo
        {
            get
            {
                if (_userLoginRepo == null)
                    _userLoginRepo = new UserLoginRepository(_context);
                return _userLoginRepo;
            }
        }
        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
