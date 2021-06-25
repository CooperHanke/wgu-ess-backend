using Microsoft.Extensions.DependencyInjection;
using WGU_ESS.Domain.Mappers;
using WGU_ESS.Domain.Services;

namespace WGU_ESS.Domain.Extensions
{
  public static class DependencyRegistration
  {
    public static IServiceCollection AddMappers(this IServiceCollection services)
    {
      services.AddSingleton<IUserMapper, UserMapper>()
        .AddSingleton<IContactMapper, ContactMapper>()
        .AddSingleton<IAppointmentMapper, AppointmentMapper>();
      return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
      services.AddScoped<IUserService, UserService>()
        .AddScoped<IContactService, ContactService>()
        .AddScoped<IAppointmentService, AppointmentService>();
      return services;
    }
  }
}