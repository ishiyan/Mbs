using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Mbs.Trading.Time;

namespace Mbs.Api.Converters.Trading.Time
{
    /// <inheritdoc />
    public class TimeGranularityJsonConverter : JsonConverter<TimeGranularity>
    {
        /// <summary>
        /// This specifier is not culture-sensitive. It takes the form [-][d'.']hh':'mm':'ss['.'fffffff].
        /// </summary>
        private const string Format = "c";

        /// <inheritdoc />
        public override TimeGranularity Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString().ToLowerInvariant();
            return value switch
            {
                "aperiodic" => TimeGranularity.Aperiodic,
                "day1" => TimeGranularity.Day1,
                _ => throw new ArgumentException($"Unknown time granularity: {value}")
            };
        }

        /// <inheritdoc />
        public override void Write(Utf8JsonWriter writer, TimeGranularity value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(Format));
        }
    }
}
