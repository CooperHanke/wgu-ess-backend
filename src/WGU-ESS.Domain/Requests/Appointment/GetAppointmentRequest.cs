using System;

namespace WGU_ESS.Domain.Requests.Appointment
{
  public class GetAppointmentRequest
  {
    public Guid Id { get; set; }
    public Guid ContactId { get; set; }
    public Guid UserId { get; set; }
  }
}