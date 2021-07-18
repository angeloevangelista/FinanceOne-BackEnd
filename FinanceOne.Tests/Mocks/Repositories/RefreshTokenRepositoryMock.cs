using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FinanceOne.Domain.Entities;
using FinanceOne.Shared.Enumerators;
using FinanceOne.Shared.Repositories;

namespace FinanceOne.Tests.Mocks.Services
{
  public class RefreshTokenRepositoryMock : IRefreshTokenRepository
  {
    private readonly IList<RefreshToken> _refreshTokens;

    public RefreshTokenRepositoryMock()
    {
      this._refreshTokens = new List<RefreshToken>();
    }

    public RefreshToken Create(RefreshToken refreshToken)
    {
      this._refreshTokens.Add(refreshToken);

      return refreshToken;
    }

    public void DeleteRefreshTokensByUser(RefreshToken refreshToken)
    {
      var refreshTokens = this._refreshTokens
        .Where(p =>
          p.UserId == refreshToken.UserId
          && p.Active == IndicatorYesNo.Yes
        );

      foreach (var recoveredRefreshToken in refreshTokens)
        recoveredRefreshToken.Active = IndicatorYesNo.No;
    }

    public RefreshToken FindById(RefreshToken refreshToken)
    {
      var foundUser = this._refreshTokens
        .FirstOrDefault(p =>
          p.Id == refreshToken.Id
          && p.Active == refreshToken.Active
        );

      return foundUser;
    }
  }
}
