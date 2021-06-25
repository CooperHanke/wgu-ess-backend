using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WGU_ESS.Domain.Entities;

namespace WGU_ESS.Infrastructure.SchemaDefinitions
{
  public class ContactEntitySchemaDefinition : IEntityTypeConfiguration<Contact>
  {
    public void Configure(EntityTypeBuilder<Contact> builder)
    {
      builder.ToTable("Contacts");
      builder.HasKey(k => k.Id);
      
      builder.Property(p => p.Address1).IsRequired();
      builder.Property(p => p.City).IsRequired();
      builder.Property(p => p.Country).IsRequired();
      builder.Property(p => p.PostalCode).IsRequired();
      builder.Property(p => p.PhoneNumber).IsRequired();
      builder.Property(p => p.UserId).IsRequired();
      
      builder.HasOne(e => e.User).WithMany(c => c.Contacts).HasForeignKey(k => k.UserId);
      builder.HasMany(e => e.Appointments).WithOne(c => c.Contact).HasForeignKey(k => k.ContactId);
    }
  }
}