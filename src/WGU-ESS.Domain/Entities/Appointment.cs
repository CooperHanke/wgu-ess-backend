using System;

namespace WGU_ESS.Domain.Entities
{
  public class Appointment
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
    public DateTime CreationTime { get; set; }
    public DateTime ModificationTime { get; set; }
    // associates the appointment with a Contact
    public Guid ContactId { get; set; }
    public Contact Contact { get; set; }
    // assoicates the appointment with a User
    public Guid UserId { get; set; }
    public User User { get; set; }
  }
}