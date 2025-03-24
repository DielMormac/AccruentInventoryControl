namespace AccruentInventoryControl.Domain.Constants
{
    public class ErrorKeys
    {
        public const string ProductNotFound = "Product not found.";
        public const string ProductOutOfStock = "Product out of stock.";
        public const string ProductCodeRequired = "Missing product code.";
        public const string InvalidProductCode = "Invalid product code.";
        public const string ProductCodeMaxLength = "Product name exceeded max length.";

        public const string WarehouseTransactionQuantityRequired = "Missing quantity.";
        public const string WarehouseTransactionQuantityinvalid = "Quantity must be a value between 1 and 2147483647.";
        public const string WarehouseTransactionTransactionTypeRequired = "Missing transaction type.";
        public const string WarehouseTransactionTransactionTypeInvalid = "Invalid transaction type. The accepted values are 'inbound' or 'outbound'.";

        public const string WarehouseTransactionReportDateRequired = "Missing date.";

        public const string InMemoryDatabaseBadRequest = "InMemoryDatabase already initialized.";
        public const string ProductDatabaseExceptionMessage = "Not possible to add new product to database.";
        public const string WarehouseTransactionDatabaseExceptionMessage = "Not possible to add new warehouse transaction to database.";
        public const string QueryParametersOrderByErrorMessage = "The orderby parameter is invalid. It must only contain the field name and an optional sort order (asc or desc).";
    }
}
