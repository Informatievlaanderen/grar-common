namespace Be.Vlaanderen.Basisregisters.GrAr.Common
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using Utilities.HexByteConvertor;

    public static class StringExtensions
    {
        public static string? RemoveDiacritics(this string? input, bool lowerCaseString = true)
        {
            if (input == null)
                return null;

            var stringBuilder = new StringBuilder(input.Length);
            var normalizedString = input.Normalize(NormalizationForm.FormD);

            foreach (var character in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(character);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                    stringBuilder.Append(character);
            }

            var resultString = stringBuilder.ToString();

            if (lowerCaseString)
                resultString = resultString.ToLower();

            return resultString.Normalize(NormalizationForm.FormC);
        }

        public static string? SanitizeForBosaSearch(this string? input)
        {
            if (input == null)
                return input;

            input = input
                .Replace("'", string.Empty)
                .Replace("-", string.Empty)
                .Replace(" ", string.Empty);

            return input.RemoveDiacritics();
        }

        public static string ToEventHash(this IHaveHashFields haveHashFields, params string[] extraValues)
        {
            const string hashSeparator = "þ";

            var valuesToHash = extraValues.Union(haveHashFields.GetHashFields());
            var value = string.Join(hashSeparator, valuesToHash);

            using SHA512 sha512Managed = new SHA512Managed();
            var hashedBytes = sha512Managed.ComputeHash(Encoding.UTF8.GetBytes(value));

            return GetStringFromHash(hashedBytes);
        }

        public static bool IsHexByteArray(this string input)
        {
            if (string.IsNullOrWhiteSpace(input) || input.Length % 2 != 0)
                return false;

            return input.All(c => (c >= '0' && c <= '9') || (c >= 'a' && c <= 'f') || (c >= 'A' && c <= 'F'));
        }

        /// <summary>
        /// Tries to interpret the string as a hex-encoded EWKB geometry and returns its SRID.
        /// Returns null if the string is not a valid hex byte array or not a valid EWKB geometry with an SRID.
        /// </summary>
        public static bool TryReadSrid(this string input, out int srid)
        {
            srid = 0;
            if (!input.IsHexByteArray())
                return false;

            try
            {
                var bytes = input.ToByteArray();
                if (bytes is null || bytes.Length < 9)
                    return false;

                // EWKB structure:
                // byte 0    : byte order (0x00 = big endian, 0x01 = little endian)
                // bytes 1-4 : geometry type (uint32), SRID flag is 0x20000000
                // bytes 5-8 : SRID (uint32), only present when SRID flag is set
                var isLittleEndian = bytes[0] == 0x01;

                uint geometryType;
                if (isLittleEndian)
                    geometryType = BitConverter.ToUInt32(bytes, 1);
                else
                    geometryType = (uint)(bytes[1] << 24 | bytes[2] << 16 | bytes[3] << 8 | bytes[4]);

                const uint sridFlag = 0x20000000;
                if ((geometryType & sridFlag) == 0)
                    return false;

                if (isLittleEndian)
                    srid = (int)BitConverter.ToUInt32(bytes, 5);
                else
                    srid = bytes[5] << 24 | bytes[6] << 16 | bytes[7] << 8 | bytes[8];

                return true;
            }
            catch
            {
                return false;
            }
        }

        private static string GetStringFromHash(byte[] hash)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString("X2"));
            }
            return result.ToString();
        }
    }
}
