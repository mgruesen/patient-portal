using System;
using PatientPortal.Api.Domain;
using PatientPortal.Api.Models;

namespace PatientPortal.Api.Mappers
{
    public interface IUserMapper
    {
        User MapUser(UserModel providerModel);
        UserModel MapUserModel(User provider);
    }

    public class UserMapper : IUserMapper
    {
        public User MapUser(UserModel model)
        {
            return new()
            {
                Id = model.Id ?? Guid.Empty,
                Username = model.Username,
                PatientId = model.PatientId,
            };
        }

        public UserModel MapUserModel(User user)
        {
            return new()
            {
                Id = user.Id,
                Username = user.Username,
                PatientId = user.PatientId
            };
        }
    }
}
