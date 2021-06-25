using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Xunit;
using WGU_ESS.Infrastructure.Repositories;
using Shouldly;
using System;
using WGU_ESS.Domain.Entities;
using System.Linq;
using WGU_ESS.Fixtures;

namespace WGU_ESS.Infrastructure.Tests
{
  public class ContactRepositoryTests : IClassFixture<SystemContextFactory>
  {
    private readonly ContactRepository _sut;
    private readonly TestSystemContext _context;

    public ContactRepositoryTests(SystemContextFactory systemContextFactory)
    {
      _context = systemContextFactory.ContextInstance;
      _sut = new ContactRepository(_context);
    }

    [Fact]
    public async Task should_get_all_contacts()
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
    [InlineData("bfed1d5f-a8dd-4ea9-a69d-4d21b42c164a")]
    public async Task should_return_contact_by_id(string guid)
    {
      var result = await _sut.GetAsync(new Guid(guid));
      result.Id.ShouldBe(new Guid(guid));
    }

    [Fact]
    public async Task should_add_new_contact()
    {
      var test_contact = new Contact
      {
        FirstName = "New",
        LastName = "Contact",
        Address1 = "217 Cottonwood Trail",
        City = "Jianyangping",
        State = "Texas",
        Country = "USA",
        Email = "bgonnint@wikispaces.com",
        PhoneNumber = "1234567890",
        IsHidden = false,
        UserId = new Guid("1d3fc940-c63a-4951-aabe-3f441297ab52")
      };
      _sut.Add(test_contact);
      await _sut.UnitOfWork.SaveEntitiesAsync();
      _context.Contacts.FirstOrDefault(_ => _.Id == test_contact.Id).ShouldNotBeNull();
      var result = _context.Contacts.FirstOrDefault(x => x.Id == test_contact.Id);
      result.UserId.ShouldBe(new Guid("1d3fc940-c63a-4951-aabe-3f441297ab52"));
    }

    [Fact]
    public async Task should_update_contact()
    {
      var test_contact = _sut.GetAsync(new Guid("bfed1d5f-a8dd-4ea9-a69d-4d21b42c164a")).Result;
      test_contact.FirstName = "Test";
      _sut.Update(test_contact);
      await _sut.UnitOfWork.SaveEntitiesAsync();
      var result = _context.Contacts.FirstOrDefault(x => x.Id == test_contact.Id);
      result.FirstName.ShouldBe("Test");
    }
  }
}