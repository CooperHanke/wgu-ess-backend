using WGU_ESS.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WGU_ESS.Domain.Requests.User;
using System;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace WGU_ESS.API.Controllers
{
  [ApiController]
  [Route("/api/users")]
  public class UserController : ControllerBase
  {
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
      _userService = userService;
    }

    [Authorize(Roles = "Manager")] // only managers should be able to get all users, in order to manage them
    [HttpGet]
    public async Task<IActionResult> Get()
    {
      var result = await _userService.GetUsersAsync();
      return Ok(result);
    }

    [Authorize]
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
      var result = await _userService.GetUserAsync(new GetUserRequest { Id = id });
      return Ok(result);
    }

    [Authorize(Roles = "Manager")] // only a manager should be able to add a user
    [HttpPost]
    public async Task<IActionResult> Post(AddUserRequest request)
    {
      // check for duplicate first and last name, as well as username
      // first, we pull down all the users
      var existingUser = await _userService.GetByUserNameAsyncForUniquenessCheck(request.UserName);
      if (existingUser != null) return BadRequest($"A user with username '{request.UserName}' already exists; please choose another username");

      var result = await _userService.AddUserAsync(request);
      return CreatedAtAction(nameof(GetById), new { id = result.Id }, null);
    }

    [Authorize]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Put(Guid id, EditUserRequest request)
    {
      // first, we make a copy of the user so we can get the password, in case it is not being changed
      var user = await _userService.GetUserAsync(new GetUserRequest { Id = request.Id });

      // if the user is regular, only accept password and dark mode preference
      // however, if they are a manager, change all fields
      if (User.HasClaim(ClaimTypes.Role, "Manager"))
      {
        var result = await _userService.EditUserAsync(request);
        return Ok(result);
      }
      // we check and ensure that only a valid claim for the same user can edit a user
      else if (User.FindFirstValue("UserId") == request.Id.ToString())
      {
        // next, we recreate the edit user request
        var result = await _userService.EditUserAsync(new EditUserRequest
        {
          Id = user.Id,
          FirstName = user.FirstName,
          LastName = user.LastName,
          UserName = user.UserName,
          Password = request.Password,
          UsesDarkMode = request.UsesDarkMode,
          NeedPasswordReset = user.NeedPasswordReset,
          Type = user.Type,
          IsLocked = user.IsLocked,
          IsHidden = user.IsHidden
        });
        return Ok(result);
      }
      else {
        return BadRequest("Current user doesn't have permission to change another user's password");
      }
    }

    [Authorize(Roles = "Manager")]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
      var request = new DeleteUserRequest { Id = id };
      await _userService.DeleteUserAsync(request);
      return NoContent();
    }

    [HttpPost("auth")]  // to authenticate
    public async Task<IActionResult> SignIn(SignInRequest request)
    {
      var result = await _userService.AuthenticateUser(request);

      if (result.Token != null)
      {
        return Ok(result);
      }
      else if (result.Status == "Locked")
      {
        return BadRequest("You are currently locked from this system. Please reach out to a manager to have access restored.");
      }
      else 
      {
      return BadRequest("Authentication failed. Please ensure that your username and password is correct, or reach out to a manager to have your password reset.");
      }
    }
  }
}
