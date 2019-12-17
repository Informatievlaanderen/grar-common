namespace Be.Vlaanderen.Basisregisters.GrAr.Common
{
    using System.Globalization;
    using System.Text;

    public static class StringExtensions
    {
        public static string? RemoveDiacritics(this string? input, bool lowerCaseString = true)
        {
            if (input == null)
                return null;

            var stringBuilder = new StringBuilder();
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
    }
}
