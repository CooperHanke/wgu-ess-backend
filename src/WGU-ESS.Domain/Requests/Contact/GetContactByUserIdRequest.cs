using System;

namespace WGU_ESS.Domain.Requests.Contact
{
  public class GetContactByUserIdRequest
  {
    public Guid UserId { get; set; }
  }
}