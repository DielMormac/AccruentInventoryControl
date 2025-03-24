using AccruentInventoryControl.Domain.Constants;
using System.ComponentModel.DataAnnotations;

namespace AccruentInventoryControl.Domain.ApiModels.Request
{
    public class WarehouseTransactionRequest
    {
        [Required(ErrorMessage = ErrorKeys.ProductCodeRequired)]
        [MaxLength(255, ErrorMessage = ErrorKeys.ProductCodeMaxLength)]
        public string ProductCode { get; set; }

        [Required(ErrorMessage = ErrorKeys.WarehouseTransactionTransactionTypeRequired)]
        [RegularExpression("^(inbound|outbound)$", ErrorMessage = ErrorKeys.WarehouseTransactionTransactionTypeInvalid)]
        public string Type { get; set; }

        [Required(ErrorMessage = ErrorKeys.WarehouseTransactionQuantityRequired)]
        [Range(1, int.MaxValue, ErrorMessage = ErrorKeys.WarehouseTransactionQuantityinvalid)]
        public int Quantity { get; set; }
    }
}
