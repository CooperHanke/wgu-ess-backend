using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WGU_ESS.Domain.Mappers;
using WGU_ESS.Domain.Repositories;
using WGU_ESS.Domain.Requests.Appointment;
using WGU_ESS.Domain.Responses.Appointment;

namespace WGU_ESS.Domain.Services
{
  public class AppointmentService : IAppointmentService
  {
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IAppointmentMapper _appointmentMapper;
    
    public AppointmentService(IAppointmentRepository appointmentRepository, IAppointmentMapper appointmentMapper)
    {
      _appointmentRepository = appointmentRepository;
      _appointmentMapper = appointmentMapper;
    }

    public async Task<IEnumerable<AppointmentResponse>> GetAppointmentsAsync()
    {
      var result = await _appointmentRepository.GetAsync();
      return result.Select(x => _appointmentMapper.Map(x));
    }

    public async Task<AppointmentResponse> GetAppointmentAsync(GetAppointmentRequest request)
    {
      if (request?.Id == null) throw new ArgumentNullException();
      var entity = await _appointmentRepository.GetAsync(request.Id);
      return _appointmentMapper.Map(entity);
    }

    public async Task<AppointmentResponse> AddAppointmentAsync(AddAppointmentRequest request)
    {
      var guidNotGood = true;
      var newId = Guid.NewGuid();
      while (guidNotGood)
      {
        var existingAppointment = await _appointmentRepository.GetAsync(newId);
        if (existingAppointment == null)
        {
          guidNotGood = false;
        }
        else
        {
          newId = Guid.NewGuid();
        }
      }
      
      if (!guidNotGood)
      {
        var appointment = _appointmentMapper.Map(request);
        appointment.CreationTime = DateTime.UtcNow;
        var result = _appointmentRepository.Add(appointment);
        await _appointmentRepository.UnitOfWork.SaveChangesAsync();
        return _appointmentMapper.Map(result);
      }

      return null; // we should never reach this condition, but it stops error messaging
    }

    public async Task<AppointmentResponse> EditAppointmentAsync(EditAppointmentRequest request)
    {
      var existingAppointment = await _appointmentRepository.GetAsync(request.Id);
      if (existingAppointment == null)
      {
        throw new ArgumentException($"Appointment with ID {request.Id} is not present");
      }
      var entity = _appointmentMapper.Map(request);
      entity.CreationTime = existingAppointment.CreationTime;
      entity.ModificationTime = DateTime.UtcNow;
      var result = _appointmentRepository.Update(entity);
      await _appointmentRepository.UnitOfWork.SaveChangesAsync();
      return _appointmentMapper.Map(result);
    }

    public async Task<AppointmentResponse> DeleteAppointmentAsync(DeleteAppointmentRequest request)
    {
      if (request?.Id == null) throw new ArgumentNullException();

      var result = await _appointmentRepository.GetAsync(request.Id);
      result.IsHidden = true;

      _appointmentRepository.Update(result);
      await _appointmentRepository.UnitOfWork.SaveChangesAsync();

      return _appointmentMapper.Map(result);
    }
  }
}