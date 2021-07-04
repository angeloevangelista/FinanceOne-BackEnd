using System;
using FinanceOne.Domain.ViewModels.SessionViewModels;

namespace FinanceOne.Domain.Services
{
  public interface ISessionService
  {
    string CreateSession(CreateSessionViewModel createSessionViewModel);
  }
}
