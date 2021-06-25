using System;

namespace WGU_ESS.Domain.Requests.Appointment
{
  public class AddAppointmentRequest
  {
    public string Title { get; set; }
    public string Description { get; set; }
    public string Location { get; set; }
    public string Type { get; set; }
    public string Url { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool NeedReminder { get; set; }
    public Guid ContactId { get; set; }
    public Guid UserId { get; set; }
  }
}