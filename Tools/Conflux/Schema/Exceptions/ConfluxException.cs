namespace Conflux.Schema.Exceptions
{
    internal class ConfluxException : Exception
    {
        public ConfluxException() : base() { }

        public ConfluxException(string? message) : base(message) { }

        public ConfluxException(string? message, Exception? innerException) : base(message, innerException) { }
    }
}
