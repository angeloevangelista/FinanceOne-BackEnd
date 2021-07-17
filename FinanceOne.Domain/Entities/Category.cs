using System;
using FinanceOne.Shared.Entities;

namespace FinanceOne.Domain.Entities
{
  public class Category : BaseEntity
  {
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
  }
}
