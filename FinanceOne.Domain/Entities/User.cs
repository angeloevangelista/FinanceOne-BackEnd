using System;
using FinanceOne.Shared.Entities;

namespace FinanceOne.Domain.Entities
{
  public class User : BaseEntity
  {
    public User() : base()
    {
    }

    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
  }
}
