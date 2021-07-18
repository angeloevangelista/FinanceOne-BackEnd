using System.Collections.Generic;
using FinanceOne.Domain.ViewModels.UserViewModels;
using FinanceOne.Domain.Entities;

namespace FinanceOne.Shared.Repositories
{
  public interface IUserRepository
  {
    User Create(User user);
    User Update(User user);
    void Delete(User user);
    User FindById(User user);
    User FindByEmail(User user);
  }
}
