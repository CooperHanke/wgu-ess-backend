using System;
using WGU_ESS.Domain.Entities;

namespace WGU_ESS.Domain.Requests.User
{
  public class EditUserRequest
  {
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Type { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public bool UsesDarkMode { get; set; }
    public bool IsLocked { get; set; }
    public bool IsHidden { get; set; }
  }
}