using System;
using System.Collections.Generic;

namespace WGU_ESS.Domain.Entities
{
  public class Contact : Person
  {
    public string Address1 { get; set; }
    public string Address2 { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string PostalCode { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Country { get; set; }
    // associates the Contact with a User
    public Guid UserId { get; set; }
    public User User { get; set; }
    // associates the Contact with multiple Appointments
    public ICollection<Appointment> Appointments { get; set; }
  }
}