using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccruentInventoryControl.Domain.ApiModels.Response
{
    public class WarehouseTransactionReportResponse
    {
        public string ProductCode { get; set; }
        public DateTime Date { get; set; }

        public List<WarehouseTransactionResponse> WarehouseTransactions { get; set; }

        public int Balance { get; set; }
    }
}
