using FinanceOne.Shared.Contracts.Services;
using FinanceOne.Shared.Controllers;
using FinanceOne.Shared.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceOne.WebApi.Controllers
{
  public class ServerController : CustomControllerBase
  {
    public ServerController(IJwtService jwtService) : base(jwtService)
    {
    }

    [Route("{*url}")]
    [ApiExplorerSettings(IgnoreApi = true)]
    [AllowAnonymous]
    public ActionResult CatchAll()
    {
      return NotFound(new ApiResponse<object>()
      {
        Success = false,
        Data = "👽"
      });
    }
  }
}
