namespace OutwardArchipelago.Utils
{
    internal interface IBinaryEncoder<T>
    {
        public abstract byte[] Encode(T value);
    }
}
