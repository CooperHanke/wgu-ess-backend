using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WGU_ESS.Infrastructure;

namespace WGU_ESS.Fixtures
{
  public class InMemoryApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
  {
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
      builder.UseEnvironment("Testing")
        .ConfigureTestServices(services =>
        {
          var options = new DbContextOptionsBuilder<SystemContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

          services.AddScoped<SystemContext>(serviceProvider => new TestSystemContext(options));
          var sp = services.BuildServiceProvider();

          using var scope = sp.CreateScope();
          var scopedServices = scope.ServiceProvider;
          var db = scopedServices.GetRequiredService<SystemContext>();
          db.Database.EnsureCreated();
        });
    }
  }
}