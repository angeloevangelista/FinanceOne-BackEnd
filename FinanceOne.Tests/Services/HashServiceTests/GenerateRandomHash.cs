using Xunit;
using FinanceOne.Implementation.Services;

namespace FinanceOne.Tests.Services.HashServiceTests
{
  public class GenerateRandomHash
  {
    [Fact]
    public void ShouldGenerateARandomHashInEveryExecution()
    {
      var hashService = new HashService();

      var hashLength = 10;

      var hashA = hashService.GenerateRandomHash(hashLength);
      var hashB = hashService.GenerateRandomHash(hashLength);

      Assert.NotEqual(hashA, hashB);
      Assert.Equal(hashA.Length, hashLength);
      Assert.Equal(hashB.Length, hashLength);
    }
  }
}
