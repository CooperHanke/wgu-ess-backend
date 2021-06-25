using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WGU_ESS.Domain.Entities;

namespace WGU_ESS.Domain.Repositories
{
  public interface IAppointmentRepository : IRepository
  {
    Task<IEnumerable<Appointment>> GetAsync();
    Task<Appointment> GetAsync(Guid id);
    Appointment Add(Appointment Appointment);
    Appointment Update(Appointment Appointment);
  }
}