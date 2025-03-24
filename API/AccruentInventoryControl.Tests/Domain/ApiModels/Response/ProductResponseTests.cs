using AccruentInventoryControl.Domain.ApiModels.Response;
using Xunit;

namespace AccruentInventoryControl.Tests.Domain.ApiModels.Response
{
    public class ProductResponseTests
    {
        [Fact]
        public void ProductResponse_ShouldSetAndGetProperties()
        {
            // Arrange
            var productResponse = new ProductResponse
            {
                Id = 1,
                Name = "Test Product",
                Code = "P12345"
            };

            // Act & Assert
            Assert.Equal(1, productResponse.Id);
            Assert.Equal("Test Product", productResponse.Name);
            Assert.Equal("P12345", productResponse.Code);
        }
    }
}
