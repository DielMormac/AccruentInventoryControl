using AccruentInventoryControl.Application.Services;
using AccruentInventoryControl.Application.Services.Interfaces;
using AccruentInventoryControl.Domain.Entities;
using Microsoft.Extensions.Logging;
using Moq;

namespace AccruentInventoryControl.Tests.Application.Services
{
    public class WarehouseTransactionReportServiceTests
    {
        private readonly Mock<IWarehouseTransactionService> _warehouseTransactionServiceMock;
        private readonly Mock<ILogger<WarehouseTransactionService>> _loggerMock;
        private readonly WarehouseTransactionReportService _service;

        public WarehouseTransactionReportServiceTests()
        {
            _warehouseTransactionServiceMock = new Mock<IWarehouseTransactionService>();
            _loggerMock = new Mock<ILogger<WarehouseTransactionService>>();
            _service = new WarehouseTransactionReportService(_warehouseTransactionServiceMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task GetWarehouseTransactionReportByDateAndProduct_ShouldReturnReport_WhenTransactionsExist()
        {
            // Arrange
            var date = DateTime.Now;
            var productCode = "P12345";
            var transactions = new List<WarehouseTransaction>
            {
                new WarehouseTransaction { Id = 1, Product = new Product { Code = productCode }, TotalQuantity = 10 },
                new WarehouseTransaction { Id = 2, Product = new Product { Code = productCode }, TotalQuantity = 20 }
            };
            _warehouseTransactionServiceMock.Setup(service => service.GetAllWarehouseTransactionsByDateAndProductCode(date, productCode))
                                            .ReturnsAsync(transactions);

            // Act
            var result = await _service.GetWarehouseTransactionReportByDateAndProduct(date, productCode);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(date, result.Date);
            Assert.Equal(productCode, result.ProductCode);
            Assert.Equal(20, result.Balance);
            Assert.Equal(transactions.Count, result.WarehouseTransactions.Count);
        }

        [Fact]
        public async Task GetWarehouseTransactionReportByDateAndProduct_ShouldReturnNull_WhenNoTransactionsExist()
        {
            // Arrange
            var date = DateTime.Now;
            var productCode = "P12345";
            _warehouseTransactionServiceMock.Setup(service => service.GetAllWarehouseTransactionsByDateAndProductCode(date, productCode))
                                            .ReturnsAsync(new List<WarehouseTransaction>());

            // Act
            var result = await _service.GetWarehouseTransactionReportByDateAndProduct(date, productCode);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetWarehouseTransactionReportByDateAndProduct_ShouldLogError_WhenExceptionIsThrown()
        {
            // Arrange
            var date = DateTime.Now;
            var productCode = "P12345";
            var exceptionMessage = "Test exception";
            _warehouseTransactionServiceMock.Setup(service => service.GetAllWarehouseTransactionsByDateAndProductCode(date, productCode))
                                            .ThrowsAsync(new Exception(exceptionMessage));

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _service.GetWarehouseTransactionReportByDateAndProduct(date, productCode));
            Assert.Equal(exceptionMessage, exception.Message);
        }

        [Fact]
        public void Dispose_ShouldCallDispose()
        {
            // Act
            _service.Dispose();

            // Assert
            Assert.True(true); // Just to ensure Dispose method is called without exceptions
        }
    }
}
