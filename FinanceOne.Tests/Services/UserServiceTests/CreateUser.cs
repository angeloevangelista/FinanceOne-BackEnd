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
  public class CreateUser
  {
    [Fact]
    public void ShouldCreateAnUser()
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

      var foundUser = userRepository.FindByEmail(new User()
      {
        Email = "peter.parker@email.com",
      });

      Assert.NotNull(foundUser);
    }

    [Fact]
    public void ShouldNotCreateAnUserWithAnEmailAlreadyInUse()
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

      Assert.Throws<BusinessException>(() =>
      {
        var userWithSameEmail = userService.CreateUser(userData);
      });
    }

    [Fact]
    public void ShouldGenerateAnUUIDForCreatedUser()
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

      var foundUser = userRepository.FindByEmail(new User()
      {
        Email = "peter.parker@email.com",
      });

      var hasGeneratedUUID = Guid.TryParse(
        foundUser.Id.ToString(),
        out Guid result
      );

      Assert.True(hasGeneratedUUID);
    }

    [Fact]
    public void ShouldHasUserPassword()
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

      var foundUser = userRepository.FindByEmail(new User()
      {
        Email = "peter.parker@email.com",
      });

      Assert.NotEqual(foundUser.Password, userData.Password);
    }
  }
}
