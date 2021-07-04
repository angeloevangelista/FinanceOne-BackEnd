using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FinanceOne.DataAccess.Contexts;
using FinanceOne.Domain.Entities;
using FinanceOne.Shared.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FinanceOne.Implementation.Repositories
{
  public class UserRepository : IUserRepository
  {
    private readonly UserDataContext _userDataContext;

    public UserRepository(UserDataContext userDataContext)
    {
      this._userDataContext = userDataContext;
    }

    public User Create(User user)
    {
      this._userDataContext.Users.Add(user);
      this._userDataContext.SaveChanges();

      return user;
    }

    public void Delete(User user)
    {
      var foundUser = this._userDataContext.Users
        .AsNoTracking()
        .First(p => p.Id == user.Id);

      this._userDataContext.Remove(foundUser);
      this._userDataContext.SaveChanges();
    }

    public User FindByEmail(string email)
    {
      var foundUser = this._userDataContext.Users
        .AsNoTracking()
        .FirstOrDefault(p => p.Email.ToLower() == email.ToLower());

      return foundUser;
    }

    public User FindById(User user)
    {
      var foundUser = this._userDataContext.Users
        .AsNoTracking()
        .FirstOrDefault(p => p.Id == user.Id);

      return foundUser;
    }

    public User Update(User user)
    {
      var foundUser = this.FindById(user);

      foundUser = user;

      this._userDataContext.Entry<User>(foundUser).State = EntityState.Modified;

      this._userDataContext.SaveChanges();

      return foundUser;
    }
  }
}
