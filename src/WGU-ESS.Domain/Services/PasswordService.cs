using System;
using System.Linq;
using System.Security.Cryptography;

namespace WGU_ESS.Domain.Services
{
  public class PasswordService : IPasswordService
  {
    public string Hash(string password)
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

    public bool PasswordMatches(string hash, string password)
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