using System;
using FinanceOne.Domain.Entities;
using FinanceOne.Domain.Enumerators;
using FinanceOne.Shared.Enumerators;
using FinanceOne.Shared.Util.DataTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinanceOne.DataAccess.Configurations
{
  public class FinancialMovementConfiguration : IEntityTypeConfiguration<FinancialMovement>
  {
    public void Configure(EntityTypeBuilder<FinancialMovement> builder)
    {
      builder.ToTable("financial_movements");
      builder.HasKey(pre => pre.Id).HasName("pk_financial_movements");

      builder.Property(pre => pre.Id)
        .HasColumnName("id");

      builder.Property(pre => pre.CategoryId)
        .HasColumnName("category_id")
        .IsRequired();

      builder.Property(pre => pre.Name)
        .HasColumnName("name")
        .HasColumnType("VARCHAR(50)")
        .IsRequired();

      builder.Property(pre => pre.Cost)
        .HasColumnName("cost")
        .IsRequired();

      builder.Property(pre => pre.Description)
        .HasColumnName("description")
        .IsRequired(false);

      builder.Property(pre => pre.Description)
        .HasColumnName("amount")
        .IsRequired();

      builder.Property(pre => pre.CreatedAt)
        .HasColumnName("created_at")
        .IsRequired();

      builder.Property(pre => pre.UpdatedAt)
        .HasColumnName("updated_at")
        .IsRequired();

      builder.Property(pre => pre.Active)
        .HasColumnName("active")
        .HasColumnType("char")
        .HasDefaultValue(IndicatorYesNo.Yes)
        .HasConversion(
          enumValue => ((char)enumValue).ToString(),
          charValue => UtilEnum.Parse<IndicatorYesNo>(charValue)
        )
        .IsRequired();

      builder.Property(pre => pre.FinancialMovementType)
        .HasColumnName("financial_movement_type")
        .HasColumnType("char")
        .HasDefaultValue(FinancialMovementType.Expense)
        .HasConversion(
          enumValue => ((char)enumValue).ToString(),
          charValue => UtilEnum.Parse<FinancialMovementType>(charValue)
        )
        .IsRequired();


      builder.Property(pre => pre.Paid)
        .HasColumnName("paid")
        .HasColumnType("char")
        .HasDefaultValue(IndicatorYesNo.No)
        .HasConversion(
          enumValue => ((char)enumValue).ToString(),
          charValue => UtilEnum.Parse<IndicatorYesNo>(charValue)
        )
        .IsRequired();

      builder
        .HasOne<Category>(pre => pre.Category)
        .WithMany()
        .HasForeignKey(pre => pre.CategoryId)
        .HasConstraintName("fk_category")
        .OnDelete(DeleteBehavior.Cascade);
    }
  }
}
