using System;
using FinanceOne.Shared.Enumerators;

namespace FinanceOne.Shared.Entities
{
  public abstract class BaseEntity
  {
    public BaseEntity()
    {
      Active = IndicatorYesNo.Yes;
      CreatedAt = DateTime.UtcNow;
      UpdatedAt = DateTime.UtcNow;
    }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public IndicatorYesNo Active { get; set; }
  }
}
