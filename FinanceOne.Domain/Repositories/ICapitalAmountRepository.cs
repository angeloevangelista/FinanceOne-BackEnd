using System.Collections.Generic;
using FinanceOne.Domain.Entities;

namespace FinanceOne.Shared.Repositories
{
  public interface ICapitalAmountRepository
  {
    CapitalAmount Create(CapitalAmount capitalAmount);
    CapitalAmount Update(CapitalAmount capitalAmount);
    void Delete(CapitalAmount capitalAmount);
    CapitalAmount FindById(CapitalAmount capitalAmount);
    IList<CapitalAmount> ListByUser(CapitalAmount capitalAmount);
  }
}
