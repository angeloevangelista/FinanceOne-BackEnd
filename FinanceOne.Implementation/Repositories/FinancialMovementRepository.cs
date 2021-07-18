using System.Collections.Generic;
using System.Linq;
using FinanceOne.DataAccess.Contexts;
using FinanceOne.Domain.Entities;
using FinanceOne.Shared.Enumerators;
using FinanceOne.Shared.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FinanceOne.Implementation.Repositories
{
  public class FinancialMovementRepository : IFinancialMovementRepository
  {
    private readonly FinanceOneDataContext _financeOneDataContext;

    public FinancialMovementRepository(
      FinanceOneDataContext financeOneDataContext
    )
    {
      this._financeOneDataContext = financeOneDataContext;
    }

    public FinancialMovement Create(FinancialMovement financialMovement)
    {
      var category = this._financeOneDataContext.Categories
        .Find(financialMovement.CategoryId);

      financialMovement.Category = category;

      this._financeOneDataContext.FinancialMovements.Add(financialMovement);
      this._financeOneDataContext.SaveChanges();

      return financialMovement;
    }

    public void Delete(FinancialMovement financialMovement)
    {
      var foundFinancialMovement = this.FindById(financialMovement);

      foundFinancialMovement.Active = IndicatorYesNo.No;

      this._financeOneDataContext.Entry<FinancialMovement>(
        foundFinancialMovement
      ).State = EntityState.Modified;

      this._financeOneDataContext.SaveChanges();
    }

    public FinancialMovement FindById(FinancialMovement financialMovement)
    {
      var foundFinancialMovement = this._financeOneDataContext
        .FinancialMovements
        .AsNoTracking()
        .FirstOrDefault(p =>
          p.Id == financialMovement.Id
          && p.Active == financialMovement.Active
        );

      return foundFinancialMovement;
    }

    public IList<FinancialMovement> ListByCategory(
      FinancialMovement financialMovement
    )
    {
      var financialMovements = this._financeOneDataContext.FinancialMovements
        .AsNoTracking()
        .Where(p =>
          p.CategoryId == financialMovement.CategoryId
          && p.Active == financialMovement.Active
        )
        .ToList();

      return financialMovements;
    }

    public FinancialMovement Update(FinancialMovement financialMovement)
    {
      var foundFinancialMovement = this.FindById(financialMovement);

      foundFinancialMovement = financialMovement;

      this._financeOneDataContext
        .Entry<FinancialMovement>(
          foundFinancialMovement
        ).State = EntityState.Modified;

      this._financeOneDataContext.SaveChanges();

      return foundFinancialMovement;
    }
  }
}
