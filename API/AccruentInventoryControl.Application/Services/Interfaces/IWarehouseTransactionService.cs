using AccruentInventoryControl.Domain.Entities;

namespace AccruentInventoryControl.Application.Services.Interfaces
{
    /// <summary>
    /// Defines the contract for warehouse transaction services.
    /// </summary>
    public interface IWarehouseTransactionService : IDisposable
    {
        /// <summary>
        /// Retrieves a warehouse transaction by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the warehouse transaction.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the warehouse transaction.</returns>
        Task<WarehouseTransaction> GetWarehouseTransaction(long id);

        /// <summary>
        /// Retrieves all warehouse transactions.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of all warehouse transactions.</returns>
        Task<IEnumerable<WarehouseTransaction>> GetAllWarehouseTransactions();

        /// <summary>
        /// Retrieves all warehouse transactions filtered by a specific date and product code.
        /// </summary>
        /// <param name="date">The date to filter the transactions.</param>
        /// <param name="productCode">The product code to filter the transactions.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of filtered warehouse transactions.</returns>
        Task<IEnumerable<WarehouseTransaction>> GetAllWarehouseTransactionsByDateAndProductCode(DateTime date, string productCode);

        /// <summary>
        /// Creates a new warehouse transaction.
        /// </summary>
        /// <param name="warehouseTransaction">The warehouse transaction to create.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the created warehouse transaction.</returns>
        Task<WarehouseTransaction> CreateWarehouseTransaction(WarehouseTransaction warehouseTransaction);
    }
}