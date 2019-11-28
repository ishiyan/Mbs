using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Mbs.Api.Converters
{
    /// <inheritdoc />
    public class TimeSpanJsonConverter : JsonConverter<TimeSpan>
    {
        /// <summary>
        /// This specifier is not culture-sensitive. It takes the form [-][d'.']hh':'mm':'ss['.'fffffff].
        /// </summary>
        private const string Format = "c";

        /// <inheritdoc />
        public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value=reader.GetString();
            return TimeSpan.ParseExact(value, Format, CultureInfo.InvariantCulture);
        }

        /// <inheritdoc />
        public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(Format));
        }
    }
}
