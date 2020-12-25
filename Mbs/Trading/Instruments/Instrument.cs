using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Mbs.Trading.Currencies;
using Mbs.Trading.Instruments.Groups;
using Mbs.Trading.Markets;
using Mbs.Trading.Time.Conventions;

namespace Mbs.Trading.Instruments
{
    /// <summary>
    /// Contains all the fields commonly used to describe an instrument.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Modeled after FIX instrument component block.
    /// See http://fiximate.fixtrading.org/latestEP/.
    /// </para>
    /// </remarks>
    public class Instrument : IValidatableObject
    {
        private const string FieldIsRequired = "The field {0} is required.";

        /// <summary>
        /// Initializes a new instance of the <see cref="Instrument"/> class.
        /// </summary>
        public Instrument()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Instrument"/> class.
        /// </summary>
        /// <param name="symbol">The symbol (ticker).</param>
        public Instrument(string symbol)
        {
            Symbol = symbol;
            SetSecurityAlternateIdAs(InstrumentSecurityIdSource.ExchangeSymbol, symbol);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Instrument"/> class.
        /// </summary>
        /// <param name="symbol">The symbol (ticker).</param>
        /// <param name="mic">The ISO 10383 Market Identifier Code (<see cref="ExchangeMic">MIC</see>).</param>
        /// <param name="isin">The International Securities Identifying Number (ISIN).</param>
        public Instrument(string symbol, ExchangeMic mic, string isin)
            : this(symbol)
        {
            Exchange = new Exchange(mic);
            SetSecurityIdAs(InstrumentSecurityIdSource.Isin, isin);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Instrument"/> class.
        /// Use this constructor for Euronext instruments.
        /// </summary>
        /// <param name="symbol">The symbol (ticker).</param>
        /// <param name="euronextMic">The Euronext <see cref="ExchangeMic">MIC</see>.</param>
        /// <param name="isin">The International Securities Identifying Number (ISIN).</param>
        /// <param name="type">The type of the instrument.</param>
        public Instrument(string symbol, EuronextMic euronextMic, string isin, InstrumentType type)
            : this(symbol)
        {
            Exchange = new Exchange(euronextMic);
            SetSecurityIdAs(InstrumentSecurityIdSource.Isin, isin);
            Type = type;
        }

        /// <summary>
        /// Gets or sets an optional security identifier value of the <see cref="SecurityIdSource"/> type.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Takes precedence in identifying security to counterparty over <see cref="SecurityAlternateIdGroup"/> block.
        /// Requires <see cref="SecurityIdSource"/> if specified.
        /// </para>
        /// </remarks>
        [DataMember(IsRequired = true)]
        [Required(ErrorMessage = FieldIsRequired, AllowEmptyStrings = false)]
        [DefaultValue("xxx")]
        public string SecurityId { get; set; } = "xxx";

        /// <summary>
        /// Gets or sets an optional source of the <see cref="SecurityId"/> value.
        /// Conditionally required when <see cref="SecurityId"/> is specified.
        /// </summary>
        [DataMember(IsRequired = true)]
        [Required(ErrorMessage = FieldIsRequired, AllowEmptyStrings = false)]
        [DefaultValue(InstrumentSecurityIdSource.Isin)]
        public InstrumentSecurityIdSource SecurityIdSource { get; set; } = InstrumentSecurityIdSource.Isin;

        /// <summary>
        /// Gets a collection of alternate security identifiers.
        /// </summary>
        [DataMember(IsRequired = false)]
        public InstrumentSecurityAlternateIdGroup SecurityAlternateIdGroup { get; } = new InstrumentSecurityAlternateIdGroup();

        /// <summary>
        /// Gets or sets the instrument type.
        /// </summary>
        [DataMember(IsRequired = false)]
        [DefaultValue(InstrumentType.Undefined)]
        public InstrumentType Type { get; set; } = InstrumentType.Undefined;

        /// <summary>
        /// Gets or sets the type of security using ISO 10962 standard, Classification of Financial Instruments
        /// (CFI code) values.
        /// <para/>
        /// FIX field: CFICode.
        /// </summary>
        /// <remarks>
        /// <para>
        /// See "Appendix 6-B FIX Fields Based Upon Other Standards".
        /// </para>
        /// <para>
        /// A subset of possible values applicable to FIX usage are identified in
        /// "Appendix 6-D CFICode Usage - ISO 10962 Classification of Financial Instruments (CFI code)".
        /// </para>
        /// <para>
        /// See http://www.onixs.biz/fixdictionary/4.4/app_6_d.html,
        /// http://www.dicharry.info/cficode/cficode.html.
        /// </para>
        /// </remarks>
        [DataMember(IsRequired = false)]
        public string Cfi { get; set; }

        /// <summary>
        /// Gets or sets the optional symbol (ticker) of the security.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Use "[N/A]" for products which do not have a symbol.
        /// </para>
        /// <para>
        /// The <see cref="SecurityId"/> can be specified if no symbol exists
        /// (e.g. non-exchange traded Collective Investment Vehicles).
        /// </para>
        /// </remarks>
        [DataMember(IsRequired = false)]
        public string Symbol { get; set; }

        /// <summary>
        /// Gets or sets a name of the instrument.
        /// </summary>
        [DataMember(IsRequired = false)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets an optional textual description for the instrument.
        /// </summary>
        [DataMember(IsRequired = false)]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets a security exchange for the instrument.
        /// </summary>
        [DataMember(IsRequired = true)]
        [Required(ErrorMessage = FieldIsRequired, AllowEmptyStrings = false)]
        public Exchange Exchange { get; set; } = new Exchange(ExchangeMic.Xxxx);

        /// <summary>
        /// Gets or sets a currency code of the instrument.
        /// </summary>
        [DataMember(IsRequired = false)]
        public CurrencyCode Currency { get; set; } = CurrencyCode.Xxx;

        /// <summary>
        /// Gets or sets an exchange holiday schedule.
        /// </summary>
        [DataMember(IsRequired = false)]
        public BusinessDayCalendar BusinessDayCalendar { get; set; } = BusinessDayCalendar.WeekendsOnly;

        /// <summary>
        /// Gets or sets the current state of the instrument>.
        /// </summary>
        [DataMember(IsRequired = false)]
        public InstrumentSecurityStatus SecurityStatus { get; set; } = InstrumentSecurityStatus.Active;

        /// <summary>
        /// Gets or sets the initial margin of the instrument.
        /// </summary>
        [DataMember(IsRequired = false)]
        public double Margin { get; set; }

        /// <summary>
        /// Gets or sets the contract value factor by which price must be adjusted to determine the true nominal value of a contract.
        /// Alternative names are contract multiplier, price multiplier.
        /// </summary>
        /// <remarks>
        /// <para>
        /// For Fixed Income:
        /// </para>
        /// <para>
        /// Amortization factor for deriving Current face from Original face for ABS or MBS securities,
        /// note the fraction may be greater than, equal to or less than.
        /// </para>
        /// <para>
        /// <c>Qty * Factor * Price = Gross Trade Amount</c>.
        /// </para>
        /// <para>
        /// For Derivatives:
        /// </para>
        /// <para>
        /// Contract value factor by which price must be adjusted to determine the true nominal value
        /// of one futures/options contract.
        /// </para>
        /// <para>
        /// <c>(Qty * Price) * Factor = Nominal Value</c>.
        /// </para>
        /// </remarks>
        [DataMember(IsRequired = false)]
        public double? Factor { get; set; }

        /// <summary>
        /// Gets or sets the minimum price increment. Could also be used to represent tick value.
        /// </summary>
        [DataMember(IsRequired = false)]
        public double? MinPriceIncrement { get; set; }

        /// <summary>
        /// Gets or sets the minimum price increment amount associated with the <see cref="MinPriceIncrementAmount"/>.
        /// </summary>
        [DataMember(IsRequired = false)]
        public double? MinPriceIncrementAmount { get; set; }

        /// <summary>
        /// Gets or sets the position limit for the instrument.
        /// </summary>
        [DataMember(IsRequired = false)]
        public int? PositionLimit { get; set; }

        /// <summary>
        /// Gets or sets the number of decimal places for the instrument prices.
        /// </summary>
        [DataMember(IsRequired = false)]
        public int? PricePrecision { get; set; }

        /// <summary>
        /// Gets the <see cref="SecurityId"/> if it has a given <see cref="SecurityIdSource"/>.
        /// </summary>
        /// <param name="source">The security id source.</param>
        /// <param name="checkAlternativeIdGroup">If true, check also the <see cref="SecurityAlternateIdGroup"/>.</param>
        /// <returns>The security id or null if the source is different.</returns>
        public string GetSecurityIdAs(InstrumentSecurityIdSource source, bool checkAlternativeIdGroup = true)
        {
            var id = source == SecurityIdSource
                ? SecurityId
                : null;

            return id != null || !checkAlternativeIdGroup || SecurityAlternateIdGroup.Count <= 0
                ? id
                : SecurityAlternateIdGroup.Find(source)?.SecurityAlternateId;
        }

        /// <summary>
        /// Sets the <see cref="SecurityId"/> of the given <see cref="SecurityIdSource"/>.
        /// </summary>
        /// <param name="source">The security id source.</param>
        /// <param name="value">The security id value.</param>
        public void SetSecurityIdAs(InstrumentSecurityIdSource source, string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException(nameof(value));
            }

            SecurityIdSource = source;
            SecurityId = value;
        }

        /// <summary>
        /// Sets the alternate <see cref="SecurityId"/> of the given <see cref="SecurityIdSource"/>
        /// to the <see cref="SecurityAlternateIdGroup"/>.
        /// </summary>
        /// <param name="source">The security id source.</param>
        /// <param name="value">The security id value.</param>
        public void SetSecurityAlternateIdAs(InstrumentSecurityIdSource source, string value)
        {
            var alternateId = SecurityAlternateIdGroup.Find(source);
            if (alternateId != null)
            {
                alternateId.SecurityAlternateId = value;
            }
            else
            {
                SecurityAlternateIdGroup.Add(value, source);
            }
        }

        /// <inheritdoc />
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }
}
