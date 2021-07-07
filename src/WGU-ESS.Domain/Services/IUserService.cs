using System.Collections.Generic;
using System.Threading.Tasks;
using WGU_ESS.Domain.Requests.User;
using WGU_ESS.Domain.Responses.User;

namespace WGU_ESS.Domain.Services
{
  public interface IUserService
  {
    Task<IEnumerable<UserResponse>> GetUsersAsync();
    Task<UserResponse> GetUserAsync(GetUserRequest request);
    Task<UserResponse> AddUserAsync(AddUserRequest request);
    // added in order to check if a username exists, regardless of status
    Task<UserResponse> GetByUserNameAsyncForUniquenessCheck(string username);
    Task<UserResponse> EditUserAsync(EditUserRequest request);
    Task<UserResponse> DeleteUserAsync(DeleteUserRequest request);
    Task<LoginResponse> AuthenticateUser(SignInRequest request);
  }
}