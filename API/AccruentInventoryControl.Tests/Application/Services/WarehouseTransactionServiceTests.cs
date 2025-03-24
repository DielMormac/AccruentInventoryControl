using AccruentInventoryControl.Application.Services;
using AccruentInventoryControl.Domain.Constants;
using AccruentInventoryControl.Domain.Entities;
using AccruentInventoryControl.Domain.Enums;
using AccruentInventoryControl.Infrastructure.Repository.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;

namespace AccruentInventoryControl.Tests.Application.Services
{
    public class WarehouseTransactionServiceTests
    {
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly Mock<IWarehouseTransactionRepository> _warehouseTransactionRepositoryMock;
        private readonly Mock<ILogger<WarehouseTransactionService>> _loggerMock;
        private readonly WarehouseTransactionService _service;

        public WarehouseTransactionServiceTests()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _warehouseTransactionRepositoryMock = new Mock<IWarehouseTransactionRepository>();
            _loggerMock = new Mock<ILogger<WarehouseTransactionService>>();
            _service = new WarehouseTransactionService(
                _productRepositoryMock.Object,
                _warehouseTransactionRepositoryMock.Object,
                _loggerMock.Object);
        }

        [Fact]
        public async Task GetWarehouseTransaction_ShouldReturnTransaction_WhenTransactionExists()
        {
            // Arrange
            var transactionId = 1;
            var expectedTransaction = new WarehouseTransaction { Id = transactionId };
            _warehouseTransactionRepositoryMock.Setup(repo => repo.GetAsync(transactionId)).ReturnsAsync(expectedTransaction);

            // Act
            var result = await _service.GetWarehouseTransaction(transactionId);

            // Assert
            Assert.Equal(expectedTransaction, result);
        }

        [Fact]
        public async Task GetAllWarehouseTransactions_ShouldReturnAllTransactions()
        {
            // Arrange
            var expectedTransactions = new List<WarehouseTransaction>
            {
                new WarehouseTransaction { Id = 1 },
                new WarehouseTransaction { Id = 2 }
            };
            _warehouseTransactionRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(expectedTransactions);

            // Act
            var result = await _service.GetAllWarehouseTransactions();

            // Assert
            Assert.Equal(expectedTransactions, result);
        }

        [Fact]
        public async Task CreateWarehouseTransaction_ShouldThrowException_WhenProductNotFound()
        {
            // Arrange
            var transaction = new WarehouseTransaction { Product = new Product { Code = "P12345" } };
            _productRepositoryMock.Setup(repo => repo.GetByCodeAsync(transaction.Product.Code)).ReturnsAsync((Product)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _service.CreateWarehouseTransaction(transaction));
            Assert.Equal(ErrorKeys.ProductNotFound, exception.Message);
        }

        [Fact]
        public async Task CreateWarehouseTransaction_ShouldCreateTransaction_WhenProductExists()
        {
            // Arrange
            var product = new Product { Code = "P12345" };
            var transaction = new WarehouseTransaction { Product = product, Quantity = 10, Type = WarehouseTransactionType.Inbound };
            _productRepositoryMock.Setup(repo => repo.GetByCodeAsync(product.Code)).ReturnsAsync(product);
            _warehouseTransactionRepositoryMock.Setup(repo => repo.GetLastCompletedWarehouseTransaction(product.Code)).ReturnsAsync((WarehouseTransaction)null);
            _warehouseTransactionRepositoryMock.Setup(repo => repo.AddAsync(transaction)).ReturnsAsync(transaction);

            // Act
            var result = await _service.CreateWarehouseTransaction(transaction);

            // Assert
            Assert.Equal(transaction, result);
            Assert.Equal(0, transaction.PreviousQuantity);
            Assert.Equal(10, transaction.TotalQuantity);
            Assert.Equal(WarehouseTransactionStatus.Completed, transaction.Status);
        }

        [Fact]
        public async Task GetAllWarehouseTransactionsByDateAndProductCode_ShouldReturnTransactions_WhenProductCodeIsEmpty()
        {
            // Arrange
            var date = DateTime.Now;
            var expectedTransactions = new List<WarehouseTransaction>
            {
                new WarehouseTransaction { Id = 1 },
                new WarehouseTransaction { Id = 2 }
            };
            _warehouseTransactionRepositoryMock.Setup(repo => repo.GetAllWarehouseTransactionByDate(date)).ReturnsAsync(expectedTransactions);

            // Act
            var result = await _service.GetAllWarehouseTransactionsByDateAndProductCode(date, string.Empty);

            // Assert
            Assert.Equal(expectedTransactions, result);
        }

        [Fact]
        public async Task GetAllWarehouseTransactionsByDateAndProductCode_ShouldReturnTransactions_WhenProductCodeIsNotEmpty()
        {
            // Arrange
            var date = DateTime.Now;
            var productCode = "P12345";
            var expectedTransactions = new List<WarehouseTransaction>
            {
                new WarehouseTransaction { Id = 1 },
                new WarehouseTransaction { Id = 2 }
            };
            _warehouseTransactionRepositoryMock.Setup(repo => repo.GetAllWarehouseTransactionByDateAndProductCode(date, productCode)).ReturnsAsync(expectedTransactions);

            // Act
            var result = await _service.GetAllWarehouseTransactionsByDateAndProductCode(date, productCode);

            // Assert
            Assert.Equal(expectedTransactions, result);
        }

        [Fact]
        public void Dispose_ShouldCallDisposeOnRepositories()
        {
            // Act
            _service.Dispose();

            // Assert
            _productRepositoryMock.Verify(repo => repo.Dispose(), Times.Once);
            _warehouseTransactionRepositoryMock.Verify(repo => repo.Dispose(), Times.Once);
        }
    }
}
