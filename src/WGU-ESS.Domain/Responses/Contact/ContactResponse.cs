using System;

namespace WGU_ESS.Domain.Responses.Contact
{
  public class ContactResponse
  {
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Address1 { get; set; }
    public string Address2 { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string PostalCode { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Country { get; set; }
    public bool IsHidden { get; set; }
    public Guid UserId { get; set; }
    public string CreationTime { get; set; }
    public string ModificationTime { get; set; }
  }
}