using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FinanceOne.DataAccess.Contexts;
using FinanceOne.Domain.Entities;
using FinanceOne.Shared.Enumerators;
using FinanceOne.Shared.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FinanceOne.Implementation.Repositories
{
  public class UserRepository : IUserRepository
  {
    private readonly FinanceOneDataContext _financeOneDataContext;

    public UserRepository(FinanceOneDataContext financeOneDataContext)
    {
      this._financeOneDataContext = financeOneDataContext;
    }

    public User Create(User user)
    {
      this._financeOneDataContext.Users.Add(user);
      this._financeOneDataContext.SaveChanges();

      return user;
    }

    public void Delete(User user)
    {
      var foundUser = this.FindById(user);

      foundUser.Active = IndicatorYesNo.No;

      this._financeOneDataContext.Entry<User>(foundUser).State =
        EntityState.Modified;

      this._financeOneDataContext.SaveChanges();
    }

    public User FindByEmail(User user)
    {
      var foundUser = this._financeOneDataContext.Users
        .AsNoTracking()
        .FirstOrDefault(p =>
          p.Email.ToLower() == user.Email.ToLower()
          && p.Active == user.Active
        );

      return foundUser;
    }

    public User FindById(User user)
    {
      var foundUser = this._financeOneDataContext.Users
        .AsNoTracking()
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

      this._financeOneDataContext.Entry<User>(foundUser).State =
        EntityState.Modified;

      this._financeOneDataContext.SaveChanges();

      return foundUser;
    }
  }
}
