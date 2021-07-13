using System;
using System.Threading.Tasks;
using Shouldly;
using WGU_ESS.Domain.Mappers;
using WGU_ESS.Domain.Requests.Appointment;
using WGU_ESS.Domain.Services;
using WGU_ESS.Fixtures;
using WGU_ESS.Infrastructure.Repositories;
using Xunit;

namespace WGU_ESS.Domain.Tests.Services
{
  public class AppointmentServiceTests : IClassFixture<SystemContextFactory>
  {
    private readonly AppointmentRepository _appointmentRepository;
    private readonly IAppointmentMapper _appointmentMapper;

    public AppointmentServiceTests(SystemContextFactory systemContextFactory)
    {
      _appointmentRepository = new AppointmentRepository(systemContextFactory.ContextInstance);
      _appointmentMapper = systemContextFactory.AppointmentMapper;
    }

    [Fact]
    public async Task get_appointments_should_return_correct_data()
    {
      AppointmentService sut = new AppointmentService(_appointmentRepository, _appointmentMapper);
      var result = await sut.GetAppointmentsAsync();
      result.ShouldNotBeNull();
    }

    [Theory]
    [InlineData("b125ab55-7e5c-4d95-ada3-e2a74f7e2130")]
    public async Task get_appointment_by_id_should_return_correct_appointment(string guid)
    {
      AppointmentService sut = new AppointmentService(_appointmentRepository, _appointmentMapper);
      var result = await sut.GetAppointmentAsync(new GetAppointmentRequest { Id = new Guid(guid)} );
      result.Id.ShouldBe(new Guid(guid));
      result.UserId.ShouldBe(new Guid("e61044cd-a73a-4786-97f9-e1570cde84c7"));
      result.ContactId.ShouldBe(new Guid("23153a80-0e99-46be-a804-83a704b86353"));
    }

    [Fact]
    public void get_appointment_should_throw_exception_with_null_id()
    {
      AppointmentService sut = new AppointmentService(_appointmentRepository, _appointmentMapper);
      sut.GetAppointmentAsync(null).ShouldThrow<ArgumentNullException>();
    }

    [Fact]
    public async Task add_appointment_should_add_right_entity()
    {
      var test_appointment = new AddAppointmentRequest
      {
        Title = "Test Title",
        Description = "Test Description",
        Type = "Initial Appointment",
        Location = "Remote (Zoom)",
        Url = "www.google.com",
        NeedReminder = true,
        StartDate = new DateTime(2021, 8, 1, 12, 0, 0).ToUniversalTime(),
        EndDate = new DateTime(2021, 8, 1, 14, 0, 0).ToUniversalTime(),
        ContactId = new Guid("41724142-d351-4505-856f-beda5a095552"),
        UserId = new Guid("66da25ef-198e-40b4-997e-af986cabf880")
      };

      AppointmentService sut = new AppointmentService(_appointmentRepository, _appointmentMapper);
      var result = await sut.AddAppointmentAsync(test_appointment);

      result.Id.ToString().ShouldNotBeNull();
      result.Title.ShouldBe(test_appointment.Title);
      result.Location.ShouldBe(test_appointment.Location);
      result.Url.ShouldBe(test_appointment.Url);
      result.NeedReminder.ShouldBe(test_appointment.NeedReminder);
      result.StartDate.ShouldBe(test_appointment.StartDate.ToString("yyyy-MM-ddTHH:mm:ssZ"));
      result.EndDate.ShouldBe(test_appointment.EndDate.ToString("yyyy-MM-ddTHH:mm:ssZ"));
      result.ContactId.ShouldBe(test_appointment.ContactId);
      result.UserId.ShouldBe(test_appointment.UserId);
      result.CreationTime.ToString().ShouldNotBeNull();
    }

    [Fact]
    public async Task edit_appointment_should_edit_the_right_entity()
    {
      var test_appointment = new EditAppointmentRequest
      {
        Id = new Guid("7e6956b9-43f4-4eb6-a311-a6474f956d85"),
        Title = "Test Title",
        Description = "Test Description",
        Type = "Initial Appointment",
        Location = "Remote (Zoom)",
        Url = "www.google.com",
        NeedReminder = true,
        IsHidden = false,
        StartDate = new DateTime(2021, 8, 1, 12, 0, 0).ToUniversalTime(),
        EndDate = new DateTime(2021, 8, 1, 14, 0, 0).ToUniversalTime(),
        ContactId = new Guid("41724142-d351-4505-856f-beda5a095552"),
        UserId = new Guid("66da25ef-198e-40b4-997e-af986cabf880")
      };

      AppointmentService sut = new AppointmentService(_appointmentRepository, _appointmentMapper);
      var result = await sut.EditAppointmentAsync(test_appointment);

      result.Id.ToString().ShouldNotBeNull();
      result.Title.ShouldBe(test_appointment.Title);
      result.Location.ShouldBe(test_appointment.Location);
      result.Url.ShouldBe(test_appointment.Url);
      result.NeedReminder.ShouldBe(test_appointment.NeedReminder);
      result.StartDate.ShouldBe(test_appointment.StartDate.ToString("yyyy-MM-ddTHH:mm:ssZ"));
      result.EndDate.ShouldBe(test_appointment.EndDate.ToString("yyyy-MM-ddTHH:mm:ssZ"));
      result.IsHidden.ShouldBe(test_appointment.IsHidden);
      result.ContactId.ShouldBe(test_appointment.ContactId);
      result.UserId.ShouldBe(test_appointment.UserId);
      result.CreationTime.ToString().ShouldNotBeNull();
    }
  }
}