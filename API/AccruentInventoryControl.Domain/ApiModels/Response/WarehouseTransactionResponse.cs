namespace AccruentInventoryControl.Domain.ApiModels.Response
{
    public class WarehouseTransactionResponse
    {
        public int Id { get; set; }
        public ProductResponse Product { get; set; }
        public int Quantity { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public DateTime Date { get; set; }
        public int PreviousQuantity { get; set; }
        public int TotalQuantity { get; set; }
    }
}
