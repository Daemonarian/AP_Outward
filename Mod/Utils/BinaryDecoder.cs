using System;
using System.Text;
using OutwardArchipelago.Archipelago;

namespace OutwardArchipelago.Utils
{
    internal abstract class BinaryDecoder<T> : IBinaryDecoder<T>
    {
        private static readonly BinaryDecoder<T> _default = CreateDefault();
        public static BinaryDecoder<T> Default => _default;

        private static BinaryDecoder<T> CreateDefault()
        {
            var runtimeType = typeof(T);

            if (runtimeType == typeof(bool))
            {
                return (BinaryDecoder<T>)(object)new BooleanBinaryDecoder();
            }

            if (runtimeType == typeof(char))
            {
                return (BinaryDecoder<T>)(object)new CharBinaryDecoder();
            }

            if (runtimeType == typeof(sbyte))
            {
                return (BinaryDecoder<T>)(object)new SByteBinaryDecoder();
            }

            if (runtimeType == typeof(byte))
            {
                return (BinaryDecoder<T>)(object)new ByteBinaryDecoder();
            }

            if (runtimeType == typeof(short))
            {
                return (BinaryDecoder<T>)(object)new Int16BinaryDecoder();
            }

            if (runtimeType == typeof(ushort))
            {
                return (BinaryDecoder<T>)(object)new UInt16BinaryDecoder();
            }

            if (runtimeType == typeof(int))
            {
                return (BinaryDecoder<T>)(object)new Int32BinaryDecoder();
            }

            if (runtimeType == typeof(uint))
            {
                return (BinaryDecoder<T>)(object)new UInt32BinaryDecoder();
            }

            if (runtimeType == typeof(long))
            {
                return (BinaryDecoder<T>)(object)new Int64BinaryDecoder();
            }

            if (runtimeType == typeof(ulong))
            {
                return (BinaryDecoder<T>)(object)new UInt64BinaryDecoder();
            }

            if (runtimeType == typeof(float))
            {
                return (BinaryDecoder<T>)(object)new SingleBinaryDecoder();
            }

            if (runtimeType == typeof(double))
            {
                return (BinaryDecoder<T>)(object)new DoubleBinaryDecoder();
            }

            if (runtimeType == typeof(APWorld.Item))
            {
                return (BinaryDecoder<T>)(object)new APItemBinaryDecoder();
            }

            if (runtimeType == typeof(APWorld.Location))
            {
                return (BinaryDecoder<T>)(object)new APLocationBinaryDecoder();
            }

            if (runtimeType == typeof(string))
            {
                return (BinaryDecoder<T>)(object)new StringBinaryDecoder();
            }

            return new ObjectBinaryDecoder<T>();
        }

        public abstract T Decode(byte[] bytes);
    }

    internal abstract class BitConverterBinaryDecoder<T> : BinaryDecoder<T>
    {
        public override T Decode(byte[] bytes)
        {
            // ensure bytes are in little-endian order
            if (!BitConverter.IsLittleEndian)
            {
                var oldBytes = bytes;
                bytes = new byte[oldBytes.Length];
                oldBytes.CopyTo(bytes, 0);
                Array.Reverse(bytes);
            }

            return FromBytes(bytes);
        }

        protected abstract T FromBytes(byte[] bytes);
    }

    internal class BooleanBinaryDecoder : BitConverterBinaryDecoder<bool>
    {
        protected override bool FromBytes(byte[] bytes) => BitConverter.ToBoolean(bytes, 0);
    }

    internal class CharBinaryDecoder : BitConverterBinaryDecoder<char>
    {
        protected override char FromBytes(byte[] bytes) => BitConverter.ToChar(bytes, 0);
    }

    internal class SByteBinaryDecoder : BinaryDecoder<sbyte>
    {
        public override sbyte Decode(byte[] bytes) => (sbyte)bytes[0];
    }

    internal class ByteBinaryDecoder : BinaryDecoder<byte>
    {
        public override byte Decode(byte[] bytes) => bytes[0];
    }

    internal class Int16BinaryDecoder : BitConverterBinaryDecoder<short>
    {
        protected override short FromBytes(byte[] bytes) => BitConverter.ToInt16(bytes, 0);
    }

    internal class UInt16BinaryDecoder : BitConverterBinaryDecoder<ushort>
    {
        protected override ushort FromBytes(byte[] bytes) => BitConverter.ToUInt16(bytes, 0);
    }

    internal class Int32BinaryDecoder : BitConverterBinaryDecoder<int>
    {
        protected override int FromBytes(byte[] bytes) => BitConverter.ToInt32(bytes, 0);
    }

    internal class UInt32BinaryDecoder : BitConverterBinaryDecoder<uint>
    {
        protected override uint FromBytes(byte[] bytes) => BitConverter.ToUInt32(bytes, 0);
    }

    internal class Int64BinaryDecoder : BitConverterBinaryDecoder<long>
    {
        protected override long FromBytes(byte[] bytes) => BitConverter.ToInt64(bytes, 0);
    }

    internal class UInt64BinaryDecoder : BitConverterBinaryDecoder<ulong>
    {
        protected override ulong FromBytes(byte[] bytes) => BitConverter.ToUInt64(bytes, 0);
    }

    internal class SingleBinaryDecoder : BitConverterBinaryDecoder<float>
    {
        protected override float FromBytes(byte[] bytes) => BitConverter.ToSingle(bytes, 0);
    }

    internal class DoubleBinaryDecoder : BitConverterBinaryDecoder<double>
    {
        protected override double FromBytes(byte[] bytes) => BitConverter.ToDouble(bytes, 0);
    }

    internal class APItemBinaryDecoder : BinaryDecoder<APWorld.Item>
    {
        public override APWorld.Item Decode(byte[] bytes) => APWorld.Item.ById[BinaryDecoder<long>.Default.Decode(bytes)];
    }

    internal class APLocationBinaryDecoder : BinaryDecoder<APWorld.Location>
    {
        public override APWorld.Location Decode(byte[] bytes) => APWorld.Location.ById[BinaryDecoder<long>.Default.Decode(bytes)];
    }

    internal class StringBinaryDecoder : BinaryDecoder<string>
    {
        public override string Decode(byte[] bytes) => Encoding.UTF8.GetString(bytes);
    }

    internal class ObjectBinaryDecoder<T> : BinaryDecoder<T>
    {
        public override T Decode(byte[] bytes) => throw new NotImplementedException();
    }
}
