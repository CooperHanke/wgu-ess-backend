using System;

namespace WGU_ESS.Domain.Requests.Contact
{
  public class GetContactRequest
  {
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
  }
}