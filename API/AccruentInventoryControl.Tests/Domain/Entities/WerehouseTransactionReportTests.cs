using AccruentInventoryControl.Domain.Entities;

namespace AccruentInventoryControl.Tests.Domain.Entities
{
    public class WarehouseTransactionReportTests
    {
        [Fact]
        public void WarehouseTransactionReport_ShouldHaveProductCodeProperty()
        {
            // Arrange
            var report = new WarehouseTransactionReport();
            var productCode = "P12345";

            // Act
            report.ProductCode = productCode;

            // Assert
            Assert.Equal(productCode, report.ProductCode);
        }

        [Fact]
        public void WarehouseTransactionReport_ShouldHaveDateProperty()
        {
            // Arrange
            var report = new WarehouseTransactionReport();
            var date = DateTime.Now;

            // Act
            report.Date = date;

            // Assert
            Assert.Equal(date, report.Date);
        }

        [Fact]
        public void WarehouseTransactionReport_ShouldHaveWarehouseTransactionsProperty()
        {
            // Arrange
            var report = new WarehouseTransactionReport();
            var transactions = new List<WarehouseTransaction>
            {
                new WarehouseTransaction { Id = 1, Quantity = 10 },
                new WarehouseTransaction { Id = 2, Quantity = 20 }
            };

            // Act
            report.WarehouseTransactions = transactions;

            // Assert
            Assert.Equal(transactions, report.WarehouseTransactions);
        }

        [Fact]
        public void WarehouseTransactionReport_ShouldHaveBalanceProperty()
        {
            // Arrange
            var report = new WarehouseTransactionReport();
            var balance = 100;

            // Act
            report.Balance = balance;

            // Assert
            Assert.Equal(balance, report.Balance);
        }
    }
}