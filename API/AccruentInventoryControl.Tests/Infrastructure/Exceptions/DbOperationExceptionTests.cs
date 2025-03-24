using AccruentInventoryControl.Infrastructure.Exceptions;

namespace AccruentInventoryControl.Tests.Infrastructure.Exceptions
{
    public class DbOperationExceptionTests
    {
        [Fact]
        public void DbOperationException_ShouldSetMessageCorrectly()
        {
            // Arrange
            var customMessage = "Test error message";
            var expectedMessage = $"A database operation error has occurred: {customMessage}";

            // Act
            var exception = new DbOperationException(customMessage);

            // Assert
            Assert.Equal(expectedMessage, exception.Message);
        }
    }
}
