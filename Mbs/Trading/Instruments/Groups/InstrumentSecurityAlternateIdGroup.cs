using System;
using System.Collections.Generic;

namespace Mbs.Trading.Instruments.Groups
{
    /// <summary>
    /// Contains a collection of alternate security identifiers.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Modeled after FIX SecAltIDGrp component.
    /// </para>
    /// <para>
    /// See http://fiximate.fixtrading.org/latestEP/.
    /// </para>
    /// </remarks>
    public sealed class InstrumentSecurityAlternateIdGroup
    {
        private readonly List<InstrumentSecurityAlternateId> list = new List<InstrumentSecurityAlternateId>();

        /// <summary>
        /// Gets a number of <see cref="InstrumentSecurityAlternateId"/> entries.
        /// </summary>
        public int Count => list.Count;

        /// <summary>
        /// Gets a collection of security alternate id entries.
        /// </summary>
        public IReadOnlyCollection<InstrumentSecurityAlternateId> Collection => list.AsReadOnly();

        /// <summary>
        /// Finds the first occurrence of the given <paramref name="source"/> id.
        /// </summary>
        /// <param name="source">The source id to find.</param>
        /// <returns>The first occurrence or <c>null</c> if not found.</returns>
        public InstrumentSecurityAlternateId Find(InstrumentSecurityIdSource source)
        {
            foreach (var alternativeId in list)
            {
                if (alternativeId.SecurityAlternateIdSource == source)
                {
                    return alternativeId;
                }
            }

            return null;
        }

        /// <summary>
        /// Adds a new security alternate id to the group or updates an existing one.
        /// </summary>
        /// <param name="value">A value of the security alternate id.</param>
        /// <param name="source">A source of the security alternate id.</param>
        public void Add(string value, InstrumentSecurityIdSource source)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException(nameof(value));
            }

            var alternateId = Find(source);
            if (alternateId != null)
            {
                alternateId.SecurityAlternateId = value;
            }
            else
            {
                list.Add(new InstrumentSecurityAlternateId(value, source));
            }
        }
    }
}
