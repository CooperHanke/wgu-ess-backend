using System;

namespace WGU_ESS.Domain.Requests.Appointment
{
  public class GetAppointmentsByUserIdRequest
  {
    public Guid UserId { get; set; }
  }
}