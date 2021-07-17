using System;
using FinanceOne.Shared.Enumerators;

namespace FinanceOne.Shared.Entities
{
  public class BaseEntity
  {
    public BaseEntity()
    {
      Active = IndicatorYesNo.Yes;
      CreatedAt = DateTime.UtcNow;
      UpdatedAt = DateTime.UtcNow;
    }

    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public IndicatorYesNo Active { get; set; }
  }
}
