using System.Collections.Generic;
using FinanceOne.Domain.Entities;

namespace FinanceOne.Shared.Repositories
{
  public interface ICategoryRepository
  {
    Category Create(Category category);
    Category Update(Category category);
    void Delete(Category category);
    Category FindById(Category category);
    IList<Category> ListByUser(Category category);
    IList<Category> ListByUserIncludingFinancialMovements(Category category);
  }
}
