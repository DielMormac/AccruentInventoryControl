using AccruentInventoryControl.Domain.Entities.Abstract;
using AccruentInventoryControl.Domain.Enums;

namespace AccruentInventoryControl.Domain.Entities
{
    public class WarehouseTransaction : BaseEntity
    {
        public Product Product { get; set; }

        public int Quantity { get; set; }

        public WarehouseTransactionType Type { get ; set; }

        public WarehouseTransactionStatus Status { get; set; }

        public int PreviousQuantity { get; set; }

        public int TotalQuantity { get; set; }
    }
}
