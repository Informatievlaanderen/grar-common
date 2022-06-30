namespace Be.Vlaanderen.Basisregisters.GrAr.Edit.Validators
{
    using System;
    using Common.Oslo.Extensions;

    public static class OsloPuriValidator
    {
        public static bool TryParseIdentifier(string url, out string identifier)
        {
            identifier = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(url))
                {
                    return false;
                }

                identifier = url
                    .AsIdentifier()
                    .Map(x => x);

                return true;
            }
            catch (UriFormatException)
            {
                return false;
            }
        }
    }
}
