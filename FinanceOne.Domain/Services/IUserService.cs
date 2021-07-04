using System;
using FinanceOne.Domain.ViewModels.UserViewModels;
using FinanceOne.Domain.Entities;

namespace FinanceOne.Domain.Services
{
  public interface IUserService
  {
    ListUserResponseViewModel CreateUser(CreateUserViewModel createUserViewModel);
    ListUserResponseViewModel UpdateUser(UpdateUserViewModel updateUserViewModel);
    void DeleteUser(Guid UserId);
  }
}
