using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WGU_ESS.Domain.Entities;

namespace WGU_ESS.Domain.Repositories
{
  public interface IUserRepository : IRepository
  {
    Task<IEnumerable<User>> GetAsync();
    Task<User> GetAsync(Guid id);
    User Add(User user);
    User Update(User user);
  }
}