using System;
using System.Text.Json.Serialization;

namespace WGU_ESS.Domain.Responses.User
{
  public class UserResponse
  {
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    [JsonIgnore]
    public string Password { get; set; }
    public string Type { get; set; }
    public bool UsesDarkMode { get; set; }
    public bool IsLocked { get; set; }
    public bool NeedPasswordReset { get; set; }
    public bool IsHidden { get; set; }
    public string CreationTime { get; set; }
    public string ModificationTime { get; set; }
  }
}