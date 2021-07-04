using Microsoft.AspNetCore.Mvc;
using FinanceOne.Domain.Services;
using FinanceOne.Domain.ViewModels.SessionViewModels;
using FinanceOne.Shared.Controllers;
using FinanceOne.Shared.ViewModels;
using Microsoft.AspNetCore.Authorization;
using FinanceOne.Shared.Contracts.Services;

namespace FinanceOne.WebApi.Controllers
{
  [ApiController]
  public class SessionController : CustomControllerBase
  {
    private readonly ISessionService _sessionService;

    public SessionController(
      ISessionService sessionService,
      IJwtService jwtService
    ) : base(jwtService)
    {
      this._sessionService = sessionService;
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("/v1/sessions")]
    public ActionResult<ApiResponse<string>> CreateUser(
      [FromBody] CreateSessionViewModel createSessionViewModel
    )
    {
      ValidateViewModel(createSessionViewModel);

      var sessionToken = this._sessionService.CreateSession(
        createSessionViewModel
      );

      return Ok(new ApiResponse<string>(sessionToken));
    }
  }
}
