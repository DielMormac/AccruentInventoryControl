using System;
using System.Collections.Generic;
using AccruentInventoryControl.Domain.ApiModels.Response;
using Xunit;

namespace AccruentInventoryControl.Tests.Domain.ApiModels.Response
{
    public class WarehouseTransactionReportResponseTests
    {
        [Fact]
        public void WarehouseTransactionReportResponse_ShouldSetAndGetProperties()
        {
            // Arrange
            var date = DateTime.Now;
            var warehouseTransactions = new List<WarehouseTransactionResponse>
            {
                new WarehouseTransactionResponse { Id = 1, Quantity = 10 },
                new WarehouseTransactionResponse { Id = 2, Quantity = 20 }
            };
            var reportResponse = new WarehouseTransactionReportResponse
            {
                ProductCode = "P12345",
                Date = date,
                WarehouseTransactions = warehouseTransactions,
                Balance = 30
            };

            // Act & Assert
            Assert.Equal("P12345", reportResponse.ProductCode);
            Assert.Equal(date, reportResponse.Date);
            Assert.Equal(warehouseTransactions, reportResponse.WarehouseTransactions);
            Assert.Equal(30, reportResponse.Balance);
        }
    }
}
