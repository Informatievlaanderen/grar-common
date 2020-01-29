namespace Be.Vlaanderen.Basisregisters.GrAr.Legacy.SpatialTools
{
    public class GeometryCoordinateValue
    {
        private readonly double _value;
        public GeometryCoordinateValue(double value) => _value = value;

        public static GeometryCoordinateValue? TryParse(string jsonValue)
            => double.TryParse(jsonValue, out var value)
                ? new GeometryCoordinateValue(value)
                : null;

        public override string ToString() => _value.ToString("F11");

        public override bool Equals(object? obj)
            => obj switch
            {
                GeometryCoordinateValue value => _value.Equals(value._value),
                double d => _value.Equals(d),
                _ => false
            };

        private bool Equals(GeometryCoordinateValue other) => _value.Equals(other._value);

        public override int GetHashCode() => _value.GetHashCode();
    }

    public static class GeometryCoordinateValueExtensions
    {
        public static string ToGeometryCoordinateValueFormat(this double value) => new GeometryCoordinateValue(value).ToString();
    }
}
