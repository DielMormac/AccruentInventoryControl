using System;
using AccruentInventoryControl.Domain.ApiModels.Response;
using Xunit;

namespace AccruentInventoryControl.Tests.Domain.ApiModels.Response
{
    public class WarehouseTransactionResponseTests
    {
        [Fact]
        public void WarehouseTransactionResponse_ShouldSetAndGetProperties()
        {
            // Arrange
            var productResponse = new ProductResponse
            {
                Id = 1,
                Name = "Test Product",
                Code = "P12345"
            };
            var date = DateTime.Now;
            var transactionResponse = new WarehouseTransactionResponse
            {
                Id = 1,
                Product = productResponse,
                Quantity = 10,
                Type = "inbound",
                Status = "completed",
                Date = date,
                PreviousQuantity = 5,
                TotalQuantity = 15
            };

            // Act & Assert
            Assert.Equal(1, transactionResponse.Id);
            Assert.Equal(productResponse, transactionResponse.Product);
            Assert.Equal(10, transactionResponse.Quantity);
            Assert.Equal("inbound", transactionResponse.Type);
            Assert.Equal("completed", transactionResponse.Status);
            Assert.Equal(date, transactionResponse.Date);
            Assert.Equal(5, transactionResponse.PreviousQuantity);
            Assert.Equal(15, transactionResponse.TotalQuantity);
        }
    }
}
