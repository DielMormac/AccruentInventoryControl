using AccruentInventoryControl.Domain.Entities.Abstract;

namespace AccruentInventoryControl.Domain.Entities;

/// <summary>
/// Represents a product entity in the inventory control domain.
/// </summary>
public class Product : BaseEntity
{

    /// <summary>
    /// Gets or sets the name of the product.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the code associated with the product.
    /// </summary>
    public string Code { get; set; }
}