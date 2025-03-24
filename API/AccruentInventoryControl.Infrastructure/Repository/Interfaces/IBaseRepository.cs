namespace AccruentInventoryControl.Infrastructure.Repository.Interfaces
{
    /// <summary>
    /// Defines a generic repository interface for performing CRUD operations on entities of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of the entity that the repository will manage. Must be a class.</typeparam>
    public interface IBaseRepository<T> : IDisposable where T : class
    {
        /// <summary>
        /// Retrieves an entity by its unique identifier asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the entity to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the entity of type <typeparamref name="T"/>.</returns>
        Task<T> GetAsync(long id);

        /// <summary>
        /// Adds a new entity to the repository asynchronously.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the added entity of type <typeparamref name="T"/>.</returns>
        Task<T> AddAsync(T entity);

        /// <summary>
        /// Updates an existing entity in the repository asynchronously.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the updated entity of type <typeparamref name="T"/>.</returns>
        Task<T> UpdateAsync(T entity);

        /// <summary>
        /// Deletes an entity from the repository by its unique identifier asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the entity to delete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the deleted entity of type <typeparamref name="T"/>.</returns>
        Task<T> DeleteAsync(long id);

        /// <summary>
        /// Retrieves all entities from the repository asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains an enumerable collection of entities of type <typeparamref name="T"/>.</returns>
        Task<IEnumerable<T>> GetAllAsync();
    }
}
