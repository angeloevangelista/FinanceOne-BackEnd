using FinanceOne.Implementation.Services;
using Xunit;

namespace FinanceOne.Tests.Services.HashServiceTests
{
  public class Verify
  {
    [Fact]
    public void ShouldMatchMessageAndHash()
    {
      var hashService = new HashService();

      var message = "With great power, comes great responsibility.";

      var hashedMessage = hashService.Hash(message);

      var hasMatch = hashService.Verify(message, hashedMessage);

      Assert.True(hasMatch);
    }

    [Fact]
    public void ShouldNotMatchMessageAndHash()
    {
      var hashService = new HashService();

      var message = "With great power, comes great responsibility.";
      var anotherMessage = "Be afraid of spiders.";

      var hashedMessage = hashService.Hash(message);

      var hasMatch = hashService.Verify(anotherMessage, hashedMessage);

      Assert.False(hasMatch);
    }
  }
}
