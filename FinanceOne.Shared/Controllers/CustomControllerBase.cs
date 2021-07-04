using System.Linq;
using System.Threading.Tasks;
using FinanceOne.Shared.Exceptions;
using FinanceOne.Shared.Contracts.Services;
using FinanceOne.Shared.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FinanceOne.Shared.Controllers
{
  [Authorize]
  public abstract class CustomControllerBase : ControllerBase
  {
    private readonly IJwtService _jwtService;

    public CustomControllerBase(IJwtService jwtService)
    {
      this._jwtService = jwtService;
    }

    protected void ValidateViewModel(BaseViewModel viewModel)
    {
      viewModel.DoValidation();

      if (viewModel.IsInvalid)
        throw new BusinessException(viewModel.GetBrokenRules().ToArray());
    }

    protected string GetAuthorizationToken()
    {
      var authorization = Request.Headers["Authorization"].ToString();
      var token = authorization.Split(' ').Last();

      return token;
    }

    protected T GetTokenPayload<T>()
    {
      var token = this.GetAuthorizationToken();

      return this._jwtService.DecodeToken<T>(token);
    }
  }
}
