using WGU_ESS.Domain.Entities;
using WGU_ESS.Domain.Requests.Appointment;
using WGU_ESS.Domain.Responses.Appointment;

namespace WGU_ESS.Domain.Mappers
{
  public interface IAppointmentMapper
  {
    Appointment Map(AddAppointmentRequest request);
    Appointment Map(EditAppointmentRequest request);
    AppointmentResponse Map(Appointment Appointment);
  }
}