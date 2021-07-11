using System;
using FinanceOne.Domain.ViewModels.UserViewModels;
using FinanceOne.Domain.Entities;
using FinanceOne.Domain.Services;
using FinanceOne.Shared.Exceptions;
using FinanceOne.Shared.Repositories;
using FinanceOne.Domain.Providers;
using FinanceOne.Domain.Enumerators;

namespace FinanceOne.Implementation.Services
{
  public class UserService : IUserService
  {
    private readonly IHashService _hashService;
    private readonly IUserRepository _userRepository;
    private readonly IStorageProvider _storageProvider;

    public UserService(
      IHashService hashService,
      IUserRepository userRepository,
      IStorageProvider storageProvider
    )
    {
      this._hashService = hashService;
      this._userRepository = userRepository;
      this._storageProvider = storageProvider;
    }

    public ShowUserResponseViewModel CreateUser(
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

      return ShowUserResponseViewModel.ConvertFromUser(user);
    }

    public void DeleteUser(string UserId)
    {
      var foundUser = this._userRepository.FindById(new User()
      {
        Id = Guid.Parse(UserId)
      });

      if (foundUser == null)
        throw new BusinessException("User not found.");

      this._userRepository.Delete(foundUser);
    }

    public ShowUserResponseViewModel GetUser(
      string userId
    )
    {
      var foundUser = this._userRepository.FindById(new User()
      {
        Id = Guid.Parse(userId)
      });

      if (foundUser == null)
        throw new BusinessException("User not found.");

      return ShowUserResponseViewModel.ConvertFromUser(foundUser);
    }

    public ShowUserResponseViewModel UpdateAvatar(
      UpdateAvatarViewModel updateAvatarViewModel
    )
    {
      var foundUser = this._userRepository.FindById(new User()
      {
        Id = Guid.Parse(updateAvatarViewModel.UserId)
      });

      if (foundUser == null)
        throw new BusinessException("User not found.");

      var avatarUrl = this._storageProvider.UploadFile(
        updateAvatarViewModel.File,
        FileType.ProfileAvatar
      );

      foundUser.AvatarUrl = avatarUrl;

      this._userRepository.Update(foundUser);

      return ShowUserResponseViewModel.ConvertFromUser(foundUser);
    }

    public ShowUserResponseViewModel UpdateUser(
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

      return ShowUserResponseViewModel.ConvertFromUser(foundUser);
    }
  }
}
