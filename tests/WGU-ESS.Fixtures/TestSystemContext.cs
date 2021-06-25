using Microsoft.EntityFrameworkCore;
using WGU_ESS.Infrastructure.Tests.Extensions;
using WGU_ESS.Domain.Entities;
using WGU_ESS.Infrastructure;

namespace WGU_ESS.Fixtures
{
  public class TestSystemContext : SystemContext
  {
    public TestSystemContext(DbContextOptions<SystemContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      base.OnModelCreating(builder);
      builder.Seed<User>("./Data/users.json");
      builder.Seed<Contact>("./Data/contacts.json");
      builder.Seed<Appointment>("./Data/appointments.json");
    }
  }
}