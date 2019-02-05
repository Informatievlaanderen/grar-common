namespace Be.Vlaanderen.Basisregisters.GrAr.Legacy
{
    using Newtonsoft.Json;
    using System;
    using System.Globalization;
    using System.Xml;
    using System.Xml.Schema;
    using System.Xml.Serialization;

    /// <remarks>
    /// The default value is <c>DateTimeOffset.MinValue</c>. This is a value
    /// type and has the same hash code as <c>DateTimeOffset</c>! Implicit
    /// assignment from <c>DateTime</c> is neither implemented nor desirable!
    /// </remarks>
    public struct Rfc3339SerializableDateTimeOffset : IXmlSerializable
    {
        private DateTimeOffset _value;

        public Rfc3339SerializableDateTimeOffset(DateTimeOffset value)
            => _value = value;

        public static implicit operator Rfc3339SerializableDateTimeOffset(DateTimeOffset value)
            => new Rfc3339SerializableDateTimeOffset(value);

        public static implicit operator DateTimeOffset(Rfc3339SerializableDateTimeOffset instance)
            => instance._value;

        public static bool operator ==(Rfc3339SerializableDateTimeOffset a, Rfc3339SerializableDateTimeOffset b)
            => a._value == b._value;

        public static bool operator !=(Rfc3339SerializableDateTimeOffset a, Rfc3339SerializableDateTimeOffset b)
            => a._value != b._value;

        public static bool operator <(Rfc3339SerializableDateTimeOffset a, Rfc3339SerializableDateTimeOffset b)
            => a._value < b._value;

        public static bool operator >(Rfc3339SerializableDateTimeOffset a, Rfc3339SerializableDateTimeOffset b)
            => a._value > b._value;

        public override int GetHashCode()
            => _value.GetHashCode();

        public XmlSchema GetSchema()
            => null;

        public override bool Equals(object o)
        {
            switch (o)
            {
                case Rfc3339SerializableDateTimeOffset other:
                    return _value.Equals(other._value);

                case DateTimeOffset other:
                    return _value.Equals(other);

                default:
                    return false;
            }
        }

        public void ReadXml(XmlReader reader)
        {
            var text = reader.ReadElementString();

            _value = DateTimeUtils.TryParseDate(text, out var value)
                ? value.Value
                : throw new FormatException("Invalid datetime format.");
        }

        public void WriteXml(XmlWriter writer)
            => writer.WriteString(DateTimeUtils.ToRfc3339String(_value));

        public override string ToString()
            => DateTimeUtils.ToRfc3339String(_value);

        public string ToString(string format)
            => _value.ToString(format);

        // https://github.com/dotnet/SyndicationFeedReaderWriter/blob/db15b5ea16ed262744784068f026cc8d20868e8e/src/Utils/DateTimeUtils.cs
        // https://stackoverflow.com/questions/522251/whats-the-difference-between-iso-8601-and-rfc-3339-date-formats
        private static class DateTimeUtils
        {
            private const string Rfc3339LocalDateTimeFormat = "yyyy-MM-ddTHH:mm:sszzz";
            private const string Rfc3339UtcDateTimeFormat = "yyyy-MM-ddTHH:mm:ssZ";

            public static bool TryParseDate(string value, out DateTimeOffset? result) =>
                TryParseDateRfc3339(value, out result);

            public static string ToRfc3339String(DateTimeOffset dto)
                => dto.Offset == TimeSpan.Zero
                    ? dto.ToUniversalTime().ToString(Rfc3339UtcDateTimeFormat, CultureInfo.InvariantCulture)
                    : dto.ToString(Rfc3339LocalDateTimeFormat, CultureInfo.InvariantCulture);

            private static bool TryParseDateRfc3339(string dateTimeString, out DateTimeOffset? result)
            {
                dateTimeString = dateTimeString.Trim();

                if (dateTimeString[19] == '.')
                {
                    // remove any fractional seconds, we choose to ignore them
                    var i = 20;
                    while (dateTimeString.Length > i && char.IsDigit(dateTimeString[i]))
                        ++i;

                    dateTimeString = dateTimeString.Substring(0, 19) + dateTimeString.Substring(i);
                }

                if (DateTimeOffset.TryParseExact(dateTimeString, Rfc3339LocalDateTimeFormat,
                    CultureInfo.InvariantCulture.DateTimeFormat,
                    DateTimeStyles.None, out var localTime))
                {
                    result = localTime;
                    return true;
                }

                if (DateTimeOffset.TryParseExact(dateTimeString, Rfc3339UtcDateTimeFormat,
                    CultureInfo.InvariantCulture.DateTimeFormat,
                    DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal, out var utcTime))
                {
                    result = utcTime;
                    return true;
                }

                result = null;
                return false;
            }
        }
    }

    public class Rfc3339SerializableDateTimeOffsetConverter : JsonConverter<Rfc3339SerializableDateTimeOffset>
    {
        public override void WriteJson(JsonWriter writer,
            Rfc3339SerializableDateTimeOffset value,
            JsonSerializer serializer)
        {
            var dateTimeOffset = (DateTimeOffset)value;

            serializer.Serialize(writer, dateTimeOffset);
        }

        public override Rfc3339SerializableDateTimeOffset ReadJson(JsonReader reader,
            Type objectType,
            Rfc3339SerializableDateTimeOffset existingValue,
            bool hasExistingValue,
            JsonSerializer serializer)
        {
            switch (serializer.DateParseHandling)
            {
                case DateParseHandling.None:
                    var parsedDateTimeOffset = DateTimeOffset.Parse((string)reader.Value);
                    return new Rfc3339SerializableDateTimeOffset(parsedDateTimeOffset);
                case DateParseHandling.DateTime:
                    var dateTime = new DateTimeOffset((DateTime)reader.Value);
                    return new Rfc3339SerializableDateTimeOffset(dateTime);
                case DateParseHandling.DateTimeOffset:
                    var dateTimeOffset = (DateTimeOffset)reader.Value;
                    return new Rfc3339SerializableDateTimeOffset(dateTimeOffset);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
