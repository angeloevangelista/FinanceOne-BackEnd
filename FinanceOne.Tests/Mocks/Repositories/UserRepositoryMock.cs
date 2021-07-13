using System.Collections.Generic;
using System.Linq;
using FinanceOne.Domain.Entities;
using FinanceOne.Shared.Enumerators;
using FinanceOne.Shared.Repositories;

namespace FinanceOne.Tests.Mocks.Services
{
  public class UserRepositoryMock : IUserRepository
  {
    private readonly IList<User> _users;

    public UserRepositoryMock()
    {
      this._users = new List<User>();
    }

    public User Create(User user)
    {
      this._users.Add(user);

      return user;
    }

    public void Delete(User user)
    {
      var foundUser = this.FindById(user);

      foundUser.Active = IndicatorYesNo.No;
    }

    public User FindByEmail(User user)
    {
      var foundUser = this._users
        .FirstOrDefault(p =>
          p.Email.ToLower() == user.Email.ToLower()
          && p.Active == user.Active
        );

      return foundUser;
    }

    public User FindById(User user)
    {
      var foundUser = this._users
        .FirstOrDefault(p =>
          p.Id == user.Id
          && p.Active == user.Active
        );

      return foundUser;
    }

    public User Update(User user)
    {
      var foundUser = this.FindById(user);

      foundUser = user;

      return foundUser;
    }
  }
}
