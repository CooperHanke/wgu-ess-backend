using System;
using Microsoft.EntityFrameworkCore;
using WGU_ESS.Domain.Mappers;
using WGU_ESS.Infrastructure;

namespace WGU_ESS.Fixtures
{
  public class SystemContextFactory
  {
    public readonly TestSystemContext ContextInstance;
    public readonly IUserMapper UserMapper;
    public readonly IContactMapper ContactMapper;
    public readonly IAppointmentMapper AppointmentMapper;

    public SystemContextFactory()
    {
      var contextOptions = new DbContextOptionsBuilder<SystemContext>()
        .UseInMemoryDatabase(Guid.NewGuid().ToString())
        .EnableSensitiveDataLogging()
        .Options;

      EnsureCreation(contextOptions);
      ContextInstance = new TestSystemContext(contextOptions);
      UserMapper = new UserMapper();
      ContactMapper = new ContactMapper();
      AppointmentMapper = new AppointmentMapper();
    }

    private void EnsureCreation(DbContextOptions<SystemContext> contextOptions)
    {
      using var context = new TestSystemContext(contextOptions);
      context.Database.EnsureCreated();
    }
  }
}