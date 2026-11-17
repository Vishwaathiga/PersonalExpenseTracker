
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PersonalExpenseTracker.Api.Models;

namespace PersonalExpenseTracker.Api.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Expense> Expenses { get; set; }

        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);
            builder.Entity<Category>().HasData(
   new Category { Id = 1, Name = "Food", IsDefault = true },
   new Category { Id = 2, Name = "Transportation", IsDefault = true },
   new Category { Id = 3, Name = "Shopping", IsDefault = true },
   new Category { Id = 4, Name = "Healthcare", IsDefault = true },
   new Category { Id = 5, Name = "Education", IsDefault = true },
   new Category { Id = 6, Name = "Bills & Utilities", IsDefault = true },
   new Category { Id = 7, Name = "Entertainment", IsDefault = true },
   new Category { Id = 8, Name = "Others", IsDefault = true }
);
            // Expense amount precision
            builder.Entity<Expense>()
                .Property(e => e.Amount)
                .HasPrecision(18, 2);

            // Expense - User relationship
            builder.Entity<Expense>()
                .HasOne(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Expense - Category relationship
            builder.Entity<Expense>()
              .HasOne(e => e.Category)
               .WithMany()
             .HasForeignKey(e => e.CategoryId)
             .OnDelete(DeleteBehavior.NoAction);

            // Category - User relationship
            builder.Entity<Category>()
                .HasOne(c => c.User)
                .WithMany()
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
