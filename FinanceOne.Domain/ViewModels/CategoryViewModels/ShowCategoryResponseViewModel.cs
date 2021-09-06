using System;
using System.Collections.Generic;
using System.Linq;
using FinanceOne.Domain.Entities;
using FinanceOne.Domain.ViewModels.FinancialMovementViewModels;
using FinanceOne.Shared.ViewModels;

namespace FinanceOne.Domain.ViewModels.CategoryViewModels
{
  public class ShowCategoryResponseViewModel : BaseViewModel
  {
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public IEnumerable<ShowFinancialMovementResponseViewModel> FinancialMovements { get; set; }

    public static ShowCategoryResponseViewModel ConvertFromEntity(
      Category category
    ) => new ShowCategoryResponseViewModel()
    {
      Id = category.Id.ToString(),
      CreatedAt = category.CreatedAt,
      UpdatedAt = category.UpdatedAt,
      Name = category.Name,
      Description = category.Description,
      FinancialMovements = category.FinancialMovements?.Select(p =>
        ShowFinancialMovementResponseViewModel.ConvertFromEntity(p)
      )
    };
  }
}
