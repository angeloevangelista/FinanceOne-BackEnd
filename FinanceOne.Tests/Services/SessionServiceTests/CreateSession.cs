using FinanceOne.Domain.ViewModels.SessionViewModels;
using FinanceOne.Implementation.Services;
using FinanceOne.Shared.Exceptions;
using FinanceOne.Shared.Services;
using FinanceOne.Tests.Mocks.Services;
using Xunit;

namespace FinanceOne.Tests.Services.SessionServiceTests
{
  public class CreateSession
  {
    [Fact]
    public void ShouldCreateASession()
    {
      var configuration = SessionServiceTestsUtil.GetConfiguration();

      var jwtService = new JwtService(configuration);
      var hashService = new HashService();
      var userRepositoryMock = new UserRepositoryMock();
      var refreshTokenRepositoryMock = new RefreshTokenRepositoryMock();

      var sessionService = new SessionService(
        jwtService,
        hashService,
        userRepositoryMock,
        refreshTokenRepositoryMock
      );

      var userPassword = "12345678";

      var user = SessionServiceTestsUtil.CreateUser(
        userRepositoryMock,
        userPassword
      );

      var createSessionViewModel = new CreateSessionViewModel()
      {
        Email = user.Email,
        Password = userPassword
      };

      createSessionViewModel.DoValidation().ThrowBusinessExceptionIfNotValid();

      var sessionPayload = sessionService.CreateSession(createSessionViewModel);

      Assert.NotNull(sessionPayload);
      Assert.NotNull(sessionPayload.Token);
      Assert.NotNull(sessionPayload.RefreshToken);
    }

    [Fact]
    public void ShouldNotCreateASessionWithWrongCredentials()
    {
      var configuration = SessionServiceTestsUtil.GetConfiguration();

      var jwtService = new JwtService(configuration);
      var hashService = new HashService();
      var userRepositoryMock = new UserRepositoryMock();
      var refreshTokenRepositoryMock = new RefreshTokenRepositoryMock();

      var sessionService = new SessionService(
        jwtService,
        hashService,
        userRepositoryMock,
        refreshTokenRepositoryMock
      );

      var user = SessionServiceTestsUtil.CreateUser(
        userRepositoryMock,
        userPassword: "12345678"
      );

      var createSessionViewModel = new CreateSessionViewModel()
      {
        Email = user.Email,
        Password = "IAmTheBestSpidey"
      };

      createSessionViewModel.DoValidation().ThrowBusinessExceptionIfNotValid();

      Assert.Throws<AuthException>(() =>
      {
        sessionService.CreateSession(createSessionViewModel);
      });
    }

    [Fact]
    public void ShouldNotCreateASessionForNonExistingUser()
    {
      var configuration = SessionServiceTestsUtil.GetConfiguration();

      var jwtService = new JwtService(configuration);
      var hashService = new HashService();
      var userRepositoryMock = new UserRepositoryMock();
      var refreshTokenRepositoryMock = new RefreshTokenRepositoryMock();

      var sessionService = new SessionService(
        jwtService,
        hashService,
        userRepositoryMock,
        refreshTokenRepositoryMock
      );

      var userPassword = "12345678";

      var createSessionViewModel = new CreateSessionViewModel()
      {
        Email = "spider.man@email.com",
        Password = userPassword
      };

      createSessionViewModel.DoValidation().ThrowBusinessExceptionIfNotValid();

      Assert.Throws<AuthException>(() =>
      {
        sessionService.CreateSession(createSessionViewModel);
      });
    }
  }
}
