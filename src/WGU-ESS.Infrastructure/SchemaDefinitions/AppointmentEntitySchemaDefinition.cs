using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WGU_ESS.Domain.Entities;

namespace WGU_ESS.Infrastructure.SchemaDefinitions
{
  public class AppointmentEntitySchemaDefinition : IEntityTypeConfiguration<Appointment>
  {
    public void Configure(EntityTypeBuilder<Appointment> builder)
    {
      builder.ToTable("Appointments");
      builder.HasKey(k => k.Id);
      
      builder.Property(p => p.Title).IsRequired();
      builder.Property(p => p.Description).IsRequired();
      builder.Property(p => p.Location).IsRequired();
      builder.Property(p => p.NeedReminder).IsRequired();
      builder.Property(p => p.StartDate).IsRequired();
      builder.Property(p => p.EndDate).IsRequired();
      builder.Property(p => p.UserId).IsRequired();
      builder.Property(p => p.ContactId).IsRequired();
      builder.Property(p => p.IsHidden).IsRequired();
      
      builder.HasOne(e => e.User).WithMany(c => c.Appointments).HasForeignKey(k => k.UserId);
      builder.HasOne(e => e.Contact).WithMany(c => c.Appointments).HasForeignKey(k => k.ContactId);
    }
  }
}