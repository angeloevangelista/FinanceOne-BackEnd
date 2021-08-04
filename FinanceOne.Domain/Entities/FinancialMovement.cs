using System;
using FinanceOne.Domain.Enumerators;
using FinanceOne.Shared.Entities;
using FinanceOne.Shared.Enumerators;

namespace FinanceOne.Domain.Entities
{
  public class FinancialMovement : BaseEntity
  {
    public string Name { get; set; }
    public decimal Cost { get; set; }
    public string Description { get; set; }
    public long Amount { get; set; }
    public IndicatorYesNo Paid { get; set; }
    public Guid CategoryId { get; set; }
    public Category Category { get; set; }
    public FinancialMovementType FinancialMovementType { get; set; }
  }
}
