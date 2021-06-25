using System;

namespace WGU_ESS.Domain.Requests.User
{
  public class AddUserRequest
  {
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Type { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public bool UsesDarkMode { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime ModificationTime { get; set; }
  }
}