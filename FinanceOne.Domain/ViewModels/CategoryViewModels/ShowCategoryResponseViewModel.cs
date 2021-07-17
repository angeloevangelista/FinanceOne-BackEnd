using FinanceOne.Domain.Entities;
using FinanceOne.Shared.ViewModels;

namespace FinanceOne.Domain.ViewModels.CategoryViewModels
{
  public class ShowCategoryResponseViewModel : BaseViewModel
  {
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public static ShowCategoryResponseViewModel ConvertFromCategory(
      Category category
    ) => new ShowCategoryResponseViewModel()
    {
      Id = category.Id.ToString(),
      Name = category.Name,
      Description = category.Description,
    };
  }
}
