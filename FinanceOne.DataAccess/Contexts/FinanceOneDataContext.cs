using FinanceOne.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinanceOne.DataAccess.Contexts
{
  public sealed class FinanceOneDataContext : DbContext
  {
    public FinanceOneDataContext(DbContextOptions<FinanceOneDataContext> options)
      : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder
        .ApplyConfigurationsFromAssembly(typeof(FinanceOneDataContext).Assembly);
    }

    public DbSet<User> Users { get; private set; }
    public DbSet<RefreshToken> RefreshTokens { get; private set; }
    public DbSet<Category> Categories { get; private set; }
  }
}
