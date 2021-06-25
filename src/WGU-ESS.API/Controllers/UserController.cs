using WGU_ESS.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WGU_ESS.Domain.Requests.User;
using System;

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

    [HttpGet]
    public async Task<IActionResult> Get()
    {
      var result = await _userService.GetUsersAsync();
      return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
      var result = await _userService.GetUserAsync(new GetUserRequest { Id = id } );
      return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Post(AddUserRequest request)
    {
      var result = await _userService.AddUserAsync(request);
      return CreatedAtAction(nameof(GetById), new { id = result.Id }, null);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Put(Guid id, EditUserRequest request)
    {
      request.Id = id;
      var result = await _userService.EditUserAsync(request);
      return Ok(result);
    }
  }
}
