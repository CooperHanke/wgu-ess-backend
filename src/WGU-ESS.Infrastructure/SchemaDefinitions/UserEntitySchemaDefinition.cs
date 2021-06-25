using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WGU_ESS.Domain.Entities;

namespace WGU_ESS.Infrastructure.SchemaDefinitions
{
  public class UserEntitySchemaDefinition : IEntityTypeConfiguration<User>
  {
    public void Configure(EntityTypeBuilder<User> builder)
    {
      builder.ToTable("Users");
      builder.HasKey(k => k.Id);

      builder.Property(p => p.FirstName).IsRequired();
      builder.Property(p => p.LastName).IsRequired();
      builder.Property(p => p.Password).IsRequired();
      builder.Property(p => p.UserName).IsRequired();
      builder.Property(p => p.Type).IsRequired();
      builder.Property(p => p.IsHidden).IsRequired();
      builder.Property(p => p.UsesDarkMode).IsRequired();

      builder.Property(p => p.Type).HasConversion<string>().IsRequired();
      
      builder.HasMany(e => e.Appointments).WithOne(e => e.User).HasForeignKey(k => k.UserId);
      builder.HasMany(e => e.Contacts).WithOne(e => e.User).HasForeignKey(k => k.UserId);
    }
  }
}