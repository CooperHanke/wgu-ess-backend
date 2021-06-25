using WGU_ESS.Domain.Entities;
using WGU_ESS.Domain.Requests.Contact;
using WGU_ESS.Domain.Responses.Contact;

namespace WGU_ESS.Domain.Mappers
{
  public interface IContactMapper
  {
    Contact Map(AddContactRequest request);
    Contact Map(EditContactRequest request);
    ContactResponse Map(Contact contact);
  }
}