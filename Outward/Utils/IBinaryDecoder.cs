namespace OutwardArchipelago.Utils
{
    internal interface IBinaryDecoder<T>
    {
        public abstract T Decode(byte[] bytes);
    }
}
