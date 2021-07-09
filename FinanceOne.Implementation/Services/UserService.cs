using System;
using FinanceOne.Domain.ViewModels.UserViewModels;
using FinanceOne.Domain.Entities;
using FinanceOne.Domain.Services;
using FinanceOne.Shared.Exceptions;
using FinanceOne.Shared.Repositories;

namespace FinanceOne.Implementation.Services
{
  public class UserService : IUserService
  {
    private readonly IHashService _hashService;
    private readonly IUserRepository _userRepository;

    public UserService(
      IHashService hashService,
      IUserRepository userRepository
    )
    {
      this._hashService = hashService;
      this._userRepository = userRepository;
    }

    public ListUserResponseViewModel CreateUser(
      CreateUserViewModel createUserViewModel
    )
    {
      var emailAlreadyUsed = this._userRepository.FindByEmail(
        new User()
        {
          Email = createUserViewModel.Email
        }
      ) != null;

      if (emailAlreadyUsed)
        throw new BusinessException("Email is already in use.");

      var hashedPassword = this._hashService.Hash(
        createUserViewModel.Password
      );

      var user = new User()
      {
        Id = Guid.NewGuid(),
        FirstName = createUserViewModel.FirstName,
        LastName = createUserViewModel.LastName,
        Email = createUserViewModel.Email,
        Password = hashedPassword,
      };

      user = this._userRepository.Create(user);

      return new ListUserResponseViewModel()
      {
        Id = user.Id,
        FirstName = user.FirstName,
        LastName = user.LastName,
        Email = user.Email
      };
    }

    public void DeleteUser(Guid UserId)
    {
      var foundUser = this._userRepository.FindById(new User()
      {
        Id = UserId
      });

      if (foundUser == null)
        throw new BusinessException("User not found.");

      this._userRepository.Delete(foundUser);
    }

    public ListUserResponseViewModel UpdateUser(
      UpdateUserViewModel updateUserViewModel
    )
    {
      var foundUser = this._userRepository.FindById(new User()
      {
        Id = Guid.Parse(updateUserViewModel.Id)
      });

      if (foundUser == null)
        throw new BusinessException("User not found.");

      foundUser.FirstName = updateUserViewModel.FirstName;
      foundUser.LastName = updateUserViewModel.LastName;

      var updateEmail = foundUser.Email != updateUserViewModel.Email;

      if (updateEmail)
      {
        var emailAlreadyUsed = this._userRepository.FindByEmail(
          new User()
          {
            Email = updateUserViewModel.Email
          }
        ) != null;

        if (emailAlreadyUsed)
          throw new BusinessException("Email is already in use.");

        foundUser.Email = updateUserViewModel.Email;
      }

      var updatePassword = updateUserViewModel.Password != null;

      if (updatePassword)
      {
        var hashedPassword = this._hashService.Hash(
          updateUserViewModel.Password
        );

        foundUser.Password = updateUserViewModel.Password;
      }

      foundUser.UpdatedAt = DateTime.UtcNow;

      foundUser = this._userRepository.Update(foundUser);

      return new ListUserResponseViewModel()
      {
        Id = foundUser.Id,
        FirstName = foundUser.FirstName,
        LastName = foundUser.LastName,
        Email = foundUser.Email
      };
    }
  }
}
