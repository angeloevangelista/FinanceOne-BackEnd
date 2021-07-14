using System.Threading;
using FinanceOne.Domain.ViewModels.SessionViewModels;
using FinanceOne.Implementation.Services;
using FinanceOne.Shared.Exceptions;
using FinanceOne.Shared.Services;
using FinanceOne.Tests.Mocks.Services;
using Xunit;

namespace FinanceOne.Tests.Services.SessionServiceTests
{
  public class RefreshSession
  {
    [Fact]
    public void ShouldRefreshTheSessionToken()
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

      var refreshSessionViewModel = new RefreshSessionViewModel()
      {
        Token = sessionPayload.Token,
        RefreshToken = sessionPayload.RefreshToken,
      };

      // Forcing 1s delay to generate a different expiration date
      Thread.Sleep(1000);

      var refreshedSession = sessionService.RefreshSession(
        refreshSessionViewModel
      );

      Assert.NotEqual(sessionPayload.Token, refreshedSession.Token);

      Assert.NotEqual(
        sessionPayload.RefreshToken,
        refreshedSession.RefreshToken
      );
    }

    [Fact]
    public void ShouldNotRefreshTheSessionTokenForAnInvalidJwt()
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

      sessionPayload.Token = "INVALID_JWT_TOKEN";

      var refreshSessionViewModel = new RefreshSessionViewModel()
      {
        Token = sessionPayload.Token,
        RefreshToken = sessionPayload.RefreshToken,
      };

      // Forcing 1s delay to generate a different expiration date
      Thread.Sleep(1000);

      Assert.Throws<AuthException>(() =>
      {
        sessionService.RefreshSession(
          refreshSessionViewModel
        );
      });
    }

    [Fact]
    public void ShouldNotRefreshTheSessionTokenForOtherUser()
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

      var userA = SessionServiceTestsUtil.CreateUser(
        userRepositoryMock,
        userPassword
      );

      var createSessionViewModel = new CreateSessionViewModel()
      {
        Email = userA.Email,
        Password = userPassword
      };

      createSessionViewModel.DoValidation().ThrowBusinessExceptionIfNotValid();

      var sessionPayloadA = sessionService.CreateSession(createSessionViewModel);

      var userB = SessionServiceTestsUtil.CreateUser(
        userRepositoryMock,
        userPassword
      );

      createSessionViewModel = new CreateSessionViewModel()
      {
        Email = userB.Email,
        Password = userPassword
      };

      createSessionViewModel.DoValidation().ThrowBusinessExceptionIfNotValid();

      var sessionPayloadB = sessionService.CreateSession(createSessionViewModel);

      var refreshSessionViewModel = new RefreshSessionViewModel()
      {
        Token = sessionPayloadA.Token,
        RefreshToken = sessionPayloadB.RefreshToken,
      };

      // Forcing 1s delay to generate a different expiration date
      Thread.Sleep(1000);

      Assert.Throws<AuthException>(() =>
      {
        sessionService.RefreshSession(
          refreshSessionViewModel
        );
      });
    }
  }
}
