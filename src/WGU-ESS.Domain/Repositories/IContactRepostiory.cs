using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WGU_ESS.Domain.Entities;

namespace WGU_ESS.Domain.Repositories
{
  public interface IContactRepository : IRepository
  {
    Task<IEnumerable<Contact>> GetAsync();
    Task<Contact> GetAsync(Guid id);
    Contact Add(Contact Contact);
    Contact Update(Contact Contact);
  }
}