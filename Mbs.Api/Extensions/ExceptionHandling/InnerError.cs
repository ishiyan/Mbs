using System.Text.Json.Serialization;

namespace Mbs.Api.Extensions.ExceptionHandling
{
    /// <summary>
    /// The recursive inner error.
    /// </summary>
    public sealed class InnerError
    {
        /// <summary>
        /// Gets or sets the message of an inner error.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets inner errors of this inner error.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public InnerError Details { get; set; }
    }
}
