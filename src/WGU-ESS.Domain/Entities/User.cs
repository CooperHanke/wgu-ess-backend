using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace WGU_ESS.Domain.Entities
{
  public enum UserType
  {
    [EnumMember( Value = "Standard") ]
    Standard,
    [EnumMember( Value = "Manager") ]
    Manager
  }
  public class User : Person
  {
    public string UserName { get; set; }
    public UserType Type { get; set; }
    public string Password { get; set; }
    public bool UsesDarkMode { get; set; }
    public bool IsLocked { get; set; }
    // associates the contacts with the user
    public ICollection<Contact> Contacts { get; set; }
    // assoicates the appointments with the user
    public ICollection<Appointment> Appointments { get; set; }
  }
}