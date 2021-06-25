using WGU_ESS.Domain.Entities;
using WGU_ESS.Domain.Requests.User;
using WGU_ESS.Domain.Responses.User;

namespace WGU_ESS.Domain.Mappers
{
  public interface IUserMapper
  {
    User Map(AddUserRequest request);
    User Map(EditUserRequest request);
    UserResponse Map(User request);
  }
}