using AccruentInventoryControl.Domain.Entities.Abstract;
using AccruentInventoryControl.Domain.Entities;
using AccruentInventoryControl.Domain.Enums;

namespace AccruentInventoryControl.Tests.Domain.Entities
{
    public class WarehouseTransactionTests
    {
        [Fact]
        public void WarehouseTransaction_ShouldInheritFromBaseEntity()
        {
            // Arrange
            var transaction = new WarehouseTransaction();

            // Act & Assert
            Assert.IsAssignableFrom<BaseEntity>(transaction);
        }

        [Fact]
        public void WarehouseTransaction_ShouldHaveProductProperty()
        {
            // Arrange
            var transaction = new WarehouseTransaction();
            var product = new Product();

            // Act
            transaction.Product = product;

            // Assert
            Assert.Equal(product, transaction.Product);
        }

        [Fact]
        public void WarehouseTransaction_ShouldHaveQuantityProperty()
        {
            // Arrange
            var transaction = new WarehouseTransaction();
            var quantity = 10;

            // Act
            transaction.Quantity = quantity;

            // Assert
            Assert.Equal(quantity, transaction.Quantity);
        }

        [Fact]
        public void WarehouseTransaction_ShouldHaveTypeProperty()
        {
            // Arrange
            var transaction = new WarehouseTransaction();
            var type = WarehouseTransactionType.Inbound;

            // Act
            transaction.Type = type;

            // Assert
            Assert.Equal(type, transaction.Type);
        }

        [Fact]
        public void WarehouseTransaction_ShouldHaveStatusProperty()
        {
            // Arrange
            var transaction = new WarehouseTransaction();
            var status = WarehouseTransactionStatus.Completed;

            // Act
            transaction.Status = status;

            // Assert
            Assert.Equal(status, transaction.Status);
        }

        [Fact]
        public void WarehouseTransaction_ShouldHavePreviousQuantityProperty()
        {
            // Arrange
            var transaction = new WarehouseTransaction();
            var previousQuantity = 5;

            // Act
            transaction.PreviousQuantity = previousQuantity;

            // Assert
            Assert.Equal(previousQuantity, transaction.PreviousQuantity);
        }

        [Fact]
        public void WarehouseTransaction_ShouldHaveTotalQuantityProperty()
        {
            // Arrange
            var transaction = new WarehouseTransaction();
            var totalQuantity = 15;

            // Act
            transaction.TotalQuantity = totalQuantity;

            // Assert
            Assert.Equal(totalQuantity, transaction.TotalQuantity);
        }
    }
}