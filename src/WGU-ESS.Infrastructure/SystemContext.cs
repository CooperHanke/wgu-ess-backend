using System;
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
      builder.Entity<User>().HasData(new User
      {
        Id = Guid.NewGuid(),
        FirstName = "Super",
        LastName = "User",
        UserName = "administrator",
        Password = "10000.UlUzXM4ddGiwY/Us1TTOIw==.Vir0S/Ac6LaOCOmPK6u9slc8JoRToUN+zHYU7DeTVKs=",
        Type = UserType.Manager,
        UsesDarkMode = false,
        CreationTime = DateTime.UtcNow,
        ModificationTime = DateTime.UtcNow,
        NeedPasswordReset = false,
        IsLocked = false,
        IsHidden = false
      });
      base.OnModelCreating(builder);
    }
    public async Task<bool> SaveEntitiesAsync(CancellationToken token = default)
    {
      await SaveChangesAsync(token);
      return true;
    }
  }
}