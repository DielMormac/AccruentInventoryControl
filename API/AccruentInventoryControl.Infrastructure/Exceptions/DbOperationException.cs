namespace AccruentInventoryControl.Infrastructure.Exceptions
{
    public class DbOperationException : Exception
    {
        private const string DefaultMessage = "A database operation error has occurred: {0}";

        public DbOperationException(string message) : base(string.Format(DefaultMessage, message))
        {
        }
    }
}
