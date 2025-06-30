
using Microsoft.EntityFrameworkCore;
using StockApp.Application.DTOs;
using StockApp.Domain.Entities;

namespace StockApp.Infra.Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<UserAuditLog> UserAuditLog { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ErrorLog> ErrorLogs { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<Supplier> Supplier { get; set;}
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }

        public DbSet<Purchase> PurchaseReports { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}
