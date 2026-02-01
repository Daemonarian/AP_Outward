namespace OutwardArchipelago.Utils
{
    internal interface IBase64SafeDecoder<T>
    {
        public abstract T Decode(string data);
    }
}
