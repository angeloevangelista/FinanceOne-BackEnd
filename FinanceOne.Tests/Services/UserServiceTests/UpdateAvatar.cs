using System;
using System.IO;
using FinanceOne.Domain.Entities;
using FinanceOne.Domain.ViewModels.UserViewModels;
using FinanceOne.Implementation.Services;
using FinanceOne.Shared.Exceptions;
using FinanceOne.Tests.Mocks.Providers;
using FinanceOne.Tests.Mocks.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Moq;
using Xunit;

namespace FinanceOne.Tests.Services.UserServiceTests
{
  public class UpdateAvatar
  {
    [Fact]
    public void ShouldUpdateUserAvatar()
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

      var createdUser = userService.CreateUser(userData);

      var lastUserAvatar = userRepository.FindByEmail(new User()
      {
        Email = "peter.parker@email.com",
      }).AvatarUrl;

      IFormFile file = null;

      using (var memoryStream = new MemoryStream())
      using (var streamWriter = new StreamWriter(memoryStream))
      {
        streamWriter.Write("File Content");
        streamWriter.Flush();
        memoryStream.Position = 0;

        var fileMock = new Mock<IFormFile>();

        fileMock.Setup(p => p.OpenReadStream()).Returns(memoryStream);
        fileMock.Setup(p => p.FileName).Returns("mock_file.png");
        fileMock.Setup(p => p.Length).Returns(memoryStream.Length);

        file = fileMock.Object;
      }

      var updateAvatarViewModel = new UpdateAvatarViewModel()
      {
        UserId = createdUser.Id.ToString(),
        File = file
      };

      updateAvatarViewModel.DoValidation().ThrowBusinessExceptionIfNotValid();

      var updatedUser = userService.UpdateAvatar(updateAvatarViewModel);

      Assert.NotEqual(updatedUser.AvatarUrl, lastUserAvatar);
    }

    [Fact]
    public void ShouldNotUpdateUserAvatarForANonExistingUser()
    {
      var hashService = new HashService();
      var userRepository = new UserRepositoryMock();
      var storageProvider = new StorageProviderMock();

      var userService = new UserService(
        hashService,
        userRepository,
        storageProvider
      );

      var user = new User()
      {
        Id = Guid.NewGuid(),
        FirstName = "Peter",
        LastName = "Parker",
        Email = "peter.parker@email.com",
        Password = "12345678"
      };

      IFormFile file = null;

      using (var memoryStream = new MemoryStream())
      using (var streamWriter = new StreamWriter(memoryStream))
      {
        streamWriter.Write("File Content");
        streamWriter.Flush();
        memoryStream.Position = 0;

        var fileMock = new Mock<IFormFile>();

        fileMock.Setup(p => p.OpenReadStream()).Returns(memoryStream);
        fileMock.Setup(p => p.FileName).Returns("mock_file.png");
        fileMock.Setup(p => p.Length).Returns(memoryStream.Length);

        file = fileMock.Object;
      }

      var updateAvatarViewModel = new UpdateAvatarViewModel()
      {
        UserId = Guid.NewGuid().ToString(),
        File = file
      };

      updateAvatarViewModel.DoValidation().ThrowBusinessExceptionIfNotValid();

      Assert.Throws<BusinessException>(() =>
      {
        userService.UpdateAvatar(updateAvatarViewModel);
      });
    }
  }
}
