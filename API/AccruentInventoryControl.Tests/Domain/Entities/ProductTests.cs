using AccruentInventoryControl.Domain.Entities.Abstract;
using AccruentInventoryControl.Domain.Entities;

namespace AccruentInventoryControl.Tests.Domain.Entities
{
    public class ProductTests
    {
        [Fact]
        public void Product_ShouldInheritFromBaseEntity()
        {
            // Arrange
            var product = new Product();

            // Act & Assert
            Assert.IsAssignableFrom<BaseEntity>(product);
        }

        [Fact]
        public void Product_ShouldHaveIdProperty()
        {
            // Arrange
            var product = new Product();
            var id = 123;

            // Act
            product.Id = id;

            // Assert
            Assert.Equal(id, product.Id);
        }

        [Fact]
        public void Product_ShouldHaveCreatedAtProperty()
        {
            // Arrange
            var product = new Product();
            var createdAt = DateTime.Now;

            // Act
            product.CreatedAt = createdAt;

            // Assert
            Assert.Equal(createdAt, product.CreatedAt);
        }

        [Fact]
        public void Product_ShouldHaveUpdatedAtProperty()
        {
            // Arrange
            var product = new Product();
            var updatedAt = DateTime.Now;

            // Act
            product.UpdatedAt = updatedAt;

            // Assert
            Assert.Equal(updatedAt, product.UpdatedAt);
        }

        [Fact]
        public void Product_ShouldHaveDeletedAtProperty()
        {
            // Arrange
            var product = new Product();
            var deletedAt = DateTime.Now;

            // Act
            product.DeletedAt = deletedAt;

            // Assert
            Assert.Equal(deletedAt, product.DeletedAt);
        }

        [Fact]
        public void Product_DeletedAt_ShouldBeNullByDefault()
        {
            // Arrange
            var product = new Product();

            // Act & Assert
            Assert.Null(product.DeletedAt);
        }
    }
}