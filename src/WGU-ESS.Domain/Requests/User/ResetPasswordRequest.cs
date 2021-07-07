using System;

namespace WGU_ESS.Domain.Requests.User
{
  public class ResetPasswordRequest
  {
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
  }
}