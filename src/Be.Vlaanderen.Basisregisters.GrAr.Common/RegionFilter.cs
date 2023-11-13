namespace Be.Vlaanderen.Basisregisters.GrAr.Common
{
    using System;
    using System.Linq;

    public static class RegionFilter
    {
        /// <summary>
        /// The first number of a NIS-code represents the province.
        /// This array is a list of the provinces of the Flemish Region and their NIS-code numbers.
        /// 1 = Antwerpen
        /// 3 = West-Vlaanderen
        /// 4 = Oost-Vlaanderen
        /// 7 = Limburg
        /// 23, 24 = Vlaams-Brabant (we can't use just 2, because that also covers Waals-Brabant)
        /// </summary>
        private static readonly string[] flemishRegionNiscodes =
        {
            "1",
            "3",
            "4",
            "7",
            "23",
            "24"
        };

        /// <summary>
        /// Determines whether the provided <paramref name="nisCode"/> represents a municipality in the Flemish Region.
        /// </summary>
        /// <param name="nisCode">The NIS-code.</param>
        /// <returns>True if the NIS-code is in the Flemish Region, otherwise false.</returns>
        public static bool IsFlemishRegion(string nisCode)
            => flemishRegionNiscodes.Any(n => nisCode.StartsWith(n, StringComparison.OrdinalIgnoreCase));
    }
}
