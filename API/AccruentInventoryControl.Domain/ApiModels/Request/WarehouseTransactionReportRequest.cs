using AccruentInventoryControl.Domain.Constants;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AccruentInventoryControl.Domain.ApiModels.Request
{
    public class WarehouseTransactionReportRequest
    {
        [Required(ErrorMessage = ErrorKeys.WarehouseTransactionReportDateRequired)]
        public DateTime Date { get; set; }

        [MaxLength(255, ErrorMessage = ErrorKeys.ProductCodeMaxLength)]
        public string ProductCode { get; set; } = String.Empty;
    }
}
