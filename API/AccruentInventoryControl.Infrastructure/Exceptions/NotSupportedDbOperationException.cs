namespace AccruentInventoryControl.Infrastructure.Exceptions
{
    public class NotSupportedDbOperationException : Exception
    {
        private const string DefaultMessage = "Operation is not supported for this database context: {0}";

        public NotSupportedDbOperationException(string message) : base(String.Format(DefaultMessage, message))
        {
        }
    }
}
