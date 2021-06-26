using System.IdentityModel.Tokens.Jwt;

namespace WGU_ESS.Domain.Responses.User
{
  public class LoginResponse
  {
    public string Status { get; set; }
    public JwtSecurityToken Token { get; set; }
  }
}