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
  public class UpdateUser
  {
    [Fact]
    public void ShouldUpdateAnUser()
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

      var newUserData = new UpdateUserViewModel()
      {
        Id = createdUser.Id.ToString(),
        FirstName = "Spider",
        LastName = "Man",
        Email = "spider.man@email.com",
        Password = "87654321"
      };

      var updatedUser = userService.UpdateUser(newUserData);

      var valuesMatch = true
        && updatedUser.Id.ToString() == newUserData.Id
        && updatedUser.FirstName == newUserData.FirstName
        && updatedUser.LastName == newUserData.LastName
        && updatedUser.Email == newUserData.Email
      ;

      Assert.True(valuesMatch);
    }

    [Fact]
    public void ShouldNotUpdateANonExistingUser()
    {
      var hashService = new HashService();
      var userRepository = new UserRepositoryMock();
      var storageProvider = new StorageProviderMock();

      var userService = new UserService(
        hashService,
        userRepository,
        storageProvider
      );

      var user = new UpdateUserViewModel()
      {
        Id = Guid.NewGuid().ToString(),
        FirstName = "Peter",
        LastName = "Parker",
        Email = "peter.parker@email.com",
        Password = "12345678"
      };

      Assert.Throws<BusinessException>(() =>
      {
        userService.UpdateUser(user);
      });
    }
  }
}
