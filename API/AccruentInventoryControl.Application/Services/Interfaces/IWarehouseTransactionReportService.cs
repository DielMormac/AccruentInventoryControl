using AccruentInventoryControl.Domain.Entities;

namespace AccruentInventoryControl.Application.Services.Interfaces
{
    /// <summary>
    /// Provides functionality to generate warehouse transaction reports.
    /// </summary>
    public interface IWarehouseTransactionReportService : IDisposable
    {

        /// <summary>
        /// Retrieves a warehouse transaction report for a specific date and product code.
        /// </summary>
        /// <param name="date">The date for which the report is to be generated.</param>
        /// <param name="productCode">The code of the product for which the report is to be generated.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the warehouse transaction report.</returns>
        Task<WarehouseTransactionReport> GetWarehouseTransactionReportByDateAndProduct(DateTime date, string productCode);
    }
}
