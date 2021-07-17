using Microsoft.AspNetCore.Mvc;
using FinanceOne.Domain.Services;
using FinanceOne.Shared.Controllers;
using FinanceOne.Shared.Contracts.Services;
using FinanceOne.Domain.ViewModels.CategoryViewModels;
using FinanceOne.Domain.Entities;
using System.Collections;
using System.Collections.Generic;

namespace FinanceOne.WebApi.Controllers
{
  [ApiController]
  public class CategoryController : CustomControllerBase
  {
    private readonly ICategoryService _categoryService;

    public CategoryController(
      IJwtService jwtService,
      ICategoryService categoryService
    ) : base(jwtService)
    {
      this._categoryService = categoryService;
    }

    [HttpPost]
    [Route("/v1/categories")]
    public ActionResult<ShowCategoryResponseViewModel> CreateCategory(
      [FromBody] CreateCategoryViewModel createCategoryViewModel
    )
    {
      createCategoryViewModel.UserId = GetTokenPayload<SessionPayload>().Id;

      ValidateViewModel(createCategoryViewModel);

      var createdCategory = this._categoryService.CreateCategory(
        createCategoryViewModel
      );

      return Ok(createdCategory);
    }

    [HttpPut]
    [Route("/v1/categories")]
    public ActionResult<ShowCategoryResponseViewModel> UpdateCategory(
      [FromBody] UpdateCategoryViewModel updateCategoryViewModel
    )
    {
      updateCategoryViewModel.UserId = GetTokenPayload<SessionPayload>().Id;

      ValidateViewModel(updateCategoryViewModel);

      var createdCategory = this._categoryService.UpdateCategory(
        updateCategoryViewModel
      );

      return Ok(createdCategory);
    }

    [HttpGet]
    [Route("/v1/categories")]
    public ActionResult<IList<ShowCategoryResponseViewModel>> ListCategories()
    {
      var listCategoriesViewModel = new ListCategoriesViewModel()
      {
        UserId = GetTokenPayload<SessionPayload>().Id
      };

      ValidateViewModel(listCategoriesViewModel);

      var categories = this._categoryService.ListCategories(
        listCategoriesViewModel
      );

      return Ok(categories);
    }

    [HttpDelete]
    [Route("/v1/categories")]
    public ActionResult DeleteCategory(
      [FromBody] DeleteCategoryViewModel deleteCategoryViewModel
    )
    {
      deleteCategoryViewModel.UserId = GetTokenPayload<SessionPayload>().Id;

      ValidateViewModel(deleteCategoryViewModel);

      this._categoryService.DeleteCategory(deleteCategoryViewModel);

      return NoContent();
    }

    [HttpGet]
    [Route("/v1/categories/{categoryId}")]
    public ActionResult GetCategory(string categoryId)
    {
      var getCategoryViewModel = new GetCategoryViewModel()
      {
        Id = categoryId,
        UserId = GetTokenPayload<SessionPayload>().Id
      };

      ValidateViewModel(getCategoryViewModel);

      var category = this._categoryService.GetCategory(getCategoryViewModel);

      return Ok(category);
    }
  }
}
