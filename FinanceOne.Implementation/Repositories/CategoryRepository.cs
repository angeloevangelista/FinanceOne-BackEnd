using System.Collections.Generic;
using System.Linq;
using FinanceOne.DataAccess.Contexts;
using FinanceOne.Domain.Entities;
using FinanceOne.Shared.Enumerators;
using FinanceOne.Shared.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FinanceOne.Implementation.Repositories
{
  public class CategoryRepository : ICategoryRepository
  {
    private readonly FinanceOneDataContext _financeOneDataContext;

    public CategoryRepository(FinanceOneDataContext financeOneDataContext)
    {
      this._financeOneDataContext = financeOneDataContext;
    }

    public Category Create(Category category)
    {
      var user = this._financeOneDataContext.Users.Find(category.User);

      category.User = user;

      this._financeOneDataContext.Categories.Add(category);
      this._financeOneDataContext.SaveChanges();

      return category;
    }

    public void Delete(Category category)
    {
      var foundCategory = this.FindById(category);

      foundCategory.Active = IndicatorYesNo.No;

      this._financeOneDataContext.Entry<Category>(foundCategory).State =
        EntityState.Modified;

      this._financeOneDataContext.SaveChanges();
    }

    public Category FindById(Category category)
    {
      var foundCategory = this._financeOneDataContext.Categories
        .AsNoTracking()
        .FirstOrDefault(p =>
          p.Id == category.Id
          && p.Active == category.Active
        );

      return foundCategory;
    }

    public Category Update(Category category)
    {
      var foundCategory = this.FindById(category);

      foundCategory = category;

      this._financeOneDataContext.Entry<Category>(foundCategory).State =
        EntityState.Modified;

      this._financeOneDataContext.SaveChanges();

      return foundCategory;
    }

    public IList<Category> ListByUser(Category category)
    {
      var categories = this._financeOneDataContext.Categories
        .AsNoTracking()
        .Where(p =>
          p.UserId == category.UserId
          && p.Active == category.Active
        )
        .ToList();

      return categories;
    }

    public IList<Category> ListByUserIncludingFinancialMovements(Category category)
    {
      var categories = this._financeOneDataContext.Categories
        .AsNoTracking()
        .Where(p =>
          p.UserId == category.UserId
          && p.Active == category.Active
        )
        .Include(p => p.FinancialMovements)
        .ToList();

      return categories;
    }
  }
}
