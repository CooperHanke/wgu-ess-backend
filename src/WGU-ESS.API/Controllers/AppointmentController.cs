using WGU_ESS.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WGU_ESS.Domain.Requests.Appointment;
using System;
using Microsoft.AspNetCore.Authorization;

namespace WGU_ESS.API.Controllers
{
  [Authorize]
  [ApiController]
  [Route("/api/appointments")]
  public class AppointmentController : ControllerBase
  {
    private readonly IAppointmentService _appointmentService;

    public AppointmentController(IAppointmentService appointmentService)
    {
      _appointmentService = appointmentService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
      var result = await _appointmentService.GetAppointmentsAsync();
      return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
      var result = await _appointmentService.GetAppointmentAsync(new GetAppointmentRequest { Id = id } );
      return Ok(result);
    }

    [HttpGet("user/{userId:guid}")]
    public async Task<IActionResult> GetByUserId(Guid userId)
    {
      var result = await _appointmentService.GetAppointmentsByUserIdAsync(new GetAppointmentsByUserIdRequest { UserId = userId } );
      return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Post(AddAppointmentRequest request)
    {
      var result = await _appointmentService.AddAppointmentAsync(request);
      return CreatedAtAction(nameof(GetById), new { id = result.Id }, null);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Put(Guid id, EditAppointmentRequest request)
    {
      request.Id = id;
      var result = await _appointmentService.EditAppointmentAsync(request);
      return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
      var request = new DeleteAppointmentRequest { Id = id };
      await _appointmentService.DeleteAppointmentAsync(request);
      return NoContent();
    }
  }
}
