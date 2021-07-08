using FinanceOne.Shared.ViewModels;

namespace FinanceOne.Domain.ViewModels.SessionViewModels
{
  public class ResultSessionViewModel : BaseViewModel
  {
    public string Token { get; set; }
    public string RefreshToken { get; set; }
  }
}
