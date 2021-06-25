using System;

namespace WGU_ESS.Domain.Requests.Contact
{
  public class AddContactRequest
  {
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
    public Guid UserId { get; set; }
  }
}