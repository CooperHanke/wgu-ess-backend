using System;
using WGU_ESS.Domain.Entities;
using WGU_ESS.Domain.Requests.User;
using WGU_ESS.Domain.Responses.User;

namespace WGU_ESS.Domain.Mappers
{
  public class UserMapper : IUserMapper
  {
    public User Map(AddUserRequest request)
    {
      if (request == null) return null;
      var user = new User
      {
        FirstName = request.FirstName,
        LastName = request.LastName,
        UserName = request.UserName,
        Password = request.Password,
        Type = (UserType) Enum.Parse(typeof(UserType), request.Type),
        UsesDarkMode = request.UsesDarkMode
      };

      return user;
    }

    public User Map(EditUserRequest request)
    {
      if (request == null) return null;
      var user = new User
      {
        Id = request.Id,
        FirstName = request.FirstName,
        LastName = request.LastName,
        UserName = request.UserName,
        Password = request.Password,
        Type = (UserType) Enum.Parse(typeof(UserType), request.Type),
        UsesDarkMode = request.UsesDarkMode,
        IsHidden = request.IsHidden,
        IsLocked = request.IsLocked,
        NeedPasswordReset = request.NeedPasswordReset
      };

      return user;
    }

    public UserResponse Map(User request)
    {
      if (request == null) return null;
      var response = new UserResponse
      {
        Id = request.Id,
        FirstName = request.FirstName,
        LastName = request.LastName,
        UserName = request.UserName,
        Password = request.Password, // the password is visible to system, but not in json payload
        Type = request.Type.ToString(),
        UsesDarkMode = request.UsesDarkMode,
        IsLocked = request.IsLocked,
        IsHidden = request.IsHidden,
        NeedPasswordReset = request.NeedPasswordReset,
        CreationTime = request.CreationTime.ToString("yyyy-MM-ddTHH:mm:ssZ"),
        ModificationTime = request.ModificationTime.ToString("yyyy-MM-ddTHH:mm:ssZ")
      };
      return response;
    }
  }
}