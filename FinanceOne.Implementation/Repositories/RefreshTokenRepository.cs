using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FinanceOne.DataAccess.Contexts;
using FinanceOne.Domain.Entities;
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
      this._financeOneDataContext.RefreshTokens.Add(refreshToken);
      this._financeOneDataContext.SaveChanges();

      return refreshToken;
    }
  }
}
