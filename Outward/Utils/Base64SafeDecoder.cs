namespace OutwardArchipelago.Utils
{
    internal abstract class Base64SafeDecoder<T> : IBase64SafeDecoder<T>
    {
        private static readonly Base64SafeDecoder<T> _default = CreateDefaultDecoder();
        public static Base64SafeDecoder<T> Default => _default;

        private static Base64SafeDecoder<T> CreateDefaultDecoder()
        {
            return new BinaryBase64SafeDecoder<T>();
        }

        public abstract T Decode(string data);
    }

    internal class BinaryBase64SafeDecoder<T> : Base64SafeDecoder<T>
    {
        public override T Decode(string data) => BinaryDecoder<T>.Default.Decode(Base64Safe.Decode(data));
    }
}
