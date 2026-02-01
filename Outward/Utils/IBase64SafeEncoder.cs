namespace OutwardArchipelago.Utils
{
    internal interface IBase64SafeEncoder<T>
    {
        public abstract string Encode(T value);
    }
}
