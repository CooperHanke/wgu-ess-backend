using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WGU_ESS.Domain.Entities;
using WGU_ESS.Domain.Repositories;
using WGU_ESS.Infrastructure.SchemaDefinitions;

namespace WGU_ESS.Infrastructure
{
  public class SystemContext : DbContext, IUnitOfWork
  {
    public DbSet<User> Users { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<Contact> Contacts { get; set; }
    public SystemContext(DbContextOptions<SystemContext> options) : base(options){}
    protected override void OnModelCreating(ModelBuilder builder)
    {
      builder.ApplyConfiguration(new UserEntitySchemaDefinition());
      builder.ApplyConfiguration(new AppointmentEntitySchemaDefinition());
      builder.ApplyConfiguration(new ContactEntitySchemaDefinition());
      base.OnModelCreating(builder);
    }
    public async Task<bool> SaveEntitiesAsync(CancellationToken token = default)
    {
      await SaveChangesAsync(token);
      return true;
    }
  }
}