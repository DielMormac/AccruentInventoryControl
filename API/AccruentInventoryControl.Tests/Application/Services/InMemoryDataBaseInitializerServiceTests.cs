using AccruentInventoryControl.Application.Services;
using AccruentInventoryControl.Application.Services.Interfaces;
using AccruentInventoryControl.Domain.Entities;
using AccruentInventoryControl.Infrastructure.Repository.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;

namespace AccruentInventoryControl.Tests.Application.Services
{
    public class InMemoryDataBaseInitializerServiceTests
    {
        private readonly Mock<ILogger<InMemoryDataBaseInitializerService>> _loggerMock;
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly Mock<IWarehouseTransactionRepository> _warehouseTransactionRepositoryMock;
        private readonly Mock<IWarehouseTransactionService> _warehouseTransactionServiceMock;
        private readonly Mock<IConfiguration> _configMock;
        private readonly InMemoryDataBaseInitializerService _service;

        public InMemoryDataBaseInitializerServiceTests()
        {
            _loggerMock = new Mock<ILogger<InMemoryDataBaseInitializerService>>();
            _productRepositoryMock = new Mock<IProductRepository>();
            _warehouseTransactionRepositoryMock = new Mock<IWarehouseTransactionRepository>();
            _warehouseTransactionServiceMock = new Mock<IWarehouseTransactionService>();
            _configMock = new Mock<IConfiguration>();

            _configMock.Setup(c => c["InMemoryProductCount"]).Returns("10");
            _configMock.Setup(c => c["InMemoryDaysHistory"]).Returns("30");

            _service = new InMemoryDataBaseInitializerService(
                _loggerMock.Object,
                _productRepositoryMock.Object,
                _warehouseTransactionRepositoryMock.Object,
                _warehouseTransactionServiceMock.Object,
                _configMock.Object);
        }

        [Fact]
        public void HasInitilized_ShouldReturnFalseInitially()
        {
            // Act
            var result = _service.HasInitilized();

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task Initialize_ShouldInitializeDatabase()
        {
            // Act
            var result = await _service.Initialize();

            // Assert
            Assert.True(result);
            Assert.True(_service.HasInitilized());
        }

        [Fact]
        public async Task Initialize_ShouldCallAddAsyncOnWarehouseTransactionRepository()
        {
            // Act
            await _service.Initialize();

            // Assert
            _warehouseTransactionRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<WarehouseTransaction>()), Times.AtLeastOnce);
        }

        [Fact]
        public async Task Initialize_ShouldCallCreateWarehouseTransactionOnWarehouseTransactionService()
        {
            // Act
            await _service.Initialize();

            // Assert
            _warehouseTransactionServiceMock.Verify(service => service.CreateWarehouseTransaction(It.IsAny<WarehouseTransaction>()), Times.AtLeastOnce);
        }
    }
}
