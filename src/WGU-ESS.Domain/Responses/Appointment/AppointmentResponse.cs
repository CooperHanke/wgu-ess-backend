using System;

namespace WGU_ESS.Domain.Responses.Appointment
{
  public class AppointmentResponse
  {
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Location { get; set; }
    public string Type { get; set; }
    public string Url { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsHidden { get; set; }
    public bool NeedReminder { get; set; }
    public Guid ContactId { get; set; }
    public Guid UserId { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime ModificationTime { get; set; }
  }
}