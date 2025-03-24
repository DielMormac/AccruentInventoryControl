using AccruentInventoryControl.Infrastructure.Exceptions;

namespace AccruentInventoryControl.Tests.Infrastructure.Exceptions
{
    public class NotSupportedDbOperationExceptionTests
    {
        [Fact]
        public void NotSupportedDbOperationException_ShouldSetMessageCorrectly()
        {
            // Arrange
            var customMessage = "Test operation";
            var expectedMessage = $"Operation is not supported for this database context: {customMessage}";

            // Act
            var exception = new NotSupportedDbOperationException(customMessage);

            // Assert
            Assert.Equal(expectedMessage, exception.Message);
        }
    }
}
