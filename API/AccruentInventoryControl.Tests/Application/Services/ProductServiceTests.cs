using AccruentInventoryControl.Application.Services;
using AccruentInventoryControl.Domain.Entities;
using AccruentInventoryControl.Infrastructure.Repository.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;

namespace AccruentInventoryControl.Tests.Application.Services
{
    public class ProductServiceTests
    {
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly Mock<ILogger<ProductService>> _loggerMock;
        private readonly ProductService _productService;

        public ProductServiceTests()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _loggerMock = new Mock<ILogger<ProductService>>();
            _productService = new ProductService(_productRepositoryMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task GetProduct_ShouldReturnProduct_WhenProductExists()
        {
            // Arrange
            var productId = 1;
            var expectedProduct = new Product { Id = productId, Name = "Test Product" };
            _productRepositoryMock.Setup(repo => repo.GetAsync(productId)).ReturnsAsync(expectedProduct);

            // Act
            var result = await _productService.GetProduct(productId);

            // Assert
            Assert.Equal(expectedProduct, result);
        }

        [Fact]
        public async Task GetAllProducts_ShouldReturnAllProducts()
        {
            // Arrange
            var expectedProducts = new List<Product>
            {
                new Product { Id = 1, Name = "Product 1" },
                new Product { Id = 2, Name = "Product 2" }
            };
            _productRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(expectedProducts);

            // Act
            var result = await _productService.GetAllProducts();

            // Assert
            Assert.Equal(expectedProducts, result);
        }

        [Fact]
        public void Dispose_ShouldCallDisposeOnProductRepository()
        {
            // Act
            _productService.Dispose();

            // Assert
            _productRepositoryMock.Verify(repo => repo.Dispose(), Times.Once);
        }
    }
}
