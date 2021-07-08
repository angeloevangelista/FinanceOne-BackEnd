using System;

namespace FinanceOne.Shared.Entities
{
  public abstract class BaseEntity
  {
    public BaseEntity()
    {
      CreatedAt = DateTime.UtcNow;
      UpdatedAt = DateTime.UtcNow;
    }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
  }
}
