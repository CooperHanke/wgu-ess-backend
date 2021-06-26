using WGU_ESS.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WGU_ESS.Domain.Requests.User;
using System;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;

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

    [Authorize]
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
      var result = await _userService.EditUserAsync(request);
      return Ok(result);
    }

    // to authenticate
    [HttpPost("auth")]
    public async Task<IActionResult> SignIn(SignInRequest request)
    {
      var result = await _userService.AuthenticateUser(request);
      
      if (result.Token != null)
      {
        return Ok(new JwtSecurityTokenHandler().WriteToken(result.Token));
      }
      return BadRequest("Invalid credintials");
    }
  }
}
