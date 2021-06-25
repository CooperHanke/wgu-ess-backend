using WGU_ESS.Domain.Entities;
using WGU_ESS.Domain.Requests.Contact;
using WGU_ESS.Domain.Responses.Contact;

namespace WGU_ESS.Domain.Mappers
{
  public class ContactMapper : IContactMapper
  {
    public Contact Map(AddContactRequest request)
    {
      if (request == null) return null;
      var contact = new Contact
      {
        FirstName = request.FirstName,
        LastName = request.LastName,
        Address1 = request.Address1,
        Address2 = request.Address2,
        City = request.City,
        State = request.State,
        PostalCode = request.PostalCode,
        Country = request.Country,
        PhoneNumber = request.PhoneNumber,
        Email = request.Email,
        UserId = request.UserId
      };
      return contact;
    }

    public Contact Map(EditContactRequest request)
    {
      if (request == null) return null;
      var contact = new Contact
      {
        Id = request.Id,
        FirstName = request.FirstName,
        LastName = request.LastName,
        Address1 = request.Address1,
        Address2 = request.Address2,
        City = request.City,
        State = request.State,
        PostalCode = request.PostalCode,
        Country = request.Country,
        PhoneNumber = request.PhoneNumber,
        Email = request.Email,
        IsHidden = request.IsHidden,
        UserId = request.UserId
      };
      return contact;
    }

    public ContactResponse Map(Contact contact)
    {
      if (contact == null) return null;
      var response = new ContactResponse
      {
        Id = contact.Id,
        FirstName = contact.FirstName,
        LastName = contact.LastName,
        Address1 = contact.Address1,
        Address2 = contact.Address2,
        City = contact.City,
        State = contact.State,
        PostalCode = contact.PostalCode,
        Country = contact.Country,
        PhoneNumber = contact.PhoneNumber,
        Email = contact.Email,
        IsHidden = contact.IsHidden,
        CreationTime = contact.CreationTime,
        ModificationTime = contact.ModificationTime,
        UserId = contact.UserId
      };

      return response;
    } 
  }
}