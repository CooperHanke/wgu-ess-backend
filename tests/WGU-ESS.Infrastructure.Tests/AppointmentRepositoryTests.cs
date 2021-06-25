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
  public class AppointmentRepositoryTests : IClassFixture<SystemContextFactory>
  {
    private readonly AppointmentRepository _sut;
    private readonly TestSystemContext _context;

    public AppointmentRepositoryTests(SystemContextFactory systemContextFactory)
    {
      _context = systemContextFactory.ContextInstance;
      _sut = new AppointmentRepository(_context);
    }

    [Fact]
    public async Task should_get_all_appointments()
    {
      var result = await _sut.GetAsync();

      result.ShouldNotBeNull();
    }

    // [Fact]
    // public async Task should_return_null_with_id_not_present()
    // {
    //   var options = new DbContextOptionsBuilder<SystemContext>().UseInMemoryDatabase(databaseName: "should_return_null_with_id_not_present").Options;
    //   await using var context = new TestSystemContext(options);
    //   context.Database.EnsureCreated();
    //   var _sut = new ContactRepository(context);

    //   var result = await _sut.GetAsync(Guid.NewGuid());
    //   result.ShouldBeNull();
    // }

    // [Theory]
    // [InlineData("bfed1d5f-a8dd-4ea9-a69d-4d21b42c164a")]
    // public async Task should_return_contact_by_id(string guid)
    // {
    //   var options = new DbContextOptionsBuilder<SystemContext>().UseInMemoryDatabase(databaseName: "should_return_record_by_id").Options;
    //   await using var context = new TestSystemContext(options);
    //   context.Database.EnsureCreated();
    //   var _sut = new ContactRepository(context);
    //   var result = await _sut.GetAsync(new Guid(guid));
    //   result.Id.ShouldBe(new Guid(guid));
    // }

    // [Fact]
    // public async Task should_add_new_contact()
    // {
    //   var test_contact = new Contact
    //   {
    //     FirstName = "New",
    //     LastName = "Contact",
    //     Address1 = "217 Cottonwood Trail",
    //     City = "Jianyangping",
    //     State = "Texas",
    //     Country = "USA",
    //     Email = "bgonnint@wikispaces.com",
    //     PhoneNumber = "1234567890",
    //     IsHidden = false,
    //     UserId = new Guid("1d3fc940-c63a-4951-aabe-3f441297ab52")
    //   };
    //   var options = new DbContextOptionsBuilder<SystemContext>().UseInMemoryDatabase(databaseName: "should_add_new_contact").Options;
    //   await using var context = new TestSystemContext(options);
    //   context.Database.EnsureCreated();
    //   var _sut = new ContactRepository(context);
    //   _sut.Add(test_contact);
    //   await _sut.UnitOfWork.SaveEntitiesAsync();
    //   context.Contacts.FirstOrDefault(_ => _.Id == test_contact.Id).ShouldNotBeNull();
    //   var result = context.Contacts.FirstOrDefault(x => x.Id == test_contact.Id);
    //   result.UserId.ShouldBe(new Guid("1d3fc940-c63a-4951-aabe-3f441297ab52"));
    // }

    // [Fact]
    // public async Task should_update_contact()
    // {
    //   var options = new DbContextOptionsBuilder<SystemContext>().UseInMemoryDatabase(databaseName: "should_update_contact").Options;
    //   await using var context = new TestSystemContext(options);
    //   context.Database.EnsureCreated();
    //   var _sut = new ContactRepository(context);
    //   var test_contact = _sut.GetAsync(new Guid("bfed1d5f-a8dd-4ea9-a69d-4d21b42c164a")).Result;
    //   test_contact.FirstName = "Test";
    //   _sut.Update(test_contact);
    //   await _sut.UnitOfWork.SaveEntitiesAsync();
    //   var result = context.Contacts.FirstOrDefault(x => x.Id == test_contact.Id);
    //   result.FirstName.ShouldBe("Test");
    // }
  }
}