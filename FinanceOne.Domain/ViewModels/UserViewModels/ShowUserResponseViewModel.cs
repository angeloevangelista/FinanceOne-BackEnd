using System;
using FinanceOne.Domain.Entities;
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

    public static ShowUserResponseViewModel ConvertFromUser(User user) =>
     new ShowUserResponseViewModel()
     {
       Id = user.Id,
       FirstName = user.FirstName,
       LastName = user.LastName,
       Email = user.Email,
       AvatarUrl = user.AvatarUrl
     };
  }
}
