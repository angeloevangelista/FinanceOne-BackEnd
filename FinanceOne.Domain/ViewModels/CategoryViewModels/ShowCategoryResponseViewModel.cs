using System;
using FinanceOne.Domain.Entities;
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

    public static ShowCategoryResponseViewModel ConvertFromEntity(
      Category category
    ) => new ShowCategoryResponseViewModel()
    {
      Id = category.Id.ToString(),
      CreatedAt = category.CreatedAt,
      UpdatedAt = category.UpdatedAt,
      Name = category.Name,
      Description = category.Description,
    };
  }
}
