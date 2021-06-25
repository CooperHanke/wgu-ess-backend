using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WGU_ESS.Domain.Entities;
using WGU_ESS.Domain.Repositories;

namespace WGU_ESS.Infrastructure.Repositories
{
  public class ContactRepository : IContactRepository
  {
    private readonly SystemContext _context;
    public IUnitOfWork UnitOfWork => _context;
    public ContactRepository(SystemContext context)
    {
      _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public Contact Add(Contact contact)
    {
      return _context.Contacts.Add(contact).Entity;
    }

    public async Task<IEnumerable<Contact>> GetAsync()
    {
      return await _context.Contacts.AsNoTracking().ToListAsync();
    }

    public async Task<Contact> GetAsync(Guid id)
    {
      return await _context.Contacts.AsNoTracking().Where(x => x.Id == id).FirstOrDefaultAsync();
    }

    public Contact Update(Contact contact)
    {
      _context.Entry(contact).State = EntityState.Modified;
      return contact;
    }
  }
}