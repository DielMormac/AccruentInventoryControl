using AccruentInventoryControl.Domain.Entities;

namespace AccruentInventoryControl.Application.Services.Interfaces
{
    /// <summary>
    /// Defines the contract for product-related operations.
    /// </summary>
    public interface IProductService : IDisposable
    {
        /// <summary>
        /// Retrieves a product by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the product.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the product.</returns>
        Task<Product> GetProduct(long id);

        /// <summary>
        /// Retrieves all products.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of products.</returns>
        Task<IEnumerable<Product>> GetAllProducts();
    }
}
