using WGU_ESS.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WGU_ESS.Domain.Requests.Contact;
using System;
using Microsoft.AspNetCore.Authorization;

namespace WGU_ESS.API.Controllers
{
  [Authorize]
  [ApiController]
  [Route("/api/contacts")]
  public class ContactController : ControllerBase
  {
    private readonly IContactService _contactService;

    public ContactController(IContactService contactService)
    {
      _contactService = contactService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
      var result = await _contactService.GetContactsAsync();
      return Ok(result);
    }

    [HttpGet("user/{id:guid}")]
    public async Task<IActionResult> GetByUserId(Guid userId)
    {
      var result = await _contactService.GetContactsByUserIdAsync(new GetContactByUserIdRequest { UserId = userId } );
      return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
      var result = await _contactService.GetContactAsync(new GetContactRequest { Id = id } );
      return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Post(AddContactRequest request)
    {
      var result = await _contactService.AddContactAsync(request);
      return CreatedAtAction(nameof(GetById), new { id = result.Id }, null);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Put(Guid id, EditContactRequest request)
    {
      request.Id = id;
      var result = await _contactService.EditContactAsync(request);
      return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
      var request = new DeleteContactRequest { Id = id };
      await _contactService.DeleteContactAsync(request);
      return NoContent();
    }
  }
}
