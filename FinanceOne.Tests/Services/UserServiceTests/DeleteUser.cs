using System;
using FinanceOne.Domain.Entities;
using FinanceOne.Domain.ViewModels.UserViewModels;
using FinanceOne.Implementation.Services;
using FinanceOne.Shared.Exceptions;
using FinanceOne.Tests.Mocks.Providers;
using FinanceOne.Tests.Mocks.Services;
using Xunit;

namespace FinanceOne.Tests.Services.UserServiceTests
{
  public class DeleteUser
  {
    [Fact]
    public void ShouldDeleteAnUser()
    {
      var hashService = new HashService();
      var userRepository = new UserRepositoryMock();
      var storageProvider = new StorageProviderMock();

      var userService = new UserService(
        hashService,
        userRepository,
        storageProvider
      );

      var userData = new CreateUserViewModel()
      {
        FirstName = "Peter",
        LastName = "Parker",
        Email = "peter.parker@email.com",
        Password = "12345678"
      };

      userData.DoValidation().ThrowBusinessExceptionIfNotValid();

      var createdUser = userService.CreateUser(userData);

      userService.DeleteUser(createdUser.Id.ToString());

      var foundUser = userRepository.FindByEmail(new User()
      {
        Email = "peter.parker@email.com",
      });

      Assert.Null(foundUser);
    }

    [Fact]
    public void ShouldNotDeleteANonExistingUser()
    {
      var hashService = new HashService();
      var userRepository = new UserRepositoryMock();
      var storageProvider = new StorageProviderMock();

      var userService = new UserService(
        hashService,
        userRepository,
        storageProvider
      );

      Assert.Throws<BusinessException>(() =>
      {
        userService.DeleteUser(Guid.NewGuid().ToString());
      });
    }
  }
}
