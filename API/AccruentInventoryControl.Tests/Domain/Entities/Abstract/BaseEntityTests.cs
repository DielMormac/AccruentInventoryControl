using System;
using Xunit;
using AccruentInventoryControl.Domain.Entities.Abstract;

namespace AccruentInventoryControl.Tests.Domain.Entities.Abstract
{
    public class BaseEntityTests
    {
        [Fact]
        public void BaseEntity_ShouldHaveIdProperty()
        {
            // Arrange
            var entity = new TestEntity();
            var id = 123;

            // Act
            entity.Id = id;

            // Assert
            Assert.Equal(id, entity.Id);
        }

        [Fact]
        public void BaseEntity_ShouldHaveCreatedAtProperty()
        {
            // Arrange
            var entity = new TestEntity();
            var createdAt = DateTime.Now;

            // Act
            entity.CreatedAt = createdAt;

            // Assert
            Assert.Equal(createdAt, entity.CreatedAt);
        }

        [Fact]
        public void BaseEntity_ShouldHaveUpdatedAtProperty()
        {
            // Arrange
            var entity = new TestEntity();
            var updatedAt = DateTime.Now;

            // Act
            entity.UpdatedAt = updatedAt;

            // Assert
            Assert.Equal(updatedAt, entity.UpdatedAt);
        }

        [Fact]
        public void BaseEntity_ShouldHaveDeletedAtProperty()
        {
            // Arrange
            var entity = new TestEntity();
            var deletedAt = DateTime.Now;

            // Act
            entity.DeletedAt = deletedAt;

            // Assert
            Assert.Equal(deletedAt, entity.DeletedAt);
        }

        [Fact]
        public void BaseEntity_DeletedAt_ShouldBeNullByDefault()
        {
            // Arrange
            var entity = new TestEntity();

            // Act & Assert
            Assert.Null(entity.DeletedAt);
        }

        // A test class that inherits from BaseEntity for testing purposes
        private class TestEntity : BaseEntity
        {
        }
    }
}
