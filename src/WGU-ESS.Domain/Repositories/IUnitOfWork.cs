using System;
using System.Threading;
using System.Threading.Tasks;

namespace WGU_ESS.Domain.Repositories
{
  public interface IUnitOfWork : IDisposable
  {
    Task<int> SaveChangesAsync(CancellationToken token = default);
    Task<bool> SaveEntitiesAsync(CancellationToken token = default);
  }
}