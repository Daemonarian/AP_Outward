namespace OutwardArchipelago.Utils
{
    internal abstract class Base64SafeEncoder<T> : IBase64SafeEncoder<T>
    {
        private static readonly Base64SafeEncoder<T> _default = CreateDefaultEncoder();
        public static Base64SafeEncoder<T> Default => _default;

        private static Base64SafeEncoder<T> CreateDefaultEncoder()
        {
            return new BinaryBase64SafeEncoder<T>();
        }

        public abstract string Encode(T value);
    }

    internal class BinaryBase64SafeEncoder<T> : Base64SafeEncoder<T>
    {
        public override string Encode(T value) => Base64Safe.Encode(BinaryEncoder<T>.Default.Encode(value));
    }
}
