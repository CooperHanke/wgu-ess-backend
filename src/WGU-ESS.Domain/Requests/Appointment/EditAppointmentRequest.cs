using System;

namespace WGU_ESS.Domain.Requests.Appointment
{
  public class EditAppointmentRequest
  {
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Location { get; set; }
    public string Type { get; set; }
    public string Url { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool NeedReminder { get; set; }
    public DateTime ReminderTime { get; set; }
    public bool IsHidden { get; set; }
    public Guid UserId { get; set; }
    public Guid ContactId { get; set; }
  }
}