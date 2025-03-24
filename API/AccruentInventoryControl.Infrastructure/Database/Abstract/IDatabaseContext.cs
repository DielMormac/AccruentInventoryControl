using AccruentInventoryControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AccruentInventoryControl.Infrastructure.Database.Abstract
{
    /// <summary>
    /// Represents the database context interface that provides access to the database entities
    /// and supports resource disposal.
    /// </summary>
    public interface IDatabaseContext : IDisposable
    {
        /// <summary>
        /// Gets or sets the DbSet for managing <see cref="Product"/> entities in the database.
        /// </summary>
        DbSet<Product> Products { get; set; }

        /// <summary>
        /// Gets or sets the DbSet for managing <see cref="WarehouseTransaction"/> entities in the database.
        /// </summary>
        DbSet<WarehouseTransaction> WarehouseTransactions { get; set; }
    }
}
