using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WGU_ESS.Domain.Entities;
using WGU_ESS.Domain.Repositories;

namespace WGU_ESS.Infrastructure.Repositories
{
  public class AppointmentRepository : IAppointmentRepository
  {
    private readonly SystemContext _context;
    public IUnitOfWork UnitOfWork => _context;
    public AppointmentRepository(SystemContext context)
    {
      _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public Appointment Add(Appointment appointment)
    {
      return _context.Appointments.Add(appointment).Entity;
    }

    public async Task<IEnumerable<Appointment>> GetAsync()
    {
      return await _context.Appointments.AsNoTracking().Where(x => !x.IsHidden).ToListAsync();
    }

    public async Task<Appointment> GetAsync(Guid id)
    {
      return await _context.Appointments.AsNoTracking().Where(x => !x.IsHidden).Where(x => x.Id == id).FirstOrDefaultAsync();
    }

    public Appointment Update(Appointment appointment)
    {
      _context.Entry(appointment).State = EntityState.Modified;
      return appointment;
    }
  }
}