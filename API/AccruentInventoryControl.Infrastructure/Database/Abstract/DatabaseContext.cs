using AccruentInventoryControl.Domain.Entities;
using AccruentInventoryControl.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccruentInventoryControl.Infrastructure.Database.Abstract
{
    public class DatabaseContext : DbContext, IDatabaseContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<WarehouseTransaction> WarehouseTransactions { get; set; }

        /// <inheritdoc />
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("AccruentInventoryControlRepository");
        }

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ProductConfiguration.Configure(modelBuilder.Entity<Product>());
            WarehouseTransactionConfiguration.Configure(modelBuilder.Entity<WarehouseTransaction>());
        }
    }
}
