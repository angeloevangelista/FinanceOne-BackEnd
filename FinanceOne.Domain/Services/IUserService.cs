using System;
using FinanceOne.Domain.ViewModels.UserViewModels;

namespace FinanceOne.Domain.Services
{
  public interface IUserService
  {
    ShowUserResponseViewModel CreateUser(
      CreateUserViewModel createUserViewModel
    );

    ShowUserResponseViewModel UpdateUser(
      UpdateUserViewModel updateUserViewModel
    );

    // ShowUserResponseViewModel UpdateAvatar(
    //   UpdateAvatarViewModel updateAvatarViewModel
    // );

    void DeleteUser(Guid UserId);
  }
}
