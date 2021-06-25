using WGU_ESS.Domain.Entities;
using WGU_ESS.Domain.Requests.Appointment;
using WGU_ESS.Domain.Responses.Appointment;

namespace WGU_ESS.Domain.Mappers
{
  public class AppointmentMapper : IAppointmentMapper
  {
    public Appointment Map(AddAppointmentRequest request)
    {
      if (request == null) return null;
      var appointment = new Appointment
      {
        Title = request.Title,
        Description = request.Description,
        Location = request.Location,
        Type = request.Type,
        Url = request.Url,
        StartDate = request.StartDate,
        EndDate = request.EndDate,
        NeedReminder = request.NeedReminder,
        ContactId = request.ContactId,
        UserId = request.UserId
      };
      return appointment;
    }

    public Appointment Map(EditAppointmentRequest request)
    {
      if (request == null) return null;
      var appointment = new Appointment
      {
        Id = request.Id,
        Title = request.Title,
        Description = request.Description,
        Location = request.Location,
        Type = request.Type,
        Url = request.Url,
        StartDate = request.StartDate,
        EndDate = request.EndDate,
        NeedReminder = request.NeedReminder,
        IsHidden = request.IsHidden,
        ContactId = request.ContactId,
        UserId = request.UserId
      };
      return appointment;
    }

    public AppointmentResponse Map(Appointment appointment)
    {
      if (appointment == null) return null;
      var response = new AppointmentResponse
      {
        Id = appointment.Id,
        Title = appointment.Title,
        Description = appointment.Description,
        Location = appointment.Location,
        Type = appointment.Type,
        Url = appointment.Url,
        StartDate = appointment.StartDate,
        EndDate = appointment.EndDate,
        NeedReminder = appointment.NeedReminder,
        IsHidden = appointment.IsHidden,
        CreationTime = appointment.CreationTime,
        ModificationTime = appointment.ModificationTime,
        ContactId = appointment.ContactId,
        UserId = appointment.UserId
      };

      return response;
    } 
  }
}