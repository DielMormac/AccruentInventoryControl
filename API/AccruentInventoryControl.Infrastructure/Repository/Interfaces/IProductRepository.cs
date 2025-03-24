using AccruentInventoryControl.Domain.Entities;

namespace AccruentInventoryControl.Infrastructure.Repository.Interfaces
{
    /// <summary>
    /// Represents the repository interface for managing <see cref="Product"/> entities.
    /// </summary>
    public interface IProductRepository : IBaseRepository<Product>
    {

        /// <summary>
        /// Asynchronously retrieves a <see cref="Product"/> entity by its unique code.
        /// </summary>
        /// <param name="code">The unique code of the product to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the <see cref="Product"/> entity if found; otherwise, null.</returns>
        Task<Product> GetByCodeAsync(string code);
    }
}
