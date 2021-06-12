using PatientPortal.Web.Clients.PatientPortalApi;
using PatientPortal.Web.Models;

namespace PatientPortal.Web.Mappers
{
    public interface IUserViewModelMapper
    {
        UserViewModel Compose(UserModel user, PatientModel patient, ContactModel contact, ProviderModel provider);
        (PatientModel, ContactModel, ProviderModel) Decompose(UserViewModel model);
    }

    public class UserViewModelMapper : IUserViewModelMapper
    {
        public UserViewModel Compose(UserModel user, PatientModel patient, ContactModel contact, ProviderModel provider)
        {
            return new UserViewModel
            {
                Username = user.Username,
                FirstName = patient?.FirstName ?? string.Empty,
                LastName = patient?.LastName ?? string.Empty,
                StreetName = contact?.StreetName ?? string.Empty,
                StreetNumber = contact?.StreetNumber ?? string.Empty,
                City = contact?.City ?? string.Empty,
                State = contact?.State ?? string.Empty,
                Zipcode = contact?.Zipcode ?? string.Empty,
                EmailAddress = contact?.EmailAddress ?? string.Empty,
                PhoneNumber = contact?.PhoneNumber ?? string.Empty,
                IsMobilePhone = contact?.IsMobilePhone ?? false,
                ProviderName = provider?.Name ?? string.Empty,
                PolicyNumber = provider?.PolicyNumber ?? string.Empty,
                GroupNumber = provider?.GroupNumber ?? string.Empty
            };
        }

        public (PatientModel, ContactModel, ProviderModel) Decompose(UserViewModel model)
        {
            var patientModel = new PatientModel
            {
                FirstName = model.FirstName,
                LastName = model.LastName
            };

            var contactModel = new ContactModel
            {
                StreetName = model.StreetName,
                StreetNumber = model.StreetNumber,
                City = model.City,
                State = model.State,
                Zipcode = model.Zipcode,
                EmailAddress = model.EmailAddress,
                PhoneNumber = model.PhoneNumber,
                IsMobilePhone = model.IsMobilePhone
            };

            var providerModel = new ProviderModel
            {
                Name = model.ProviderName,
                PolicyNumber = model.PolicyNumber,
                GroupNumber = model.GroupNumber
            };

            return (patientModel, contactModel, providerModel);
        }
    }
}
