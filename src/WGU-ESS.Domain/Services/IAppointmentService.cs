using System.Collections.Generic;
using System.Threading.Tasks;
using WGU_ESS.Domain.Requests.Appointment;
using WGU_ESS.Domain.Responses.Appointment;

namespace WGU_ESS.Domain.Services
{
  public interface IAppointmentService
  {
    Task<IEnumerable<AppointmentResponse>> GetAppointmentsAsync();
    Task<AppointmentResponse> GetAppointmentAsync(GetAppointmentRequest request);
    Task<AppointmentResponse> AddAppointmentAsync(AddAppointmentRequest request);
    Task<AppointmentResponse> EditAppointmentAsync(EditAppointmentRequest request);
    Task<AppointmentResponse> DeleteAppointmentAsync(DeleteAppointmentRequest request);
  }
}