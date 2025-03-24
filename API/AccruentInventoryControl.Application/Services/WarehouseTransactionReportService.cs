using AccruentInventoryControl.Application.Services.Interfaces;
using AccruentInventoryControl.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace AccruentInventoryControl.Application.Services
{
    public class WarehouseTransactionReportService : IWarehouseTransactionReportService
    {
        private readonly IWarehouseTransactionService _warehouseTransactionService;
        private readonly ILogger<WarehouseTransactionService> _logger;
        private bool _disposedValue = false;

        public WarehouseTransactionReportService(
            IWarehouseTransactionService warehouseTransactionService,
            ILogger<WarehouseTransactionService> logger)
        {
            _warehouseTransactionService = warehouseTransactionService;
            _logger = logger;
        }

        public async Task<WarehouseTransactionReport> GetWarehouseTransactionReportByDateAndProduct(DateTime date, string productCode)
        {
            try
            {
                var transactions = await _warehouseTransactionService.GetAllWarehouseTransactionsByDateAndProductCode(date, productCode);

                if (transactions == null || transactions.Count() == 0)
                {
                    _logger.LogWarning($"No transactions found for product {productCode} on date {date}");

                    return null;
                }

                var report = new WarehouseTransactionReport
                {
                    Date = date,
                    ProductCode = productCode,
                    Balance = transactions.Last().TotalQuantity,
                    WarehouseTransactions = transactions.ToList(),
                };

                return report;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);

                throw;
            }
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
                }
                _disposedValue = true;
            }
        }

        #endregion IDisposable Support
    }
}
