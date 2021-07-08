using System;
using FinanceOne.Shared.Entities;

namespace FinanceOne.Domain.Entities
{
  public class RefreshToken: BaseEntity
  {
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    public DateTime ExpiresAt { get; set; }
  }
}
