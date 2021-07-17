using System;
using FinanceOne.Domain.Entities;
using FinanceOne.Shared.Enumerators;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinanceOne.DataAccess.Configurations
{
  public class CategoryConfiguration : IEntityTypeConfiguration<Category>
  {
    public void Configure(EntityTypeBuilder<Category> builder)
    {
      builder.ToTable("categories");
      builder.HasKey(pre => pre.Id).HasName("pk_categories");

      builder.Property(pre => pre.Id)
        .HasColumnName("id");

      builder.Property(pre => pre.UserId)
        .HasColumnName("user_id")
        .IsRequired();

      builder.Property(pre => pre.Name)
        .HasColumnName("name")
        .HasColumnType("VARCHAR(50)")
        .IsRequired();

      builder.Property(pre => pre.Description)
        .HasColumnName("description")
        .HasColumnType("VARCHAR(150)")
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
          charValue => (IndicatorYesNo)Enum.Parse(
            typeof(IndicatorYesNo),
            Enum.GetName(typeof(IndicatorYesNo), Convert.ToChar(charValue))
          )
        )
        .IsRequired();

      builder
        .HasOne<User>(pre => pre.User)
        .WithMany()
        .HasForeignKey(pre => pre.UserId)
        .HasConstraintName("fk_user")
        .OnDelete(DeleteBehavior.Cascade);
    }
  }
}
