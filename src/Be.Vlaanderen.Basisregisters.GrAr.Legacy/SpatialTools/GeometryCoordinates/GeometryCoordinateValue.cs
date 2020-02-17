namespace Be.Vlaanderen.Basisregisters.GrAr.Legacy.SpatialTools
{
    using System.Globalization;

    public class GeometryCoordinateValue
    {
        private readonly double _value;
        public GeometryCoordinateValue(double value) => _value = value;

        public static GeometryCoordinateValue? TryParse(string jsonValue)
            => double.TryParse(jsonValue, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out var value)
                ? new GeometryCoordinateValue(value)
                : null;

        public override string ToString() => _value.ToString("F11", CultureInfo.InvariantCulture);

        public override bool Equals(object? obj)
            => obj switch
            {
                GeometryCoordinateValue value => _value.Equals(value._value),
                double d => _value.Equals(d),
                _ => false
            };

        public override int GetHashCode() => _value.GetHashCode();
    }

    public static class GeometryCoordinateValueExtensions
    {
        public static string ToGeometryCoordinateValueFormat(this double value) => new GeometryCoordinateValue(value).ToString();
    }
}
