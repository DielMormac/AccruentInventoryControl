using System.Linq;
using AccruentInventoryControl.Domain.Entities;
using AccruentInventoryControl.Infrastructure.Database.Abstract;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace AccruentInventoryControl.Tests.Infrastructure.Database.Abstract
{
    public class DatabaseContextTests
    {
        private class TestDatabaseContext : DatabaseContext
        {
            public TestDatabaseContext(DbContextOptions<DatabaseContext> options) { }

            public new void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);
            }
        }

        [Fact]
        public void DatabaseContext_ShouldSetDbSetProperties()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            // Act
            using (var context = new TestDatabaseContext(options))
            {
                // Assert
                Assert.NotNull(context.Products);
                Assert.NotNull(context.WarehouseTransactions);
            }
        }

        [Fact]
        public void DatabaseContext_ShouldConfigureModelBuilder()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            // Act
            using (var context = new TestDatabaseContext(options))
            {
                var modelBuilder = new ModelBuilder(new Microsoft.EntityFrameworkCore.Metadata.Conventions.ConventionSet());
                context.OnModelCreating(modelBuilder);

                // Assert
                var productEntity = modelBuilder.Model.FindEntityType(typeof(Product));
                var warehouseTransactionEntity = modelBuilder.Model.FindEntityType(typeof(WarehouseTransaction));

                Assert.NotNull(productEntity);
                Assert.NotNull(warehouseTransactionEntity);
            }
        }

        [Fact]
        public void DatabaseContext_ShouldUseInMemoryDatabase()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            // Act
            using (var context = new TestDatabaseContext(options))
            {
                context.Products.Add(new Product { Id = 1, Code = "P12345", Name = "Test Product" });
                context.SaveChanges();

                // Assert
                Assert.Equal(1, context.Products.Count());
                Assert.Equal("P12345", context.Products.Single().Code);
            }
        }
    }
}
