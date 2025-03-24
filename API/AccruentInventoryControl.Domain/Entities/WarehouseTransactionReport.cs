using AccruentInventoryControl.Domain.Entities.Abstract;

namespace AccruentInventoryControl.Domain.Entities
{
    public class WarehouseTransactionReport
    {
        public string ProductCode { get; set; }

        public DateTime Date { get; set; }

        public List<WarehouseTransaction> WarehouseTransactions { get; set; }

        public int Balance { get; set; }
    }
}
