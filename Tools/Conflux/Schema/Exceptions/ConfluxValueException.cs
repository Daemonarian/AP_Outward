namespace Conflux.Schema.Exceptions
{
    internal class ConfluxValueException : ConfluxException
    {
        public ConfluxValueException() : base() { }

        public ConfluxValueException(string? message) : base(message) { }

        public ConfluxValueException(string? message, Exception? innerException) : base(message, innerException) { }
    }
}
