using System.Reflection;
using WGU_ESS.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace WGU_ESS.API.Extensions
{
  public static class DatabaseExtensions
  {
    public static IServiceCollection AddSystemContext(this IServiceCollection services, string connectionString)
    {
      return services.AddDbContext<SystemContext>(opts =>
        {
          opts.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), b => b.MigrationsAssembly(typeof(Startup).Assembly.FullName));
        });
    }
  }
}