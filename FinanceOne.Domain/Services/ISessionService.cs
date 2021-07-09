using System;
using FinanceOne.Domain.ViewModels.SessionViewModels;

namespace FinanceOne.Domain.Services
{
  public interface ISessionService
  {
    ResultSessionViewModel CreateSession(
      CreateSessionViewModel createSessionViewModel
    );

    ResultSessionViewModel RefreshSession(
      RefreshSessionViewModel refreshSessionViewModel
    );
  }
}
