using System;
using System.Threading.Tasks;
using Shouldly;
using WGU_ESS.Domain.Mappers;
using WGU_ESS.Domain.Requests.Contact;
using WGU_ESS.Domain.Services;
using WGU_ESS.Fixtures;
using WGU_ESS.Infrastructure.Repositories;
using Xunit;

namespace WGU_ESS.Domain.Tests.Services
{
  public class ContactServiceTests : IClassFixture<SystemContextFactory>
  {
    private readonly ContactRepository _contactRepository;
    private readonly IContactMapper _contactMapper;

    public ContactServiceTests(SystemContextFactory systemContextFactory)
    {
      _contactRepository = new ContactRepository(systemContextFactory.ContextInstance);
      _contactMapper = systemContextFactory.ContactMapper;
    }

    [Fact]
    public async Task get_contacts_should_return_correct_data()
    {
      ContactService sut = new ContactService(_contactRepository, _contactMapper);
      var result = await sut.GetContactsAsync();
      result.ShouldNotBeNull();
    }

    [Theory]
    [InlineData("f161a106-dea4-4132-a015-f8a6a66cf0cd")]
    public async Task get_contact_by_id_should_return_correct_contact(string guid)
    {
      ContactService sut = new ContactService(_contactRepository, _contactMapper);
      var result = await sut.GetContactAsync(new GetContactRequest { Id = new Guid(guid)} );
      result.Id.ShouldBe(new Guid(guid));
      result.UserId.ShouldBe(new Guid("e61044cd-a73a-4786-97f9-e1570cde84c7"));
    }

    [Fact]
    public void get_contact_should_throw_exception_with_null_id()
    {
      ContactService sut = new ContactService(_contactRepository, _contactMapper);
      sut.GetContactAsync(null).ShouldThrow<ArgumentNullException>();
    }

    [Fact]
    public async Task add_contact_should_add_right_entity()
    {
      var test_contact = new AddContactRequest
      {
        FirstName = "New",
        LastName = "Contact",
        Address1 = "123 Anywhere Rd",
        Address2 = "Nullable?",
        City = "Atlanta",
        State = "Georgia",
        PostalCode = "00000",
        Country = "USA",
        PhoneNumber = "1234567890",
        Email = "newcontact@test.net",
        UserId = new Guid("66da25ef-198e-40b4-997e-af986cabf880")
      };

      ContactService sut = new ContactService(_contactRepository, _contactMapper);
      var result = await sut.AddContactAsync(test_contact);

      result.Id.ToString().ShouldNotBeNull();
      result.FirstName.ShouldBe(test_contact.FirstName);
      result.LastName.ShouldBe(test_contact.LastName);
      result.Address1.ShouldBe(test_contact.Address1);
      result.City.ShouldBe(test_contact.City);
      result.State.ShouldBe(test_contact.State);
      result.PostalCode.ShouldBe(test_contact.PostalCode);
      result.Country.ShouldBe(test_contact.Country);
      result.PhoneNumber.ShouldBe(test_contact.PhoneNumber);
      result.Email.ShouldBe(test_contact.Email);
      result.UserId.ShouldBe(test_contact.UserId);
      result.CreationTime.ToString().ShouldNotBeNull();
    }

    [Fact]
    public async Task edit_contact_should_edit_the_right_entity()
    {
      var test_contact = new EditContactRequest
      {
        Id = new Guid("f161a106-dea4-4132-a015-f8a6a66cf0cd"),
        FirstName = "New",
        LastName = "Contact",
        Address1 = "123 Anywhere Rd",
        Address2 = "Nullable?",
        City = "Atlanta",
        State = "Georgia",
        PostalCode = "00000",
        Country = "USA",
        PhoneNumber = "1234567890",
        Email = "newcontact@test.net",
        IsHidden = false,
        UserId = new Guid("e61044cd-a73a-4786-97f9-e1570cde84c7")
      };

      ContactService sut = new ContactService(_contactRepository, _contactMapper);
      var result = await sut.EditContactAsync(test_contact);

      result.Id.ToString().ShouldNotBeNull();
      result.FirstName.ShouldBe(test_contact.FirstName);
      result.LastName.ShouldBe(test_contact.LastName);
      result.Address1.ShouldBe(test_contact.Address1);
      result.City.ShouldBe(test_contact.City);
      result.State.ShouldBe(test_contact.State);
      result.PostalCode.ShouldBe(test_contact.PostalCode);
      result.Country.ShouldBe(test_contact.Country);
      result.PhoneNumber.ShouldBe(test_contact.PhoneNumber);
      result.Email.ShouldBe(test_contact.Email);
      result.UserId.ShouldBe(test_contact.UserId);
      result.CreationTime.ToString().ShouldNotBeNull();
      result.ModificationTime.ToString().ShouldNotBeNull();
    }
  }
}