using FinanceOne.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinanceOne.DataAccess.Contexts
{
  public class UserDataContext : DbContext
  {
    public UserDataContext(DbContextOptions<UserDataContext> options)
      : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder
        .ApplyConfigurationsFromAssembly(typeof(UserDataContext).Assembly);
    }

    public DbSet<User> Users { get; private set; }
  }
}
