using System;
using System.Collections.Generic;
using System.Linq;

namespace Mbs.Api.Extensions.Swagger
{
    /// <summary>
    /// An api version attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    internal class ApiVersionAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApiVersionAttribute"/> class.
        /// </summary>
        /// <param name="version">The version.</param>
        public ApiVersionAttribute(string version)
        {
            Versions = new List<string> { version };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiVersionAttribute"/> class.
        /// </summary>
        /// <param name="versions">The multiple versions.</param>
        public ApiVersionAttribute(params string[] versions)
        {
            Versions = versions.ToList();
        }

        /// <summary>
        /// Gets the versions.
        /// </summary>
        public List<string> Versions { get; }
    }
}
