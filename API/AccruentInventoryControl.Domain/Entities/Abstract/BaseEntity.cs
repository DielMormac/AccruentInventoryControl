namespace AccruentInventoryControl.Domain.Entities.Abstract
{
    /// <summary>
    /// Represents a base entity interface with common properties.
    /// </summary>
    public abstract class BaseEntity
    {
        /// <summary>
        /// Gets or sets the unique identifier for the entity.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the entity was created.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the entity was last updated.
        /// </summary>
        public DateTime UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; } = null;
    }
}