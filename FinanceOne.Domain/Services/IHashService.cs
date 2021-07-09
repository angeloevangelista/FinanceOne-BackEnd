namespace FinanceOne.Domain.Services
{
  public interface IHashService
  {
    string Hash(string message);
    bool Verify(string message, string hash);
    string GenerateRandomHash(int length = 8);
  }
}
