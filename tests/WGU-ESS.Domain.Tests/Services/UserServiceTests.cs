using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Shouldly;
using WGU_ESS.Domain.Entities;
using WGU_ESS.Domain.Mappers;
using WGU_ESS.Domain.Requests.User;
using WGU_ESS.Domain.Services;
using WGU_ESS.Fixtures;
using WGU_ESS.Infrastructure.Repositories;
using Xunit;

namespace WGU_ESS.Domain.Tests.Services
{
  public class UserServiceTests : IClassFixture<SystemContextFactory>
  {
    private readonly UserRepository _userRepository;
    private readonly ContactRepository _contactRepository;
    private readonly AppointmentRepository _appointmentRepository;
    private readonly PasswordService _passwordService;
    private readonly IUserMapper _userMapper;
    private readonly IConfiguration _configuration;

    public UserServiceTests(SystemContextFactory systemContextFactory)
    {
      _userRepository = new UserRepository(systemContextFactory.ContextInstance);
      _contactRepository = new ContactRepository(systemContextFactory.ContextInstance);
      _appointmentRepository = new AppointmentRepository(systemContextFactory.ContextInstance);
      _configuration = new ConfigurationBuilder().SetBasePath(AppContext.BaseDirectory).AddJsonFile("appsettings.json").Build();
      _passwordService = new PasswordService(); // not part of context but part of users
      _userMapper = systemContextFactory.UserMapper;
    }

    [Fact]
    public async Task logging_in_should_return_jwt()
    {
      UserService sut = new UserService(_userRepository, _contactRepository, _appointmentRepository, _passwordService, _userMapper, _configuration);
      
      var result = await sut.AuthenticateUser(new SignInRequest
      {
        UserName = "dwadman0",
        Password = "KiPycjHQO9"
      });

      result.Status.ShouldBe("Success");
      result.Token.ShouldNotBeNull();
    }

    [Fact]
    public async Task get_users_should_return_correct_data()
    {
      UserService sut = new UserService(_userRepository, _contactRepository, _appointmentRepository, _passwordService, _userMapper, _configuration);
      var result = await sut.GetUsersAsync();
      result.ShouldNotBeNull();
    }

    [Theory]
    [InlineData("e61044cd-a73a-4786-97f9-e1570cde84c7")]
    public async Task get_user_by_id_should_return_correct_user(string guid)
    {
      UserService sut = new UserService(_userRepository, _contactRepository, _appointmentRepository, _passwordService, _userMapper, _configuration);
      var result = await sut.GetUserAsync(new GetUserRequest { Id = new Guid(guid)} );
      result.Id.ShouldBe(new Guid(guid));
    }

    [Fact]
    public void get_user_should_throw_exception_with_null_id()
    {
      UserService sut = new UserService(_userRepository, _contactRepository, _appointmentRepository, _passwordService, _userMapper, _configuration);
      sut.GetUserAsync(null).ShouldThrow<ArgumentNullException>();
    }

    [Fact]
    public async Task add_user_should_add_right_entity()
    {
      var test_user = new AddUserRequest
      {
        FirstName = "New",
        LastName = "User",
        Password = "superSecurePassword!",
        Type = "Standard",
        UserName = "test_user",
        UsesDarkMode = false
      };

      UserService sut = new UserService(_userRepository, _contactRepository, _appointmentRepository, _passwordService, _userMapper, _configuration);
      var result = await sut.AddUserAsync(test_user);
      result.FirstName.ShouldBe(test_user.FirstName);
      result.LastName.ShouldBe(test_user.LastName);
      result.UserName.ShouldBe(test_user.UserName);
      result.Type.ShouldBe(UserType.Standard.ToString());
      result.UsesDarkMode.ShouldBe(false);
      result.CreationTime.ToString().ShouldNotBeNull();
    }

    [Fact]
    public async Task editUser_should_edit_the_right_entity()
    {
      var test_user = new EditUserRequest
      {
        Id = new Guid("66da25ef-198e-40b4-997e-af986cabf880"),
        FirstName = "New",
        LastName = "User",
        Password = "superSecurePassword!",
        Type = "Standard",
        UserName = "test_user",
        UsesDarkMode = false
      };

      UserService sut = new UserService(_userRepository, _contactRepository, _appointmentRepository, _passwordService, _userMapper, _configuration);
      var result = await sut.EditUserAsync(test_user);
      result.FirstName.ShouldBe(test_user.FirstName);
      result.LastName.ShouldBe(test_user.LastName);
      result.UserName.ShouldBe(test_user.UserName);
      result.Type.ShouldBe(UserType.Standard.ToString());
      result.UsesDarkMode.ShouldBe(false);
      result.CreationTime.ToString().ShouldNotBeNull();
      result.ModificationTime.ToString().ShouldNotBeNull();
    }

    [Theory]
    [InlineData("e61044cd-a73a-4786-97f9-e1570cde84c7")]
    public async Task delete_user_should_set_user_to_hidden(string guid)
    {
      var userToDelete = new DeleteUserRequest
      {
        Id = new Guid(guid)
      };

      UserService sut = new UserService(_userRepository, _contactRepository, _appointmentRepository, _passwordService, _userMapper, _configuration);
      var result = await sut.DeleteUserAsync(userToDelete);
      result.IsHidden = true;
    }
  }
}