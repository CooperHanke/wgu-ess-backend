﻿using WGU_ESS.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WGU_ESS.Domain.Requests.User;
using System;
using System.IdentityModel.Tokens.Jwt;
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
      var result = await _userService.AddUserAsync(request);
      return CreatedAtAction(nameof(GetById), new { id = result.Id }, null);
    }

    [Authorize]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Put(Guid id, EditUserRequest request)
    {
      request.Id = id;

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
        // first, we make a copy of the user as they are by retrieving the user
        var user = await _userService.GetUserAsync(new GetUserRequest { Id = request.Id });
        // next, we recreate the edit user request
        var result = await _userService.EditUserAsync(new EditUserRequest
        {
          Id = user.Id,
          FirstName = user.FirstName,
          LastName = user.LastName,
          UserName = user.UserName,
          Password = request.Password,
          UsesDarkMode = request.UsesDarkMode,
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

    [HttpPost("auth")]  // to authenticate
    public async Task<IActionResult> SignIn(SignInRequest request)
    {
      var result = await _userService.AuthenticateUser(request);

      if (result.Token != null)
      {
        return Ok(result);
      }
      return BadRequest("Invalid credintials");
    }
  }
}
