using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WGU_ESS.Domain.Mappers;
using WGU_ESS.Domain.Repositories;
using WGU_ESS.Domain.Requests.User;
using WGU_ESS.Domain.Responses.User;

namespace WGU_ESS.Domain.Services
{
  public class UserService : IUserService
  {
    private readonly IUserRepository _userRepository;
    private readonly IUserMapper _userMapper;
    
    public UserService(IUserRepository userRepository, IUserMapper userMapper)
    {
      _userRepository = userRepository;
      _userMapper = userMapper;
    }

    public async Task<IEnumerable<UserResponse>> GetUsersAsync()
    {
      var result = await _userRepository.GetAsync();
      return result.Select(x => _userMapper.Map(x));
    }

    public async Task<UserResponse> GetUserAsync(GetUserRequest request)
    {
      if (request?.Id == null) throw new ArgumentNullException();
      var entity = await _userRepository.GetAsync(request.Id);
      return _userMapper.Map(entity);
    }

    public async Task<UserResponse> AddUserAsync(AddUserRequest request)
    {
      var guidNotGood = true;
      var newId = Guid.NewGuid();
      while (guidNotGood)
      {
        var existingUser = await _userRepository.GetAsync(newId);
        if (existingUser == null)
        {
          guidNotGood = false;
        }
        else
        {
          newId = Guid.NewGuid();
        }
      }

      var user = _userMapper.Map(request);
      user.Id = newId;
      var utcDateTime = DateTime.UtcNow;
      user.CreationTime = utcDateTime;
      user.ModificationTime = utcDateTime;
      var result = _userRepository.Add(user);
      await _userRepository.UnitOfWork.SaveChangesAsync();
      return _userMapper.Map(result);
    }

    public async Task<UserResponse> EditUserAsync(EditUserRequest request)
    {
      var existingUser = await _userRepository.GetAsync(request.Id);
      if (existingUser == null)
      {
        throw new ArgumentException($"User with ID {request.Id} is not present");
      }
      var entity = _userMapper.Map(request);
      entity.CreationTime = existingUser.CreationTime;
      entity.ModificationTime = DateTime.UtcNow;
      var result = _userRepository.Update(entity);
      await _userRepository.UnitOfWork.SaveChangesAsync();
      return _userMapper.Map(result);
    }
  }
}