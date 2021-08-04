using System;
using FinanceOne.Domain.Entities;
using FinanceOne.Domain.Enumerators;
using FinanceOne.Shared.ViewModels;

namespace FinanceOne.Domain.ViewModels.FinancialMovementViewModels
{
  public class ShowFinancialMovementResponseViewModel : BaseViewModel
  {
    public string Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string CategoryId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public long Amount { get; set; }
    public string Paid { get; set; }
    public decimal Cost { get; set; }
    public string FinancialMovementType { get; set; }

    public static ShowFinancialMovementResponseViewModel ConvertFromEntity(
      FinancialMovement financialMovement
    ) => new ShowFinancialMovementResponseViewModel()
    {
      Id = financialMovement.Id.ToString(),
      CreatedAt = financialMovement.CreatedAt,
      UpdatedAt = financialMovement.UpdatedAt,
      CategoryId = financialMovement.CategoryId.ToString(),
      Name = financialMovement.Name,
      Cost = financialMovement.Cost,
      Description = financialMovement.Description,
      Amount = financialMovement.Amount,
      Paid = (
        (char)financialMovement.Paid
      ).ToString(),
      FinancialMovementType = (
        (char)financialMovement.FinancialMovementType
      ).ToString()
    };
  }
}
