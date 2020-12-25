using System;

namespace Mbs.Trading.Instruments.Groups
{
    /// <summary>
    /// Contains an alternate security identifier value of the specified source.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Modeled after FIX SecAltIDGrp component.
    /// </para>
    /// <para>
    /// See http://fiximate.fixtrading.org/latestEP/.
    /// </para>
    /// </remarks>
    public sealed class InstrumentSecurityAlternateId
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InstrumentSecurityAlternateId"/> class.
        /// </summary>
        /// <param name="value">A value of the security alternate id.</param>
        /// <param name="source">A source of the security alternate id.</param>
        internal InstrumentSecurityAlternateId(string value, InstrumentSecurityIdSource source)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException(nameof(value));
            }

            SecurityAlternateId = value;
            SecurityAlternateIdSource = source;
        }

        /// <summary>
        /// Gets an alternate security identifier value of the <see cref="SecurityAlternateIdSource"/> type.
        /// </summary>
        public string SecurityAlternateId { get; internal set; }

        /// <summary>
        /// Gets the source of the <see cref="SecurityAlternateId"/> value.
        /// </summary>
        public InstrumentSecurityIdSource SecurityAlternateIdSource { get; }
    }
}
