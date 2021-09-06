using System.Collections.Generic;
using System.Linq;
using FinanceOne.DataAccess.Contexts;
using FinanceOne.Domain.Entities;
using FinanceOne.Shared.Enumerators;
using FinanceOne.Shared.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FinanceOne.Implementation.Repositories
{
  public class CapitalAmountRepository : ICapitalAmountRepository
  {
    private readonly FinanceOneDataContext _financeOneDataContext;

    public CapitalAmountRepository(FinanceOneDataContext financeOneDataContext)
    {
      this._financeOneDataContext = financeOneDataContext;
    }

    public CapitalAmount Create(CapitalAmount capitalAmount)
    {
      var user = this._financeOneDataContext.Users.Find(capitalAmount.User);

      capitalAmount.User = user;

      this._financeOneDataContext.CapitalAmounts.Add(capitalAmount);
      this._financeOneDataContext.SaveChanges();

      return capitalAmount;
    }

    public void Delete(CapitalAmount capitalAmount)
    {
      var foundCapitalAmount = this.FindById(capitalAmount);

      foundCapitalAmount.Active = IndicatorYesNo.No;

      this._financeOneDataContext
        .Entry<CapitalAmount>(foundCapitalAmount)
        .State = EntityState.Modified;

      this._financeOneDataContext.SaveChanges();
    }

    public CapitalAmount FindById(CapitalAmount capitalAmount)
    {
      var foundCapitalAmount = this._financeOneDataContext.CapitalAmounts
        .AsNoTracking()
        .FirstOrDefault(p =>
          p.Id == capitalAmount.Id
          && p.Active == capitalAmount.Active
        );

      return foundCapitalAmount;
    }

    public IList<CapitalAmount> ListByUser(CapitalAmount capitalAmount)
    {
      var capitalAmounts = this._financeOneDataContext.CapitalAmounts
        .AsNoTracking()
        .Where(p =>
          p.UserId == capitalAmount.UserId
          && p.Active == capitalAmount.Active
        )
        .ToList();

      return capitalAmounts;
    }

    public CapitalAmount Update(CapitalAmount capitalAmount)
    {
      var foundCapitalAmount = this.FindById(capitalAmount);

      foundCapitalAmount = capitalAmount;

      this._financeOneDataContext
        .Entry<CapitalAmount>(foundCapitalAmount)
        .State = EntityState.Modified;

      this._financeOneDataContext.SaveChanges();

      return foundCapitalAmount;
    }
  }
}
