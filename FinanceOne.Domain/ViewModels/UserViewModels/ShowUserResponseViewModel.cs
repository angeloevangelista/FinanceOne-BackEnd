using System;
using FinanceOne.Domain.Entities;
using FinanceOne.Shared.ViewModels;

namespace FinanceOne.Domain.ViewModels.UserViewModels
{
  public class ShowUserResponseViewModel : BaseViewModel
  {
    public string Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string AvatarUrl { get; set; }

    public static ShowUserResponseViewModel ConvertFromUser(User user) =>
      new ShowUserResponseViewModel()
      {
        Id = user.Id.ToString(),
        CreatedAt = user.CreatedAt,
        UpdatedAt = user.UpdatedAt,
        FirstName = user.FirstName,
        LastName = user.LastName,
        Email = user.Email,
        AvatarUrl = user.AvatarUrl
      };
  }
}
