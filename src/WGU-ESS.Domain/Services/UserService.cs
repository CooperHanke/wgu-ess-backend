using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
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
    private readonly IConfiguration _configuration;

    public UserService(IUserRepository userRepository, IUserMapper userMapper, IConfiguration configuration)
    {
      _userRepository = userRepository;
      _userMapper = userMapper;
      _configuration = configuration;
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

    public async Task<UserResponse> GetByUserNameAsyncForUniquenessCheck(string username)
    {
      var existingUser = await _userRepository.GetByUserNameAsyncForUniquenessCheck(username);
      return _userMapper.Map(existingUser);
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

      request.Password = Hash(request.Password);

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

      if (request.Password == null)
      {
        request.Password = existingUser.Password;
      }
      else
      {
        request.Password = Hash(request.Password);
      }

      var entity = _userMapper.Map(request);
      entity.CreationTime = existingUser.CreationTime;
      entity.ModificationTime = DateTime.UtcNow;

      var result = _userRepository.Update(entity);
      await _userRepository.UnitOfWork.SaveChangesAsync();
      return _userMapper.Map(result);
    }

    public async Task<UserResponse> DeleteUserAsync(DeleteUserRequest request)
    {
      if (request?.Id == null) throw new ArgumentNullException();

      var result = await _userRepository.GetAsync(request.Id);
      result.ModificationTime = DateTime.UtcNow;
      result.IsHidden = true;

      _userRepository.Update(result);
      await _userRepository.UnitOfWork.SaveChangesAsync();

      return _userMapper.Map(result);
    }

    public async Task<LoginResponse> AuthenticateUser(SignInRequest request)
    {
      var response = new LoginResponse { Token = null, Status = "Failure", UserId = null };

      var user = await _userRepository.GetByUserNameAsync(request.UserName);

      if (user != null && user.IsLocked.Equals(true))
      {
        {
          response.UserId = user.Id.ToString();
          response.Status = "Locked";
          return response;
        }
      }

      if (user != null && PasswordMatches(user.Password, request.Password))
      {
        var claims = new[] {
          new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
          new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
          new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
          new Claim("UserId", user.Id.ToString()),
          new Claim("FirstName", user.FirstName),
          new Claim("LastName", user.LastName),
          new Claim("UserName", user.UserName),
          new Claim("roles", user.Type.ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

        var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
          _configuration["Jwt:Issuer"],
          _configuration["Jwt:Audience"],
          claims,
          expires: DateTime.UtcNow.AddDays(1),
          signingCredentials: signIn);

        response.Token = new JwtSecurityTokenHandler().WriteToken(token);
        response.UserId = user.Id.ToString();
        response.Status = "Success";
      }

      return response;
    }

    private string Hash(string password)
    {
      int saltSize = 16; // 128 bit 
      int keySize = 32; // 256 bit
      int iterations = 10000;
      using (var algorithm = new Rfc2898DeriveBytes(
        password,
        saltSize,
        iterations,
        HashAlgorithmName.SHA512))
      {
        var key = Convert.ToBase64String(algorithm.GetBytes(keySize));
        var salt = Convert.ToBase64String(algorithm.Salt);

        return $"{iterations}.{salt}.{key}";
      }
    }

    private bool PasswordMatches(string hash, string password)
    {
      int keySize = 32; // 256 bit
      var parts = hash.Split('.', 3);

      if (parts.Length == 3)
      {
        var iterations = Convert.ToInt32(parts[0]);
        var salt = Convert.FromBase64String(parts[1]);
        var key = Convert.FromBase64String(parts[2]);

        using (var algorithm = new Rfc2898DeriveBytes(
          password,
          salt,
          iterations,
          HashAlgorithmName.SHA512))
        {
          if (algorithm.GetBytes(keySize).SequenceEqual(key))
          {
            return true;
          }
        }
      }

      return false;
    }

  }
}