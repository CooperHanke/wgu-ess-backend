using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Xunit;
using WGU_ESS.Fixtures;
using WGU_ESS.Infrastructure.Repositories;
using Shouldly;
using System;
using WGU_ESS.Domain.Entities;
using System.Linq;

namespace WGU_ESS.Infrastructure.Tests
{
  public class UserRepostioryTests : IClassFixture<SystemContextFactory>
  {
    private readonly UserRepository _sut;
    private readonly TestSystemContext _context;
    
    public UserRepostioryTests(SystemContextFactory systemContextFactory)
    {
      _context = systemContextFactory.ContextInstance;
      _sut = new UserRepository(_context);
    }
    
    [Fact]
    public async Task should_get_data()
    {
      var result = await _sut.GetAsync();

      result.ShouldNotBeNull();
    }

    [Fact]
    public async Task should_return_null_with_id_not_present()
    {
      var result = await _sut.GetAsync(Guid.NewGuid());
      result.ShouldBeNull();
    }

    [Theory]
    [InlineData("e61044cd-a73a-4786-97f9-e1570cde84c7")]
    public async Task should_return_record_by_id(string guid)
    {
      var result = await _sut.GetAsync(new Guid(guid));
      result.Id.ShouldBe(new Guid(guid));
    }

    [Fact]
    public async Task should_add_new_user()
    {
      var test_user = new User
      {
        FirstName = "Test",
        LastName = "Dummy",
        UserName = "@test-dummy",
        Password = "secure_password",
        Type = UserType.Standard,
        IsHidden = false,
        IsLocked = false,
        UsesDarkMode = true
      };
      _sut.Add(test_user);
      await _sut.UnitOfWork.SaveEntitiesAsync();
      _context.Users.FirstOrDefault(_ => _.Id == test_user.Id).ShouldNotBeNull();
    }

    [Fact]
    public async Task should_update_user()
    {
      var test_user = _sut.GetAsync(new Guid("e61044cd-a73a-4786-97f9-e1570cde84c7")).Result;
      test_user.FirstName = "Test";
      _sut.Update(test_user);
      await _sut.UnitOfWork.SaveEntitiesAsync();
      var result = _context.Users.FirstOrDefault(x => x.Id == test_user.Id);
      result.FirstName.ShouldBe("Test");
    }
  }
}