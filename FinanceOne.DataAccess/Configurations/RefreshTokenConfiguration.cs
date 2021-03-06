using System;
using FinanceOne.Domain.Entities;
using FinanceOne.Shared.Enumerators;
using FinanceOne.Shared.Util.DataTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinanceOne.DataAccess.Configurations
{
  public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
  {
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
      builder.ToTable("refresh_tokens");
      builder.HasKey(pre => pre.Id).HasName("pk_refresh_tokens");

      builder.Property(pre => pre.Id)
        .HasColumnName("id");

      builder.Property(pre => pre.UserId)
        .HasColumnName("user_id")
        .IsRequired();

      builder.Property(pre => pre.ExpiresAt)
        .HasColumnName("expires_at")
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

      builder
        .HasOne<User>(pre => pre.User)
        .WithMany()
        .HasForeignKey(pre => pre.UserId)
        .HasConstraintName("fk_user")
        .OnDelete(DeleteBehavior.Cascade);
    }
  }
}
