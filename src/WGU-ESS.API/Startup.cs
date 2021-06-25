using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WGU_ESS.API.Extensions;
using WGU_ESS.Domain.Extensions;
using WGU_ESS.Domain.Repositories;
using WGU_ESS.Infrastructure.Repositories;

namespace WGU_ESS.API
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddSystemContext(Configuration.GetSection("Datasource:ConnectionString").Value)
          .AddScoped<IUserRepository, UserRepository>()
          .AddScoped<IContactRepository, ContactRepository>()
          .AddScoped<IAppointmentRepository, AppointmentRepository>()
          .AddMappers()
          .AddServices()
          .AddControllers();
        }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseHttpsRedirection();

      app.UseRouting();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
