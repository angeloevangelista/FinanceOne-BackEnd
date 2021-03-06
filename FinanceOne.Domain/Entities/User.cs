using System;
using System.Collections.Generic;
using FinanceOne.Shared.Entities;

namespace FinanceOne.Domain.Entities
{
  public class User : BaseEntity
  {
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string AvatarUrl { get; set; }
  }
}
