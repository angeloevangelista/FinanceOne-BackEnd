using Microsoft.AspNetCore.Mvc;
using FinanceOne.Domain.Services;
using FinanceOne.Shared.Controllers;
using FinanceOne.Shared.Contracts.Services;
using FinanceOne.Domain.ViewModels.CapitalAmountViewModels;
using FinanceOne.Domain.Entities;
using System.Collections.Generic;

namespace FinanceOne.WebApi.Controllers
{
  [ApiController]
  public class CapitalAmountController : CustomControllerBase
  {
    private readonly ICapitalAmountService _capitalAmountService;

    public CapitalAmountController(
      IJwtService jwtService,
      ICapitalAmountService capitalAmountService
    ) : base(jwtService)
    {
      this._capitalAmountService = capitalAmountService;
    }

    [HttpPost]
    [Route("/v1/capital-amounts")]
    public ActionResult<ShowCapitalAmountResponseViewModel> CreateCapitalAmount(
      [FromBody] CreateCapitalAmountViewModel createCapitalAmountViewModel
    )
    {
      createCapitalAmountViewModel.UserId = GetTokenPayload<SessionPayload>().Id;

      ValidateViewModel(createCapitalAmountViewModel);

      var createdCapitalAmount = this._capitalAmountService.CreateCapitalAmount(
        createCapitalAmountViewModel
      );

      return Ok(createdCapitalAmount);
    }

    [HttpGet]
    [Route("/v1/capital-amounts")]
    public ActionResult<IList<ShowCapitalAmountResponseViewModel>> ListCapitalAmounts()
    {
      var listCapitalAmountsViewModel = new ListCapitalAmountsViewModel()
      {
        UserId = GetTokenPayload<SessionPayload>().Id
      };

      ValidateViewModel(listCapitalAmountsViewModel);

      var capitalAmounts = this._capitalAmountService.ListCapitalAmounts(
        listCapitalAmountsViewModel
      );

      return Ok(capitalAmounts);
    }

    [HttpPut]
    [Route("/v1/capital-amounts/{capitalAmountId}")]
    public ActionResult<ShowCapitalAmountResponseViewModel> UpdateCapitalAmount(
      [FromRoute] string capitalAmountId,
      [FromBody] UpdateCapitalAmountViewModel updateCapitalAmountViewModel
    )
    {
      updateCapitalAmountViewModel.Id = capitalAmountId;
      updateCapitalAmountViewModel.UserId = GetTokenPayload<SessionPayload>().Id;

      ValidateViewModel(updateCapitalAmountViewModel);

      var updatedCapitalAmount = this._capitalAmountService.UpdateCapitalAmount(
        updateCapitalAmountViewModel
      );

      return Ok(updatedCapitalAmount);
    }

    [HttpDelete]
    [Route("/v1/capital-amounts/{capitalAmountId}")]
    public ActionResult DeleteCapitalAmount(
      [FromRoute] string capitalAmountId
    )
    {
      var deleteCapitalAmountViewModel = new DeleteCapitalAmountViewModel()
      {
        Id = capitalAmountId,
        UserId = GetTokenPayload<SessionPayload>().Id
      };

      ValidateViewModel(deleteCapitalAmountViewModel);

      this._capitalAmountService.DeleteCapitalAmount(deleteCapitalAmountViewModel);

      return NoContent();
    }
  }
}
