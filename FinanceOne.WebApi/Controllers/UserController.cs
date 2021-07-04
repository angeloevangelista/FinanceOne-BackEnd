using Microsoft.AspNetCore.Mvc;
using FinanceOne.Domain.Services;
using FinanceOne.Domain.ViewModels.UserViewModels;
using FinanceOne.Shared.Controllers;
using FinanceOne.Shared.ViewModels;
using Microsoft.AspNetCore.Authorization;
using FinanceOne.Shared.Contracts.Services;
using FinanceOne.Domain.Entities;

namespace FinanceOne.WebApi.Controllers
{
  [ApiController]
  public class UserController : CustomControllerBase
  {

    private readonly IUserService _userService;

    public UserController(IUserService userService, IJwtService jwtService)
      : base(jwtService)
    {
      this._userService = userService;
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("/v1/users")]
    public ActionResult<ApiResponse<ListUserResponseViewModel>> CreateUser(
      [FromBody] CreateUserViewModel createUserViewModel
    )
    {
      ValidateViewModel(createUserViewModel);

      var createdUser = this._userService.CreateUser(createUserViewModel);

      return Ok(new ApiResponse<ListUserResponseViewModel>(createdUser));
    }

    [HttpPut]
    [Route("/v1/users")]
    public ActionResult<ApiResponse<ListUserResponseViewModel>> UpdateUser(
      [FromBody] UpdateUserViewModel updateUserViewModel
    )
    {
      updateUserViewModel.Id = this.GetTokenPayload<SessionPayload>().Id;

      ValidateViewModel(updateUserViewModel);

      var updatedUser = this._userService.UpdateUser(updateUserViewModel);

      return Ok(new ApiResponse<ListUserResponseViewModel>(updatedUser));
    }
  }
}
