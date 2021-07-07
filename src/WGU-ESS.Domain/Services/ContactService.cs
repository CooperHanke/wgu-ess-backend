using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WGU_ESS.Domain.Mappers;
using WGU_ESS.Domain.Repositories;
using WGU_ESS.Domain.Requests.Contact;
using WGU_ESS.Domain.Responses.Contact;

namespace WGU_ESS.Domain.Services
{
  public class ContactService : IContactService
  {
    private readonly IContactRepository _contactRepository;
    private readonly IContactMapper _contactMapper;
    
    public ContactService(IContactRepository contactRepository, IContactMapper contactMapper)
    {
      _contactRepository = contactRepository;
      _contactMapper = contactMapper;
    }

    public async Task<IEnumerable<ContactResponse>> GetContactsAsync()
    {
      var result = await _contactRepository.GetAsync();
      return result.Select(x => _contactMapper.Map(x));
    }

    public async Task<ContactResponse> GetContactAsync(GetContactRequest request)
    {
      if (request?.Id == null) throw new ArgumentNullException();
      var entity = await _contactRepository.GetAsync(request.Id);
      return _contactMapper.Map(entity);
    }

    public async Task<ContactResponse> AddContactAsync(AddContactRequest request)
    {
      var guidNotGood = true;
      var newId = Guid.NewGuid();
      while (guidNotGood)
      {
        var existingContact = await _contactRepository.GetAsync(newId);
        if (existingContact == null)
        {
          guidNotGood = false;
        }
        else
        {
          newId = Guid.NewGuid();
        }
      }
      
      var contact = _contactMapper.Map(request);
      contact.Id = newId;
      var utcDateTime = DateTime.UtcNow;
      contact.CreationTime = utcDateTime;
      contact.ModificationTime = utcDateTime;
      var result = _contactRepository.Add(contact);
      await _contactRepository.UnitOfWork.SaveChangesAsync();
      return _contactMapper.Map(result);
    }

    public async Task<ContactResponse> EditContactAsync(EditContactRequest request)
    {
      var existingContact = await _contactRepository.GetAsync(request.Id);
      if (existingContact == null)
      {
        throw new ArgumentException($"Contact with ID {request.Id} is not present");
      }
      var entity = _contactMapper.Map(request);
      entity.CreationTime = existingContact.CreationTime;
      entity.ModificationTime = DateTime.UtcNow;
      var result = _contactRepository.Update(entity);
      await _contactRepository.UnitOfWork.SaveChangesAsync();
      return _contactMapper.Map(result);
    }

    public async Task<ContactResponse> DeleteContactAsync(DeleteContactRequest request)
    {
      if (request?.Id == null) throw new ArgumentNullException();

      var result = await _contactRepository.GetAsync(request.Id);
      result.ModificationTime = DateTime.UtcNow;
      result.IsHidden = true;

      _contactRepository.Update(result);
      await _contactRepository.UnitOfWork.SaveChangesAsync();

      return _contactMapper.Map(result);
    }
  }
}