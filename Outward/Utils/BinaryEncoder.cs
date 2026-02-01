using System;
using System.Text;

namespace OutwardArchipelago.Utils
{
    internal abstract class BinaryEncoder<T> : IBinaryEncoder<T>
    {
        private static readonly BinaryEncoder<T> _default = CreateDefault();
        public static BinaryEncoder<T> Default => _default;

        private static BinaryEncoder<T> CreateDefault()
        {
            var runtimeType = typeof(T);

            if (runtimeType == typeof(bool))
            {
                return (BinaryEncoder<T>)(object)new BooleanBinaryEncoder();
            }

            if (runtimeType == typeof(char))
            {
                return (BinaryEncoder<T>)(object)new CharBinaryEncoder();
            }

            if (runtimeType == typeof(sbyte))
            {
                return (BinaryEncoder<T>)(object)new SByteBinaryEncoder();
            }

            if (runtimeType == typeof(byte))
            {
                return (BinaryEncoder<T>)(object)new ByteBinaryEncoder();
            }

            if (runtimeType == typeof(short))
            {
                return (BinaryEncoder<T>)(object)new Int16BinaryEncoder();
            }

            if (runtimeType == typeof(ushort))
            {
                return (BinaryEncoder<T>)(object)new UInt16BinaryEncoder();
            }

            if (runtimeType == typeof(int))
            {
                return (BinaryEncoder<T>)(object)new Int32BinaryEncoder();
            }

            if (runtimeType == typeof(uint))
            {
                return (BinaryEncoder<T>)(object)new UInt32BinaryEncoder();
            }

            if (runtimeType == typeof(long))
            {
                return (BinaryEncoder<T>)(object)new Int64BinaryEncoder();
            }

            if (runtimeType == typeof(ulong))
            {
                return (BinaryEncoder<T>)(object)new UInt64BinaryEncoder();
            }

            if (runtimeType == typeof(float))
            {
                return (BinaryEncoder<T>)(object)new SingleBinaryEncoder();
            }

            if (runtimeType == typeof(double))
            {
                return (BinaryEncoder<T>)(object)new DoubleBinaryEncoder();
            }

            if (runtimeType == typeof(string))
            {
                return (BinaryEncoder<T>)(object)new StringBinaryEncoder();
            }

            return new ObjectBinaryEncoder<T>();
        }

        public abstract byte[] Encode(T value);
    }

    internal abstract class BitConverterBinaryEncoder<T> : BinaryEncoder<T>
    {
        public override byte[] Encode(T value)
        {
            byte[] bytes = GetBytes(value);

            // ensure bytes are in little-endian order
            if (!BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }

            return bytes;
        }

        protected abstract byte[] GetBytes(T value);
    }

    internal class BooleanBinaryEncoder : BitConverterBinaryEncoder<bool>
    {
        protected override byte[] GetBytes(bool value) => BitConverter.GetBytes(value);
    }

    internal class CharBinaryEncoder : BitConverterBinaryEncoder<char>
    {
        protected override byte[] GetBytes(char value) => BitConverter.GetBytes(value);
    }

    internal class SByteBinaryEncoder : BinaryEncoder<sbyte>
    {
        public override byte[] Encode(sbyte value) => new[] { (byte)value };
    }

    internal class ByteBinaryEncoder : BinaryEncoder<byte>
    {
        public override byte[] Encode(byte value) => new[] { value };
    }

    internal class Int16BinaryEncoder : BitConverterBinaryEncoder<short>
    {
        protected override byte[] GetBytes(short value) => BitConverter.GetBytes(value);
    }

    internal class UInt16BinaryEncoder : BitConverterBinaryEncoder<ushort>
    {
        protected override byte[] GetBytes(ushort value) => BitConverter.GetBytes(value);
    }

    internal class Int32BinaryEncoder : BitConverterBinaryEncoder<int>
    {
        protected override byte[] GetBytes(int value) => BitConverter.GetBytes(value);
    }

    internal class UInt32BinaryEncoder : BitConverterBinaryEncoder<uint>
    {
        protected override byte[] GetBytes(uint value) => BitConverter.GetBytes(value);
    }

    internal class Int64BinaryEncoder : BitConverterBinaryEncoder<long>
    {
        protected override byte[] GetBytes(long value) => BitConverter.GetBytes(value);
    }

    internal class UInt64BinaryEncoder : BitConverterBinaryEncoder<ulong>
    {
        protected override byte[] GetBytes(ulong value) => BitConverter.GetBytes(value);
    }

    internal class SingleBinaryEncoder : BitConverterBinaryEncoder<float>
    {
        protected override byte[] GetBytes(float value) => BitConverter.GetBytes(value);
    }

    internal class DoubleBinaryEncoder : BitConverterBinaryEncoder<double>
    {
        protected override byte[] GetBytes(double value) => BitConverter.GetBytes(value);
    }

    internal class StringBinaryEncoder : BinaryEncoder<string>
    {
        public override byte[] Encode(string value) => Encoding.UTF8.GetBytes(value);
    }

    internal class ObjectBinaryEncoder<T> : BinaryEncoder<T>
    {
        public override byte[] Encode(T value) => throw new NotImplementedException();
    }
}
