using System;
using FinanceOne.Domain.Enumerators;
using FinanceOne.Shared.Entities;

namespace FinanceOne.Domain.Entities
{
  public class FinancialMovement : BaseEntity
  {
    public string Name { get; set; }
    public decimal Cost { get; set; }
    public Guid CategoryId { get; set; }
    public Category Category { get; set; }
    public FinancialMovementType FinancialMovementType { get; set; }
  }
}
