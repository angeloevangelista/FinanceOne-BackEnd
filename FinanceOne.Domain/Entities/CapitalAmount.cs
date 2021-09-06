using System;
using FinanceOne.Shared.Entities;

namespace FinanceOne.Domain.Entities
{
  public class CapitalAmount: BaseEntity
  {
    public DateTime ReferenceDate { get; set; }
    public decimal Amount { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
  }
}
