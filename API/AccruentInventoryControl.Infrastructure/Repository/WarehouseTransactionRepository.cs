using AccruentInventoryControl.Domain.Constants;
using AccruentInventoryControl.Domain.Entities;
using AccruentInventoryControl.Domain.Enums;
using AccruentInventoryControl.Infrastructure.Database.Abstract;
using AccruentInventoryControl.Infrastructure.Exceptions;
using AccruentInventoryControl.Infrastructure.Repository.Interfaces;
using Microsoft.Extensions.Logging;

namespace AccruentInventoryControl.Infrastructure.Repository
{
    public class WarehouseTransactionRepository : IWarehouseTransactionRepository
    {
        private readonly IDatabaseContext _dbContext;
        private readonly ILogger<WarehouseTransactionRepository> _logger;
        private bool _disposedValue = false;
        public WarehouseTransactionRepository(
            IDatabaseContext dbContext,
            ILogger<WarehouseTransactionRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<WarehouseTransaction> AddAsync(WarehouseTransaction entity)
        {
            try
            {
                _logger.LogInformation("Adding new warehouse transaction to database");

                if(entity.CreatedAt.Equals(DateTime.MinValue))
                    entity.CreatedAt = DateTime.Now;

                entity.UpdatedAt = DateTime.Now;

                var warehouseTransactions = await _dbContext.WarehouseTransactions.AddAsync(entity);

                if (warehouseTransactions != null)
                {
                    return warehouseTransactions.Entity;
                }

                _logger.LogError("Error adding new warehouse transaction to database");
                throw new DbOperationException(ErrorKeys.WarehouseTransactionDatabaseExceptionMessage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new DbOperationException(ex.Message);
            }
        }

        public async Task<WarehouseTransaction> DeleteAsync(long id)
        {
            throw new NotSupportedDbOperationException("DeleteAsync(long id)");
        }

        public async Task<IEnumerable<WarehouseTransaction>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Getting all warehouse transactions");

                var warehouseTransactions = _dbContext.WarehouseTransactions.Local
                    .Where(wt => wt.DeletedAt == null)
                    .ToList();

                return warehouseTransactions;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new DbOperationException(ex.Message);
            }
        }

        public async Task<WarehouseTransaction> GetAsync(long id)
        {
            try
            {
                _logger.LogInformation($"Getting warehouse transaction with id {id}");

                var warehouseTransaction = _dbContext.WarehouseTransactions.Local
                    .Where(wt => wt.Id == id
                        && wt.DeletedAt == null)
                    .FirstOrDefault();

                return warehouseTransaction;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new DbOperationException(ex.Message);
            }
        }

        public async Task<WarehouseTransaction> UpdateAsync(WarehouseTransaction entity)
        {
            throw new NotSupportedDbOperationException("UpdateAsync(WarehouseTransaction entity)");
        }

        public async Task<WarehouseTransaction> GetLastCompletedWarehouseTransaction(string productCode)
        {
            try
            {
                _logger.LogInformation($"Getting last warehouse transaction for product with code {productCode}");

                var warehouseTransaction = _dbContext.WarehouseTransactions.Local
                    .Where(wt => wt.DeletedAt == null
                        && wt.Product.Code.Equals(productCode)
                        && wt.Status.Equals(WarehouseTransactionStatus.Completed))
                    .OrderByDescending(wt => wt.CreatedAt)
                    .FirstOrDefault();

                return warehouseTransaction;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new DbOperationException(ex.Message);
            }
        }

        public async Task<IEnumerable<WarehouseTransaction>> GetAllWarehouseTransactionByProductCode(string productCode)
        {
            try
            {
                _logger.LogInformation($"Getting all warehouse transactions for product with code {productCode}");

                var warehouseTransactions = _dbContext.WarehouseTransactions.Local
                    .Where(wt => wt.DeletedAt == null
                        && wt.Product.Code.Equals(productCode))
                    .ToList();

                return warehouseTransactions;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new DbOperationException(ex.Message);
            }
        }

        public async Task<IEnumerable<WarehouseTransaction>> GetAllWarehouseTransactionByDateAndProductCode(DateTime date, string productCode)
        {
            try
            {
                _logger.LogInformation($"Getting all warehouse transactions for product with code {productCode} and date {date}");

                var warehouseTransactions = _dbContext.WarehouseTransactions.Local
                    .Where(wt => wt.DeletedAt == null
                        && wt.Product.Code.Equals(productCode)
                        && wt.CreatedAt.Date.Equals(date.Date))
                    .OrderBy(wt =>  wt.CreatedAt)
                    .ToList();

                return warehouseTransactions;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new DbOperationException(ex.Message);
            }
        }

        public async Task<IEnumerable<WarehouseTransaction>> GetAllWarehouseTransactionByDate(DateTime date)
        {
            try
            {
                _logger.LogInformation($"Getting all warehouse transactions for date {date}");

                var warehouseTransactions = _dbContext.WarehouseTransactions.Local
                    .Where(wt => wt.DeletedAt == null
                        && wt.CreatedAt.Date.Equals(date.Date))
                    .OrderBy(wt => wt.CreatedAt)
                    .ToList();

                return warehouseTransactions;
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
