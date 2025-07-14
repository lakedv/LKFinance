using BudgetService.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BudgetService.API.Data
{
    public class BudgetDbContext : DbContext
    {
        public BudgetDbContext(DbContextOptions<BudgetDbContext> options) : base(options)
        {
        }

        public DbSet<Budget> Budgets { get; set; }
        public DbSet<Income> Incomes { get; set; }
        public DbSet<BudgetCategory> BudgetCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Budget>()
                .HasIndex(b => new { b.UserId, b.Month, b.Year })
                .IsUnique();

            modelBuilder.Entity<Budget>()
                .HasMany(b => b.Categories)
                .WithOne(c => c.Budget)
                .HasForeignKey(c => c.BudgetId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Budget>()
                .HasMany(b => b.Incomes)
                .WithOne(i => i.Budget)
                .HasForeignKey(i => i.BudgetId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
