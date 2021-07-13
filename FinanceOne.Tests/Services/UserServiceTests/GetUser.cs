using System;
using FinanceOne.Domain.ViewModels.UserViewModels;
using FinanceOne.Implementation.Services;
using FinanceOne.Shared.Exceptions;
using FinanceOne.Tests.Mocks.Providers;
using FinanceOne.Tests.Mocks.Services;
using Xunit;

namespace FinanceOne.Tests.Services.UserServiceTests
{
  public class GetUser
  {
    [Fact]
    public void ShouldGetAnExistingUser()
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

      var userViewModel = userService.GetUser(createdUser.Id.ToString());

      var valuesMatch = true
        && createdUser.Id == userViewModel.Id
        && createdUser.FirstName == userViewModel.FirstName
        && createdUser.LastName == userViewModel.LastName
        && createdUser.Email == userViewModel.Email
      ;

      Assert.True(valuesMatch);
    }

    [Fact]
    public void ShouldNotGetANonExistingUser()
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
        userService.GetUser(Guid.NewGuid().ToString());
      });
    }
  }
}
