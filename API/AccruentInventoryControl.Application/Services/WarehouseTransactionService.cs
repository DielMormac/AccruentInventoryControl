using AccruentInventoryControl.Application.Services.Interfaces;
using AccruentInventoryControl.Domain.Constants;
using AccruentInventoryControl.Domain.Entities;
using AccruentInventoryControl.Domain.Enums;
using AccruentInventoryControl.Infrastructure.Repository.Interfaces;
using Microsoft.Extensions.Logging;

namespace AccruentInventoryControl.Application.Services
{
    public class WarehouseTransactionService : IWarehouseTransactionService
    {
        private readonly IProductRepository _productRepository;
        private readonly IWarehouseTransactionRepository _warehouseTransactionRepository;
        private readonly ILogger<WarehouseTransactionService> _logger;
        private bool _disposedValue = false;

        public WarehouseTransactionService(
            IProductRepository productRepository,
            IWarehouseTransactionRepository warehouseTransactionRepository,
            ILogger<WarehouseTransactionService> logger)
        {
            _productRepository = productRepository;
            _warehouseTransactionRepository = warehouseTransactionRepository;
            _logger = logger;
        }

        public async Task<WarehouseTransaction> GetWarehouseTransaction(long id)
        {
            _logger.LogInformation($"Getting warehouse transaction with id {id}");
            return await _warehouseTransactionRepository.GetAsync(id);
        }

        public async Task<IEnumerable<WarehouseTransaction>> GetAllWarehouseTransactions()
        {
            _logger.LogInformation("Getting all warehouse transactions");
            return await _warehouseTransactionRepository.GetAllAsync();
        }

        public async Task<WarehouseTransaction> CreateWarehouseTransaction(WarehouseTransaction warehouseTransaction)
        {
            _logger.LogInformation($"Creating warehouse transaction for product {warehouseTransaction.Product.Code}");
            var product = await _productRepository.GetByCodeAsync(warehouseTransaction.Product.Code);

            if (product == null)
            {
                _logger.LogError($"Product with code {warehouseTransaction.Product.Code} not found");
                throw new Exception(ErrorKeys.ProductNotFound);
            }

            warehouseTransaction.Product = product;

            _logger.LogInformation($"Getting last warehouse transaction for product {warehouseTransaction.Product.Code}");
            var lastWarehouseTransaction = await _warehouseTransactionRepository.GetLastCompletedWarehouseTransaction(warehouseTransaction.Product.Code);

            if (lastWarehouseTransaction == null)
            {
                if (warehouseTransaction.Type == WarehouseTransactionType.Outbound)
                {
                    _logger.LogError($"Product with code {warehouseTransaction.Product.Code} out of stock");

                    warehouseTransaction.PreviousQuantity = 0;
                    warehouseTransaction.TotalQuantity = 0;
                    warehouseTransaction.Status = WarehouseTransactionStatus.OutOfStock;

                    return await _warehouseTransactionRepository.AddAsync(warehouseTransaction);
                }

                warehouseTransaction.PreviousQuantity = 0;
                warehouseTransaction.TotalQuantity = warehouseTransaction.Quantity;
                warehouseTransaction.Status = WarehouseTransactionStatus.Completed;

                return await _warehouseTransactionRepository.AddAsync(warehouseTransaction);
            }

            warehouseTransaction.PreviousQuantity = lastWarehouseTransaction.TotalQuantity;

            switch (warehouseTransaction.Type)
            {
                case WarehouseTransactionType.Outbound:
                    warehouseTransaction.TotalQuantity = warehouseTransaction.PreviousQuantity - warehouseTransaction.Quantity;
                    break;
                case WarehouseTransactionType.Inbound:
                    warehouseTransaction.TotalQuantity = warehouseTransaction.PreviousQuantity + warehouseTransaction.Quantity;
                    break;

            }

            if (warehouseTransaction.TotalQuantity < 0)
            {
                warehouseTransaction.TotalQuantity = warehouseTransaction.PreviousQuantity;
                warehouseTransaction.Status = WarehouseTransactionStatus.OutOfStock;

                return await _warehouseTransactionRepository.AddAsync(warehouseTransaction);
            }

            warehouseTransaction.Status = WarehouseTransactionStatus.Completed;
            return await _warehouseTransactionRepository.AddAsync(warehouseTransaction);
        }


        public async Task<IEnumerable<WarehouseTransaction>> GetAllWarehouseTransactionsByDateAndProductCode(DateTime date, string productCode)
        {
            _logger.LogInformation($"Getting all warehouse transactions for product {productCode}");

            if(productCode.Equals(String.Empty))
                return await _warehouseTransactionRepository.GetAllWarehouseTransactionByDate(date);


            return await _warehouseTransactionRepository.GetAllWarehouseTransactionByDateAndProductCode(date, productCode);
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
                    _warehouseTransactionRepository?.Dispose();
                }
                _disposedValue = true;
            }
        }

        #endregion IDisposable Support

    }
}
