namespace AccruentInventoryControl.Application.Services.Interfaces
{
    /// <summary>
    /// Defines the contract for a service that initializes an in-memory database.
    /// </summary>
    public interface IInMemoryDataBaseInitializerService
    {
        /// <summary>
        /// Asynchronously initializes the in-memory database.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains a boolean
        /// indicating whether the initialization was successful.
        /// </returns>
        Task<bool> Initialize();

        /// <summary>
        /// Determines whether the in-memory database has already been initialized.
        /// </summary>
        /// <returns>
        /// A boolean value indicating whether the database has been initialized.
        /// </returns>
        bool HasInitilized();
    }
}
