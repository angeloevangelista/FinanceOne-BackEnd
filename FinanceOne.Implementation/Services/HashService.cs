using FinanceOne.Domain.Services;

using BCryptNet = BCrypt.Net.BCrypt;

namespace FinanceOne.Implementation.Services
{
  public class HashService : IHashService
  {
    public string Hash(string message)
    {
      var hash = BCryptNet.HashPassword(message);

      return hash;
    }

    public bool Verify(string message, string hash)
    {
      bool hashMatch = BCryptNet.Verify(message, hash);

      return hashMatch;
    }
  }
}
