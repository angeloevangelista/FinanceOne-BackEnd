using Microsoft.AspNetCore.Mvc;
using FinanceOne.Domain.Services;
using FinanceOne.Shared.Controllers;
using FinanceOne.Shared.Contracts.Services;
using FinanceOne.Domain.ViewModels.FinancialMovementViewModels;
using FinanceOne.Domain.Entities;
using System.Collections;
using System.Collections.Generic;

namespace FinanceOne.WebApi.Controllers
{
  [ApiController]
  public class FinancialMovementController : CustomControllerBase
  {
    private readonly IFinancialMovementService _financialMovementService;

    public FinancialMovementController(
      IJwtService jwtService,
      IFinancialMovementService financialMovementService
    ) : base(jwtService)
    {
      this._financialMovementService = financialMovementService;
    }

    [HttpPost]
    [Route("/v1/categories/{categoryId}/financial-movements")]
    public ActionResult<ShowFinancialMovementResponseViewModel> CreateFinancialMovement(
      [FromRoute]
      string categoryId,

      [FromBody]
      CreateFinancialMovementViewModel createFinancialMovementViewModel
    )
    {
      createFinancialMovementViewModel.CategoryId = categoryId;
      createFinancialMovementViewModel.UserId =
        GetTokenPayload<SessionPayload>().Id;

      ValidateViewModel(createFinancialMovementViewModel);

      var createdFinancialMovement = this._financialMovementService
        .CreateFinancialMovement(
          createFinancialMovementViewModel
        );

      return Ok(createdFinancialMovement);
    }

    [HttpPut]
    [Route("/v1/categories/{categoryId}/financial-movements/{financialMovementId}")]
    public ActionResult<ShowFinancialMovementResponseViewModel> UpdateFinancialMovement(
      [FromRoute]
      string categoryId,

      [FromRoute]
      string financialMovementId,

      [FromBody]
      UpdateFinancialMovementViewModel updateFinancialMovementViewModel
    )
    {
      updateFinancialMovementViewModel.Id = financialMovementId;
      updateFinancialMovementViewModel.CategoryId = categoryId;
      updateFinancialMovementViewModel.UserId =
        GetTokenPayload<SessionPayload>().Id;

      ValidateViewModel(updateFinancialMovementViewModel);

      var updatedFinancialMovement = this._financialMovementService
        .UpdateFinancialMovement(updateFinancialMovementViewModel);

      return Ok(updatedFinancialMovement);
    }

    [HttpGet]
    [Route("/v1/categories/{categoryId}/financial-movements")]
    public ActionResult<IList<ShowFinancialMovementResponseViewModel>> ListFinancialMovements(
      [FromRoute]
      string categoryId
    )
    {
      var listFinancialMovementsViewModel = new ListFinancialMovementsViewModel()
      {
        CategoryId = categoryId,
        UserId = GetTokenPayload<SessionPayload>().Id
      };

      ValidateViewModel(listFinancialMovementsViewModel);

      var financialMovements = this._financialMovementService
        .ListFinancialMovements(
          listFinancialMovementsViewModel
        );

      return Ok(financialMovements);
    }

    [HttpGet]
    [Route("/v1/categories/{categoryId}/financial-movements/{financialMovementId}")]
    public ActionResult<ShowFinancialMovementResponseViewModel> GetFinancialMovement(
      [FromRoute] string categoryId, [FromRoute] string financialMovementId
    )
    {
      var getFinancialMovementViewModel = new GetFinancialMovementViewModel()
      {
        CategoryId = categoryId,
        FinancialMovementId = financialMovementId,
        UserId = GetTokenPayload<SessionPayload>().Id
      };

      ValidateViewModel(getFinancialMovementViewModel);

      var financialMovements = this._financialMovementService
        .GetFinancialMovement(
          getFinancialMovementViewModel
        );

      return Ok(financialMovements);
    }

    [HttpDelete]
    [Route("/v1/categories/{categoryId}/financial-movements/{financialMovementId}")]
    public ActionResult<ShowFinancialMovementResponseViewModel> DeleteFinancialMovement(
      [FromRoute] string categoryId, [FromRoute] string financialMovementId
    )
    {
      var deleteFinancialMovement = new DeleteFinancialMovementViewModel()
      {
        CategoryId = categoryId,
        FinancialMovementId = financialMovementId,
        UserId = GetTokenPayload<SessionPayload>().Id
      };

      ValidateViewModel(deleteFinancialMovement);

      this._financialMovementService.DeleteFinancialMovement(
        deleteFinancialMovement
      );

      return NoContent();
    }
  }
}
