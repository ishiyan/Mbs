using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;

namespace Mbs.Api.Extensions.ExceptionHandling
{
    /// <summary>
    /// Encapsulates an error.
    /// </summary>
    public sealed class InternalError
    {
        private const string ModelBindingErrorMessage = "Invalid parameters.";

        /// <summary>
        /// Gets or sets the status code of the error.
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// Gets or sets a human-readable representation of the error.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets inner errors of this error.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<InnerError> Details { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="InternalError"/> class.
        /// </summary>
        public InternalError()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InternalError"/> class.
        /// </summary>
        /// <param name="modelState">The state of the model.</param>
        public InternalError(ModelStateDictionary modelState)
        {
            StatusCode = StatusCodes.Status400BadRequest;
            Message = ModelBindingErrorMessage;

            var list = new List<InnerError>();
            foreach (var key in modelState.Keys)
            {
                var value = modelState[key];
                foreach (var error in value.Errors)
                {
                    string err = error.ErrorMessage;
                    if (string.IsNullOrWhiteSpace(err))
                        err = error.Exception?.Message;

                    if (!string.IsNullOrWhiteSpace(err))
                        list.Add(new InnerError { Message = $"{key}: {err}" });
                }
            }

            if (list.Count > 0)
            {
                Details = list.ToArray();
            }
        }
    }
}
