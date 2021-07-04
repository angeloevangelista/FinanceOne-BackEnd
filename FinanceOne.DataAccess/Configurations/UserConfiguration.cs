using FinanceOne.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinanceOne.DataAccess.Configurations
{
  public class UserConfiguration : IEntityTypeConfiguration<User>
  {
    public void Configure(EntityTypeBuilder<User> builder)
    {
      builder.ToTable("users");
      builder.HasKey(pre => pre.Id).HasName("pk_users");

      builder.Property(pre => pre.Id)
        .HasColumnName("id");

      builder.Property(pre => pre.FirstName)
        .HasColumnName("first_name")
        .HasColumnType("VARCHAR(50)")
        .IsRequired();

      builder.Property(pre => pre.LastName)
        .HasColumnName("last_name")
        .HasColumnType("VARCHAR(50)")
        .IsRequired();

      builder.Property(pre => pre.Email)
        .HasColumnName("email")
        .HasColumnType("VARCHAR(50)")
        .IsRequired();

      builder.Property(pre => pre.Password)
        .HasColumnName("password")
        .HasColumnType("VARCHAR(255)")
        .IsRequired();

      builder.Property(pre => pre.CreatedAt)
        .HasColumnName("created_at")
        .IsRequired();

      builder.Property(pre => pre.UpdatedAt)
        .HasColumnName("updated_at")
        .IsRequired();
    }
  }
}
