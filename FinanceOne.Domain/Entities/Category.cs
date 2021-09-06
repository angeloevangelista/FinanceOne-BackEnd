using System;
using System.Collections.Generic;
using FinanceOne.Shared.Entities;

namespace FinanceOne.Domain.Entities
{
  public class Category : BaseEntity
  {
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    public IList<FinancialMovement> FinancialMovements { get; set; }
  }
}
