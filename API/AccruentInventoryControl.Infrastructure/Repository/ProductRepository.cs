using AccruentInventoryControl.Domain.Constants;
using AccruentInventoryControl.Domain.Entities;
using AccruentInventoryControl.Infrastructure.Database.Abstract;
using AccruentInventoryControl.Infrastructure.Exceptions;
using AccruentInventoryControl.Infrastructure.Repository.Interfaces;
using Microsoft.Extensions.Logging;

namespace AccruentInventoryControl.Infrastructure.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly IDatabaseContext _dbContext;
        private readonly ILogger<ProductRepository> _logger;
        private bool _disposedValue = false;

        public ProductRepository(
            IDatabaseContext dbContext,
            ILogger<ProductRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<Product> AddAsync(Product entity)
        {
            try
            {
                _logger.LogInformation($"Adding new product with code {entity.Code} to database");

                entity.CreatedAt = DateTime.Now;
                entity.UpdatedAt = DateTime.Now;

                var product = await _dbContext.Products.AddAsync(entity);

                if (product != null)
                {
                    return product.Entity;
                }

                _logger.LogError(ErrorKeys.ProductDatabaseExceptionMessage);
                throw new DbOperationException(ErrorKeys.ProductDatabaseExceptionMessage);
            }
            catch (Exception ex)
            {
                throw new DbOperationException(ex.Message);
            }
        }

        public async Task<Product> DeleteAsync(long id)
        {
            throw new NotSupportedDbOperationException("DeleteAsync(long id)");
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Getting all products from database");

                var products = _dbContext.Products.Local
                    .Where(p => p.DeletedAt == null)
                    .ToList();

                return products;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new DbOperationException(ex.Message);
            }
        }

        public async Task<Product> GetAsync(long id)
        {
            try
            {
                _logger.LogInformation($"Getting product with id {id}");

                var product = _dbContext.Products.Local
                    .Where(p => p.Id == id
                        && p.DeletedAt == null)
                    .FirstOrDefault();

                return product;
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message);
                throw new DbOperationException(ex.Message);
            }
        }

        public async Task<Product> UpdateAsync(Product entity)
        {
            throw new NotSupportedDbOperationException("UpdateAsync(Product entity)");
        }

        public async Task<Product> GetByCodeAsync(string code)
        {
            try
            {
                _logger.LogInformation($"Getting product with code {code}");

                var product = _dbContext.Products.Local
                    .Where(p => p.Code == code
                        && p.DeletedAt == null)
                    .FirstOrDefault();

                return product;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new DbOperationException(ex.Message);
            }
        }

        #region IDisposable Support

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion IDisposable Support
    }
}
