using System.IO;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace WGU_ESS.Infrastructure.Tests.Extensions
{
  public static class ModelBuilderExtensions
  {
    public static ModelBuilder Seed<T>(this ModelBuilder builder, string file) where T : class
    {
      using (var reader = new StreamReader(file))
      {
        var json = reader.ReadToEnd();
        var data = JsonConvert.DeserializeObject<T[]>(json);
        builder.Entity<T>().HasData(data);
      }
      return builder;
    }
  }
}