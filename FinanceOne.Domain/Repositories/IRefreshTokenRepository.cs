using System.Collections.Generic;
using FinanceOne.Domain.ViewModels.UserViewModels;
using FinanceOne.Domain.Entities;

namespace FinanceOne.Shared.Repositories
{
  public interface IRefreshTokenRepository
  {
    RefreshToken Create(RefreshToken refreshToken);
    RefreshToken FindById(RefreshToken refreshToken);
    void DeleteAllRefreshTokensByUserId(RefreshToken refreshToken);
  }
}
