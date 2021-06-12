using PatientPortal.Web.Clients.PatientPortalApi;
using PatientPortal.Web.Models;

namespace PatientPortal.Web.Mappers
{
    public interface IUserViewModelMapper
    {
        UserViewModel Map(UserModel user, PatientModel patient, ContactModel contact, ProviderModel provider);
    }

    public class UserViewModelMapper : IUserViewModelMapper
    {
        public UserViewModel Map(UserModel user, PatientModel patient, ContactModel contact, ProviderModel provider)
        {
            var streetAddress = contact == null ? string.Empty
                : $"{contact.StreetName} {contact.StreetNumber} {contact.City}, {contact.State} {contact.Zipcode}";
            var isMobilePhone = contact == null ? string.Empty
                : (contact.IsMobilePhone ? "Yes" : "No");
            return new UserViewModel
            {
                Username = user.Username,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                StreetAddress = streetAddress,
                EmailAddress = contact?.EmailAddress ?? string.Empty,
                PhoneNumber = contact?.PhoneNumber ?? string.Empty,
                IsMobilePhone = isMobilePhone,
                ProviderName = provider?.Name ?? string.Empty,
                PolicyNumber = provider?.PolicyNumber ?? string.Empty,
                GroupNumber = provider?.GroupNumber ?? string.Empty
            };
        }
    }
}
