using PasswordManager.Entities.EntityModels;
using PasswordManager.Entities.IRepository;
using PasswordManager.Presistance.Data;

namespace PasswordManager.Presistance.Repository
{
    public class UserLoginRepository : BaseRepository<UserLogin>, IUserLoginRepository
    {
        public UserLoginRepository(PasswordManagerContext passwordManagerContext) : base(passwordManagerContext)
        {

        }
    }
}
