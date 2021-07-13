namespace WGU_ESS.Domain.Services
{
  public interface IPasswordService
  {
    public string Hash(string password);
    public bool PasswordMatches(string hash, string password);
  }
}