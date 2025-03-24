using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccruentInventoryControl.Domain.Entities;
using AccruentInventoryControl.Infrastructure.Database.Abstract;
using AccruentInventoryControl.Infrastructure.Exceptions;
using AccruentInventoryControl.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace AccruentInventoryControl.Tests.Infrastructure.Repository
{
    public class ProductRepositoryTests
    {
        private readonly Mock<IDatabaseContext> _mockDbContext;
        private readonly Mock<ILogger<ProductRepository>> _mockLogger;
        private readonly ProductRepository _productRepository;

        public ProductRepositoryTests()
        {
            _mockDbContext = new Mock<IDatabaseContext>();
            _mockLogger = new Mock<ILogger<ProductRepository>>();
            _productRepository = new ProductRepository(_mockDbContext.Object, _mockLogger.Object);
        }

        private Mock<DbSet<T>> CreateMockDbSet<T>(IEnumerable<T> elements) where T : class
        {
            var queryable = elements.AsQueryable();
            var dbSet = new Mock<DbSet<T>>();
            dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(queryable.GetEnumerator());
            return dbSet;
        }

        [Fact]
        public async Task AddAsync_ShouldThrowDbOperationException_WhenExceptionOccurs()
        {
            // Arrange
            var product = new Product { Id = 1, Code = "P12345", Name = "Test Product" };
            _mockDbContext.Setup(db => db.Products.AddAsync(product, default)).ThrowsAsync(new Exception("Test exception"));

            // Act & Assert
            await Assert.ThrowsAsync<DbOperationException>(() => _productRepository.AddAsync(product));
        }

        [Fact]
        public async Task DeleteAsync_ShouldThrowNotSupportedDbOperationException()
        {
            // Act & Assert
            await Assert.ThrowsAsync<NotSupportedDbOperationException>(() => _productRepository.DeleteAsync(1));
        }

        [Fact]
        public async Task UpdateAsync_ShouldThrowNotSupportedDbOperationException()
        {
            // Arrange
            var product = new Product { Id = 1, Code = "P12345", Name = "Test Product" };

            // Act & Assert
            await Assert.ThrowsAsync<NotSupportedDbOperationException>(() => _productRepository.UpdateAsync(product));
        }
    }
}
