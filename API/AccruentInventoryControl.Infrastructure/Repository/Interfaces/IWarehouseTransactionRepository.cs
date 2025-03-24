using AccruentInventoryControl.Domain.Entities;

namespace AccruentInventoryControl.Infrastructure.Repository.Interfaces
{
    /// <summary>
    /// Interface for managing warehouse transactions in the repository.
    /// </summary>
    public interface IWarehouseTransactionRepository : IBaseRepository<WarehouseTransaction>
    {
        /// <summary>
        /// Retrieves the last completed warehouse transaction for a given product code.
        /// </summary>
        /// <param name="productCode">The code of the product to retrieve the transaction for.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the last completed <see cref="WarehouseTransaction"/>.</returns>
        Task<WarehouseTransaction> GetLastCompletedWarehouseTransaction(string productCode);

        /// <summary>
        /// Retrieves all warehouse transactions for a specific date and product code.
        /// </summary>
        /// <param name="date">The date to filter the transactions by.</param>
        /// <param name="productCode">The code of the product to filter the transactions by.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of <see cref="WarehouseTransaction"/> objects.</returns>
        Task<IEnumerable<WarehouseTransaction>> GetAllWarehouseTransactionByDateAndProductCode(DateTime date, string productCode);

        /// <summary>
        /// Retrieves all warehouse transactions for a specific date.
        /// </summary>
        /// <param name="date">The date to filter the transactions by.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of <see cref="WarehouseTransaction"/> objects.</returns>
        Task<IEnumerable<WarehouseTransaction>> GetAllWarehouseTransactionByDate(DateTime date);
    }
}
