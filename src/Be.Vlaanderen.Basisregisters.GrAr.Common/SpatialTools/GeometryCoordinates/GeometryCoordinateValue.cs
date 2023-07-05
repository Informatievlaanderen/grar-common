namespace Be.Vlaanderen.Basisregisters.GrAr.Common.SpatialTools.GeometryCoordinates
{
    using System;
    using System.Globalization;

    public abstract class GeometryCoordinateValue
    {
        private readonly double _value;
        private readonly string _format;

        protected GeometryCoordinateValue(double value, string format)
        {
            _value = value;
            _format = format;
        }

        public override string ToString() => _value.ToString(_format, CultureInfo.InvariantCulture);

        public override bool Equals(object? obj)
            => obj switch
            {
                GeometryCoordinateValue value => _value.Equals(value._value),
                double d => _value.Equals(d),
                _ => false
            };

        public override int GetHashCode() => _value.GetHashCode();

        protected static T? TryParse<T>(string jsonValue, Func<double, T> ctorFunc) where T : GeometryCoordinateValue
            => double.TryParse(jsonValue, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out var value)
                ? ctorFunc(value)
                : null;
    }

    public class PolygonGeometryCoordinateValue : GeometryCoordinateValue
    {
        public PolygonGeometryCoordinateValue(double value) : base(value, "F11") { }

        public static PolygonGeometryCoordinateValue? TryParse(string jsonValue)
            => TryParse(jsonValue, value => new PolygonGeometryCoordinateValue(value));
    }

    public class LineStringGeometryCoordinateValue : GeometryCoordinateValue
    {
        public LineStringGeometryCoordinateValue(double value) : base(value, "F11") { }

        public static LineStringGeometryCoordinateValue? TryParse(string jsonValue)
            => TryParse(jsonValue, value => new LineStringGeometryCoordinateValue(value));
    }

    public class PointGeometryCoordinateValue : GeometryCoordinateValue
    {
        public PointGeometryCoordinateValue(double value) : base(value, "F2") { }

        public static PointGeometryCoordinateValue? TryParse(string jsonValue)
            => TryParse(jsonValue, value => new PointGeometryCoordinateValue(value));
    }

    public static class GeometryCoordinateValueExtensions
    {
        public static string ToPolygonGeometryCoordinateValueFormat(this double value) => new PolygonGeometryCoordinateValue(value).ToString();
        public static string ToPointGeometryCoordinateValueFormat(this double value) => new PointGeometryCoordinateValue(value).ToString();
    }
}
