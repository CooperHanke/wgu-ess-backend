using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WGU_ESS.Domain.Entities;
using WGU_ESS.Domain.Repositories;

namespace WGU_ESS.Infrastructure.Repositories
{
  public class UserRepository : IUserRepository
  {
    private readonly SystemContext _context;
    public IUnitOfWork UnitOfWork => _context;
    public UserRepository(SystemContext context)
    {
      _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public User Add(User user)
    {
      return _context.Users.Add(user).Entity;
    }

    public async Task<IEnumerable<User>> GetAsync()
    {
      return await _context.Users.AsNoTracking().Where(x => !x.IsHidden).ToListAsync();
    }

    public async Task<User> GetAsync(Guid id)
    {
      return await _context.Users.AsNoTracking().Where(x => !x.IsHidden).Where(x => x.Id == id).FirstOrDefaultAsync();
    }

    public User Update(User user)
    {
      _context.Entry(user).State = EntityState.Modified;
      return user;
    }

    // for authentication
    public async Task<User> GetByUserNameAsync(string username)
    {
      return await _context.Users.AsNoTracking().Where(x => !x.IsHidden).Where(x => x.UserName == username).FirstOrDefaultAsync();
    }

    // for checking username for uniqueness
    public async Task<User> GetByUserNameAsyncForUniquenessCheck(string username)
    {
      return await _context.Users.AsNoTracking().Where(x => x.UserName == username).FirstOrDefaultAsync();
    }
  }
}