using System;
using System.Text;

namespace OutwardArchipelago
{
    internal static class ItemSideData
    {
        internal const string KEY_PREFIX = "__side_data_";

        internal static void SetSideData(this Item item, string key, byte[] value) => SetSideData(item, key, value, x => x);
        internal static void SetSideData(this Item item, string key, bool value) => SetSideData(item, key, value, ByteEncoding.Encode);
        internal static void SetSideData(this Item item, string key, char value) => SetSideData(item, key, value, ByteEncoding.Encode);
        internal static void SetSideData(this Item item, string key, double value) => SetSideData(item, key, value, ByteEncoding.Encode);
        internal static void SetSideData(this Item item, string key, float value) => SetSideData(item, key, value, ByteEncoding.Encode);
        internal static void SetSideData(this Item item, string key, int value) => SetSideData(item, key, value, ByteEncoding.Encode);
        internal static void SetSideData(this Item item, string key, long value) => SetSideData(item, key, value, ByteEncoding.Encode);
        internal static void SetSideData(this Item item, string key, short value) => SetSideData(item, key, value, ByteEncoding.Encode);
        internal static void SetSideData(this Item item, string key, uint value) => SetSideData(item, key, value, ByteEncoding.Encode);
        internal static void SetSideData(this Item item, string key, ulong value) => SetSideData(item, key, value, ByteEncoding.Encode);
        internal static void SetSideData(this Item item, string key, ushort value) => SetSideData(item, key, value, ByteEncoding.Encode);
        internal static void SetSideData(this Item item, string key, string value) => SetSideData(item, key, value, ByteEncoding.Encode);

        internal static byte[] GetSideDataAsBytes(this Item item, string key) => GetSideData(item, key, x => x);
        internal static bool GetSideDataAsBool(this Item item, string key) => GetSideData(item, key, ByteEncoding.DecodeAsBool);
        internal static char GetSideDataAsChar(this Item item, string key) => GetSideData(item, key, ByteEncoding.DecodeAsChar);
        internal static double GetSideDataAsDouble(this Item item, string key) => GetSideData(item, key, ByteEncoding.DecodeAsDouble);
        internal static float GetSideDataAsFloat(this Item item, string key) => GetSideData(item, key, ByteEncoding.DecodeAsFloat);
        internal static int GetSideDataAsInt(this Item item, string key) => GetSideData(item, key, ByteEncoding.DecodeAsInt);
        internal static long GetSideDataAsLong(this Item item, string key) => GetSideData(item, key, ByteEncoding.DecodeAsLong);
        internal static short GetSideDataAsShort(this Item item, string key) => GetSideData(item, key, ByteEncoding.DecodeAsShort);
        internal static uint GetSideDataAsUint(this Item item, string key) => GetSideData(item, key, ByteEncoding.DecodeAsUint);
        internal static ulong GetSideDataAsUlong(this Item item, string key) => GetSideData(item, key, ByteEncoding.DecodeAsUlong);
        internal static ushort GetSideDataAsUshot(this Item item, string key) => GetSideData(item, key, ByteEncoding.DecodeAsUshort);
        internal static string GetSideDataAsString(this Item item, string key) => GetSideData(item, key, ByteEncoding.DecodeAsString);

        internal static bool TryGetSideData(this Item item, string key, out byte[] value) => TryGetSideData(item, key, out value, x => x);
        internal static bool TryGetSideData(this Item item, string key, out bool value) => TryGetSideData(item, key, out value, ByteEncoding.DecodeAsBool);
        internal static bool TryGetSideData(this Item item, string key, out char value) => TryGetSideData(item, key, out value, ByteEncoding.DecodeAsChar);
        internal static bool TryGetSideData(this Item item, string key, out double value) => TryGetSideData(item, key, out value, ByteEncoding.DecodeAsDouble);
        internal static bool TryGetSideData(this Item item, string key, out float value) => TryGetSideData(item, key, out value, ByteEncoding.DecodeAsFloat);
        internal static bool TryGetSideData(this Item item, string key, out int value) => TryGetSideData(item, key, out value, ByteEncoding.DecodeAsInt);
        internal static bool TryGetSideData(this Item item, string key, out long value) => TryGetSideData(item, key, out value, ByteEncoding.DecodeAsLong);
        internal static bool TryGetSideData(this Item item, string key, out short value) => TryGetSideData(item, key, out value, ByteEncoding.DecodeAsShort);
        internal static bool TryGetSideData(this Item item, string key, out uint value) => TryGetSideData(item, key, out value, ByteEncoding.DecodeAsUint);
        internal static bool TryGetSideData(this Item item, string key, out ulong value) => TryGetSideData(item, key, out value, ByteEncoding.DecodeAsUlong);
        internal static bool TryGetSideData(this Item item, string key, out ushort value) => TryGetSideData(item, key, out value, ByteEncoding.DecodeAsUshort);
        internal static bool TryGetSideData(this Item item, string key, out string value) => TryGetSideData(item, key, out value, ByteEncoding.DecodeAsString);

        private static void SetSideData<T>(Item item, string key, T value, Func<T, byte[]> encode) => item.m_tempExtraData[SafeKey(key)] = Base64Safe.Encode(encode(value));

        private static T GetSideData<T>(Item item, string key, Func<byte[], T> decode) => decode(Base64Safe.Decode(item.m_tempExtraData[SafeKey(key)]));

        private static bool TryGetSideData<T>(Item item, string key, out T value, Func<byte[], T> decode)
        {
            if (item.m_tempExtraData.TryGetValue(SafeKey(key), out var valueString))
            {
                value = decode(Base64Safe.Decode(valueString));
                return true;
            }

            value = default;
            return false;
        }

        private static string SafeKey(string key) => $"{KEY_PREFIX}{Base64Safe.Encode(ByteEncoding.Encode(key))}";

        private static class Base64Safe
        {
            internal static string Encode(byte[] input) => Convert.ToBase64String(input, Base64FormattingOptions.None).Replace('+', '-').Replace('/', '_');
            internal static byte[] Decode(string input) => Convert.FromBase64String(input.Replace('-', '+').Replace('_', '/'));
        }

        private static class ByteEncoding
        {
            internal static byte[] Encode(bool input) => FixEndianness(BitConverter.GetBytes(input));
            internal static byte[] Encode(char input) => FixEndianness(BitConverter.GetBytes(input));
            internal static byte[] Encode(double input) => FixEndianness(BitConverter.GetBytes(input));
            internal static byte[] Encode(float input) => FixEndianness(BitConverter.GetBytes(input));
            internal static byte[] Encode(int input) => FixEndianness(BitConverter.GetBytes(input));
            internal static byte[] Encode(long input) => FixEndianness(BitConverter.GetBytes(input));
            internal static byte[] Encode(short input) => FixEndianness(BitConverter.GetBytes(input));
            internal static byte[] Encode(uint input) => FixEndianness(BitConverter.GetBytes(input));
            internal static byte[] Encode(ulong input) => FixEndianness(BitConverter.GetBytes(input));
            internal static byte[] Encode(ushort input) => FixEndianness(BitConverter.GetBytes(input));
            internal static byte[] Encode(string input) => Encoding.UTF8.GetBytes(input);

            internal static bool DecodeAsBool(byte[] input) => BitConverter.ToBoolean(FixEndianness(input), 0);
            internal static char DecodeAsChar(byte[] input) => BitConverter.ToChar(FixEndianness(input), 0);
            internal static double DecodeAsDouble(byte[] input) => BitConverter.ToDouble(FixEndianness(input), 0);
            internal static float DecodeAsFloat(byte[] input) => BitConverter.ToSingle(FixEndianness(input), 0);
            internal static int DecodeAsInt(byte[] input) => BitConverter.ToInt32(FixEndianness(input), 0);
            internal static long DecodeAsLong(byte[] input) => BitConverter.ToInt64(FixEndianness(input), 0);
            internal static short DecodeAsShort(byte[] input) => BitConverter.ToInt16(FixEndianness(input), 0);
            internal static uint DecodeAsUint(byte[] input) => BitConverter.ToUInt32(FixEndianness(input), 0);
            internal static ulong DecodeAsUlong(byte[] input) => BitConverter.ToUInt64(FixEndianness(input), 0);
            internal static ushort DecodeAsUshort(byte[] input) => BitConverter.ToUInt16(FixEndianness(input), 0);
            internal static string DecodeAsString(byte[] bytes) => Encoding.UTF8.GetString(bytes);

            private static byte[] FixEndianness(byte[] input)
            {
                if (!BitConverter.IsLittleEndian)
                {
                    Array.Reverse(input);
                }

                return input;
            }
        }
    }
}
