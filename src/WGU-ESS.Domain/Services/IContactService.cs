using System.Collections.Generic;
using System.Threading.Tasks;
using WGU_ESS.Domain.Requests.Contact;
using WGU_ESS.Domain.Responses.Contact;

namespace WGU_ESS.Domain.Services
{
  public interface IContactService
  {
    Task<IEnumerable<ContactResponse>> GetContactsAsync();
    Task<ContactResponse> GetContactAsync(GetContactRequest request);
    Task<ContactResponse> AddContactAsync(AddContactRequest request);
    Task<ContactResponse> EditContactAsync(EditContactRequest request);
    Task<ContactResponse> DeleteContactAsync(DeleteContactRequest request);
  }
}