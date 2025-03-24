using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccruentInventoryControl.Domain.Constants
{
    public class WarehouseTransactionStatusConstants
    {
        public const string Pending = "Pending";
        public const string OutOfStock = "Out of Stock";
        public const string Completed = "Completed";
        public const string Cancelled = "Cancelled";
        public const string Unknown = "Unknown";
    }
}
