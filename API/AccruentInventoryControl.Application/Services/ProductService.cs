using AccruentInventoryControl.Application.Services.Interfaces;
using AccruentInventoryControl.Domain.Entities;
using AccruentInventoryControl.Infrastructure.Repository.Interfaces;
using Microsoft.Extensions.Logging;

namespace AccruentInventoryControl.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<ProductService> _logger;
        private bool _disposedValue = false;

        public ProductService(
            IProductRepository productRepository,
            ILogger<ProductService> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        public async Task<Product> GetProduct(long id)
        {
            _logger.LogInformation($"Getting product with id {id}");
            return await _productRepository.GetAsync(id);
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            _logger.LogInformation("Getting all products");

            return await _productRepository.GetAllAsync(); ;
        }

        #region IDisposable Support
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _productRepository?.Dispose();
                }
                _disposedValue = true;
            }
        }

        #endregion IDisposable Support
    }
}
