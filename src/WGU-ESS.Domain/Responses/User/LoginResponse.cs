using System.IdentityModel.Tokens.Jwt;

namespace WGU_ESS.Domain.Responses.User
{
  public class LoginResponse
  {
    public string Status { get; set; }
    public string Token { get; set; }
    public string UserId { get; set; }
  }
}