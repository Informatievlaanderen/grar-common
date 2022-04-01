namespace Be.Vlaanderen.Basisregisters.GrAr.Common
{
    using System.Globalization;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;

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

        public static string SanitizeForBosaSearch(this string input)
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
