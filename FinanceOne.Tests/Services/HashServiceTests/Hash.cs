using FinanceOne.Implementation.Services;
using Xunit;

namespace FinanceOne.Tests.Services.HashServiceTests
{
  public class Hash
  {
    [Fact]
    public void ShouldHashTheGivenString()
    {
      var hashService = new HashService();

      var message = "With great power, comes great responsibility.";

      var hashedMessage = hashService.Hash(message);

      Assert.NotEqual(message, hashedMessage);
    }
  }
}
