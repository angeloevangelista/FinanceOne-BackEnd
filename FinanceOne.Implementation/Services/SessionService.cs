using FinanceOne.Domain.Entities;
using FinanceOne.Domain.Services;
using FinanceOne.Shared.Exceptions;
using FinanceOne.Shared.Repositories;
using FinanceOne.Domain.ViewModels.SessionViewModels;
using FinanceOne.Shared.Contracts.Services;
using System;
using System.Transactions;

namespace FinanceOne.Implementation.Services
{
  public class SessionService : ISessionService
  {
    private readonly IHashService _hashService;
    private readonly IUserRepository _userRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IJwtService _jwtService;

    public SessionService(
      IJwtService jwtService,
      IHashService hashService,
      IUserRepository userRepository,
      IRefreshTokenRepository refreshTokenRepository
    )
    {
      this._jwtService = jwtService;
      this._hashService = hashService;
      this._userRepository = userRepository;
      this._refreshTokenRepository = refreshTokenRepository;
    }

    public ResultSessionViewModel CreateSession(
      CreateSessionViewModel createSessionViewModel
    )
    {
      var foundUser = this._userRepository.FindByEmail(
        new User()
        {
          Email = createSessionViewModel.Email
        }
      );

      if (foundUser == null)
        throw new AuthException("Invalid credentials.");

      var passwordsMatch = this._hashService.Verify(
        createSessionViewModel.Password,
        foundUser.Password
      );

      if (!passwordsMatch)
        throw new AuthException("Invalid credentials.");

      var sessionPayload = new SessionPayload()
      {
        Id = foundUser.Id.ToString(),
        Name = $"{foundUser.FirstName} {foundUser.LastName}",
      };

      var token = this._jwtService.GenerateToken(sessionPayload, "Id");

      var refreshToken = new RefreshToken()
      {
        Id = Guid.NewGuid(),
        ExpiresAt = DateTime.UtcNow.AddDays(7),
        UserId = foundUser.Id
      };

      using (var transactionScope = new TransactionScope())
      {
        this._refreshTokenRepository.DeleteRefreshTokensByUser(
          new RefreshToken()
          {
            UserId = foundUser.Id
          }
        );

        this._refreshTokenRepository.Create(refreshToken);

        var session = new ResultSessionViewModel()
        {
          Token = token,
          RefreshToken = refreshToken.Id.ToString(),
        };

        transactionScope.Complete();

        return session;
      }
    }

    public ResultSessionViewModel RefreshSession(
      RefreshSessionViewModel refreshSessionViewModel
    )
    {
      var authException = new AuthException("Invalid authorization keys.");

      var tokenWasCreatedByApp = this._jwtService.CheckTokenIsTrustworthy(
        refreshSessionViewModel.Token
      );

      if (!tokenWasCreatedByApp)
        throw authException;

      var recoveredRefreshToken = this._refreshTokenRepository.FindById(
        new RefreshToken()
        {
          Id = Guid.Parse(refreshSessionViewModel.RefreshToken)
        }
      );

      if (recoveredRefreshToken == null)
        throw authException;

      if (recoveredRefreshToken.ExpiresAt <= DateTime.UtcNow)
        throw authException;

      var foundUser = this._userRepository.FindById(
        new User() { Id = recoveredRefreshToken.UserId }
      );

      if (foundUser == null)
        throw authException;

      var tokenPayload = this._jwtService.DecodeToken<SessionPayload>(
        refreshSessionViewModel.Token,
        false
      );

      var refreshAndTokenUsersMatchs =
        tokenPayload.Id == foundUser.Id.ToString();

      if (!refreshAndTokenUsersMatchs)
        throw authException;

      var sessionPayload = new SessionPayload()
      {
        Id = foundUser.Id.ToString(),
        Name = $"{foundUser.FirstName} {foundUser.LastName}",
      };

      var token = this._jwtService.GenerateToken(sessionPayload, "Id");

      var refreshToken = new RefreshToken()
      {
        Id = Guid.NewGuid(),
        ExpiresAt = DateTime.UtcNow.AddDays(7),
        UserId = foundUser.Id
      };

      using (var transactionScope = new TransactionScope())
      {
        this._refreshTokenRepository.DeleteRefreshTokensByUser(
          new RefreshToken()
          {
            UserId = foundUser.Id
          }
        );

        refreshToken = this._refreshTokenRepository.Create(refreshToken);

        transactionScope.Complete();
      }

      var session = new ResultSessionViewModel()
      {
        Token = token,
        RefreshToken = refreshToken.Id.ToString(),
      };

      return session;
    }
  }
}
