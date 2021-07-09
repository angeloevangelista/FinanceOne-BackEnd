using System;
using System.Linq;
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

    public string GenerateRandomHash(int length = 8)
    {
      const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

      string hash = new string(Enumerable.Repeat(chars, length)
        .Select(s => s[new Random().Next(s.Length)]).ToArray());

      return hash;
    }
  }
}
