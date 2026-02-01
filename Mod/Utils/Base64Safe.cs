using System;

namespace OutwardArchipelago.Utils
{
    /// <summary>
    /// Saving data to the Outward save file often involves encoding data in Base64Url
    /// to avoid the data containing the expected delimeter characters.
    /// </summary>
    internal static class Base64Safe
    {
        /// <summary>
        /// Encode a byte array as a string using only the characters 'A'-'Z', 'a'-'z', '0'-'9', '-', and '_'.
        /// These characters should be safe to use for values in an Outward save file.
        /// The original input can be obtained by running the output through <see cref="Decode(string)"/>.
        /// </summary>
        /// <param name="input">The byte array to encode.</param>
        /// <returns>The string containing only safe characters.</returns>
        public static string Encode(byte[] input)
        {
            var output = Convert.ToBase64String(input);
            output = output.TrimEnd('='); // we do not want the padding characters
            output = output.Replace('+', '-').Replace('/', '_');
            return output;
        }

        /// <summary>
        /// Decodes a byte array from a string as encoded by <see cref="Encode(byte[])"/>.
        /// </summary>
        /// <param name="input">The encoded string.</param>
        /// <returns>The byte array.</returns>
        public static byte[] Decode(string input)
        {
            input = input.Replace('/', '_').Replace('-', '+');
            int totalWidth = input.Length + ((4 - (input.Length % 4)) % 4); // least multiple of 4 greater than or equal to input.Length
            input = input.PadRight(totalWidth, '='); // pad back out to a multiple of 4
            return Convert.FromBase64String(input);
        }
    }
}
