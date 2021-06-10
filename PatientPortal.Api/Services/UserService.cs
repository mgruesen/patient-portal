using System.Linq;
using System.Threading.Tasks;
using PatientPortal.Api.Domain;
using PatientPortal.Api.Extensions;

namespace PatientPortal.Api.Services
{
    public interface IUserService
    {
        User Authenticate(string username, string password);
    }

    public class UserService : IUserService
    {
        private readonly IPasswordHash _passwordHash;
        private readonly PatientPortalDbContext _dbContext;

        public UserService(IPasswordHash passwordHash, PatientPortalDbContext dbContext)
        {
            _passwordHash = passwordHash;
            _dbContext = dbContext;
        }

        public User Authenticate(string username, string password)
        {
            var user = _dbContext.Users.SingleOrDefault(u => u.Username == username);

            if (user == null)
                return null;

            return _passwordHash.Check(user.Password, password) ? user : null;
        }
    }
}
