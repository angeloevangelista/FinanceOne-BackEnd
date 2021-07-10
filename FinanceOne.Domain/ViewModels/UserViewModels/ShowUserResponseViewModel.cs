using System;
using FinanceOne.Shared.ViewModels;

namespace FinanceOne.Domain.ViewModels.UserViewModels
{
  public class ShowUserResponseViewModel : BaseViewModel
  {
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string AvatarUrl { get; set; }
  }
}
