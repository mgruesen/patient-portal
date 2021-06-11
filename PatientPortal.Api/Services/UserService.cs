using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PatientPortal.Api.Domain;
using PatientPortal.Api.Extensions;
using PatientPortal.Api.Mappers;
using PatientPortal.Api.Models;

namespace PatientPortal.Api.Services
{
    public interface IUserService
    {
        UserModel Authenticate(string username, string password);
        IEnumerable<UserModel> GetByIds(Guid[] userIds);
        Task<UserModel> Update(UserModel user);
        Task<IEnumerable<Guid>> DeleteByIds(Guid[] userIds);
    }

    public class UserService : IUserService
    {
        private readonly IUserMapper _userMapper;
        private readonly IPasswordHash _passwordHash;
        private readonly PatientPortalDbContext _dbContext;

        public UserService(IPasswordHash passwordHash, PatientPortalDbContext dbContext, IUserMapper userMapper)
        {
            _passwordHash = passwordHash;
            _dbContext = dbContext;
            _userMapper = userMapper;
        }

        public UserModel Authenticate(string username, string password)
        {
            var user = _dbContext.Users.SingleOrDefault(u => u.Username == username);

            if (user == null)
                return null;

            return _passwordHash.Check(user.Password, password) ?
                _userMapper.MapUserModel(user) : null;
        }

        public IEnumerable<UserModel> GetByIds(params Guid[] userIds)
        {
            var users = userIds.Length == 0 ?
                _dbContext.Users.Where(u => !u.IsDeleted) :
                _dbContext.Users.Where(u =>
                    userIds.Contains(u.Id) && !u.IsDeleted);

            return users.Select(_userMapper.MapUserModel);
        }

        public async Task<UserModel> Update(UserModel userModel)
        {
            var user = _dbContext.Users
                .SingleOrDefault(u => u.Id == userModel.Id && !u.IsDeleted);

            if (user == null)
            {
                return null;
            }
            else
            {
                user.PatientId = userModel.PatientId;
            }

            await _dbContext.SaveChangesAsync();

            return _userMapper.MapUserModel(user);
        }

        public async Task<IEnumerable<Guid>> DeleteByIds(params Guid[] userIds)
        {
            var users = _dbContext.Users
                .Where(p => userIds.Contains(p.Id) && !p.IsDeleted);

            var deletedIds = users.Select(p => p.Id).ToList();

            foreach (var user in users)
                user.IsDeleted = true;

            await _dbContext.SaveChangesAsync();

            return deletedIds;
        }
    }
}
