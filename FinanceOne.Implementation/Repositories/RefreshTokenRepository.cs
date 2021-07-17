using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FinanceOne.DataAccess.Contexts;
using FinanceOne.Domain.Entities;
using FinanceOne.Shared.Enumerators;
using FinanceOne.Shared.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FinanceOne.Implementation.Repositories
{
  public class RefreshTokenRepository : IRefreshTokenRepository
  {
    private readonly FinanceOneDataContext _financeOneDataContext;

    public RefreshTokenRepository(FinanceOneDataContext financeOneDataContext)
    {
      this._financeOneDataContext = financeOneDataContext;
    }

    public RefreshToken Create(RefreshToken refreshToken)
    {
      var user = this._financeOneDataContext.Users.Find(refreshToken.User);

      refreshToken.User = user;

      this._financeOneDataContext.RefreshTokens.Add(refreshToken);
      this._financeOneDataContext.SaveChanges();

      return refreshToken;
    }

    public void DeleteAllRefreshTokensByUserId(RefreshToken refreshToken)
    {
      var refreshTokens = this._financeOneDataContext.RefreshTokens
        .Where(p =>
          p.UserId == refreshToken.UserId
          && p.Active == IndicatorYesNo.Yes
        );

      foreach (var recoveredRefreshToken in refreshTokens)
        recoveredRefreshToken.Active = IndicatorYesNo.No;

      this._financeOneDataContext.SaveChanges();
    }

    public RefreshToken FindById(RefreshToken refreshToken)
    {
      var foundUser = this._financeOneDataContext.RefreshTokens
        .AsNoTracking()
        .FirstOrDefault(p =>
          p.Id == refreshToken.Id
          && p.Active == refreshToken.Active
        );

      return foundUser;
    }
  }
}
