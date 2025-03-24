using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccruentInventoryControl.Domain.Entities;
using AccruentInventoryControl.Domain.Enums;
using AccruentInventoryControl.Infrastructure.Database.Abstract;
using AccruentInventoryControl.Infrastructure.Exceptions;
using AccruentInventoryControl.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace AccruentInventoryControl.Tests.Infrastructure.Repository
{
    public class WarehouseTransactionRepositoryTests
    {
        private readonly Mock<IDatabaseContext> _mockDbContext;
        private readonly Mock<ILogger<WarehouseTransactionRepository>> _mockLogger;
        private readonly WarehouseTransactionRepository _repository;

        public WarehouseTransactionRepositoryTests()
        {
            _mockDbContext = new Mock<IDatabaseContext>();
            _mockLogger = new Mock<ILogger<WarehouseTransactionRepository>>();
            _repository = new WarehouseTransactionRepository(_mockDbContext.Object, _mockLogger.Object);
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
            var transaction = new WarehouseTransaction { Id = 1, Product = new Product { Code = "P12345" } };
            _mockDbContext.Setup(db => db.WarehouseTransactions.AddAsync(transaction, default)).ThrowsAsync(new Exception("Test exception"));

            // Act & Assert
            await Assert.ThrowsAsync<DbOperationException>(() => _repository.AddAsync(transaction));
        }
    }
}
