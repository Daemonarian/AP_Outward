using OutwardArchipelago.Utils;

namespace OutwardArchipelago
{
    /// <summary>
    /// Utility functions for managing extra data about Outward items that are saved and synced.
    /// </summary>
    internal static class OutwardItemSideData
    {
        internal const string KEY_PREFIX = "__side_data_";

        public static void SetSideData<T>(this Item item, string key, T value) => item.m_tempExtraData[SafeKey(key)] = Base64SafeEncoder<T>.Default.Encode(value);

        public static T GetSideData<T>(this Item item, string key) => Base64SafeDecoder<T>.Default.Decode(item.m_tempExtraData[SafeKey(key)]);

        public static bool TryGetSideData<T>(this Item item, string key, out T value)
        {
            if (item.m_tempExtraData.TryGetValue(SafeKey(key), out var valueString))
            {
                value = Base64SafeDecoder<T>.Default.Decode(valueString);
                return true;
            }

            value = default;
            return false;
        }

        private static string SafeKey(string key) => $"{KEY_PREFIX}{Base64SafeEncoder<string>.Default.Encode(key)}";
    }
}
