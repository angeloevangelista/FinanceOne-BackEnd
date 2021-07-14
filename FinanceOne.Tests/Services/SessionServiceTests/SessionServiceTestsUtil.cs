using System.IO;
using FinanceOne.Domain.Entities;
using FinanceOne.Domain.ViewModels.UserViewModels;
using FinanceOne.Implementation.Services;
using FinanceOne.Shared.Repositories;
using FinanceOne.Tests.Mocks.Providers;
using Microsoft.Extensions.Configuration;

namespace FinanceOne.Tests.Services.SessionServiceTests
{
  public static class SessionServiceTestsUtil
  {
    public static IConfiguration GetConfiguration()
    {
      var rootPath = Directory.GetParent(
        Directory.GetParent(
          Directory.GetParent(
            Directory.GetCurrentDirectory()
          ).FullName
        ).FullName
      ).FullName;

      var configurationPath = Path.Combine(rootPath, "appsettings.Tests.json");

      var configurationBuilder = new ConfigurationBuilder();
      configurationBuilder.AddJsonFile(configurationPath);

      return configurationBuilder.Build();
    }

    public static ShowUserResponseViewModel CreateUser(
      IUserRepository userRepository,
      string userPassword
    )
    {
      var emailPrefix = "user";

      var hashService = new HashService();
      var storageProvider = new StorageProviderMock();

      var userService = new UserService(
        hashService,
        userRepository,
        storageProvider
      );

      var emailAlreadyUsed = false;

      do
      {
        emailPrefix += "0";

        emailAlreadyUsed = userRepository
          .FindByEmail(new User()
          {
            Email = $"{emailPrefix}@email.com"
          }
        ) != null;
      }
      while (emailAlreadyUsed);

      var userData = new CreateUserViewModel()
      {
        FirstName = "Peter",
        LastName = "Parker",
        Email = $"{emailPrefix}@email.com",
        Password = userPassword
      };

      userData.DoValidation().ThrowBusinessExceptionIfNotValid();

      var createdUser = userService.CreateUser(userData);

      return createdUser;
    }
  }
}
