using AccruentInventoryControl.Application.Mappers;
using AccruentInventoryControl.Domain.ApiModels.Response;
using AccruentInventoryControl.Domain.Entities;
using AutoMapper;

namespace AccruentInventoryControl.Tests.Application.Mappers
{
    public class ProductMapperTests
    {
        private readonly IMapper _mapper;

        public ProductMapperTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ProductMapper>();
            });
            _mapper = config.CreateMapper();
        }

        [Fact]
        public void ProductMapper_ConfigurationIsValid()
        {
            // Arrange & Act
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ProductMapper>();
            });

            // Assert
            config.AssertConfigurationIsValid();
        }

        [Fact]
        public void ProductMapper_ShouldMapProductToProductResponse()
        {
            // Arrange
            var product = new Product
            {
                Id = 1,
                Name = "Test Product",
                Code = "P12345"
            };

            // Act
            var productResponse = _mapper.Map<ProductResponse>(product);

            // Assert
            Assert.Equal(product.Id, productResponse.Id);
            Assert.Equal(product.Name, productResponse.Name);
            Assert.Equal(product.Code, productResponse.Code);
        }
    }
}
