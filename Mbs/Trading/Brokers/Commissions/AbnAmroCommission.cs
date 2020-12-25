using System;
using Mbs.Trading.Currencies;
using Mbs.Trading.Instruments;
using Mbs.Trading.Markets;
using Mbs.Trading.Orders;

namespace Mbs.Trading.Brokers.Commissions
{
    /// <summary>
    /// Implements the AbnAmro 2013 commission strategies as described in
    /// <c>http://www.abnamro.nl/nl/images/Generiek/PDFs/030_Beleggen/Tarieven/Brochure_Tarieven_Beleggen_totaal.pdf</c>.
    /// </summary>
    public sealed class AbnAmroCommission : ICommission
    {
        private readonly AbnAmroBeleggingsvoorm beleggingsvoorm;

        private AbnAmroCommission(AbnAmroBeleggingsvoorm beleggingsvoorm)
        {
            this.beleggingsvoorm = beleggingsvoorm;
        }

        /// <summary>
        /// Gets an <see cref="AbnAmroCommission"/> instance for the beleggingsadvies contract.
        /// </summary>
        public static AbnAmroCommission Beleggingsadvies => new AbnAmroCommission(AbnAmroBeleggingsvoorm.Beleggingsadvies);

        /// <summary>
        /// Gets an <see cref="AbnAmroCommission"/> instance for the direct bellegen contract.
        /// </summary>
        public static AbnAmroCommission DirectBeleggen => new AbnAmroCommission(AbnAmroBeleggingsvoorm.DirectBeleggen);

        /// <inheritdoc />
        public CurrencyCode Currency => CurrencyCode.Eur;

        /// <inheritdoc />
        public double Amount(
            SingleOrder order,
            double lastPrice,
            double lastQuantity,
            double leavesQuantity,
            double cumulativeQuantity,
            double averagePrice,
            double cumulativeCommission)
        {
            double commission;
            Instrument instrument = order.Instrument;
            if (instrument?.Exchange == null)
            {
                return 0d;
            }

            ExchangeCountry country = instrument.Exchange.Country;
            bool isEuronext = instrument.Exchange.IsEuronext && country != ExchangeCountry.Portugal;

            bool isCompletion = Math.Abs(leavesQuantity) < double.Epsilon;
            bool isNoCommission = Math.Abs(cumulativeCommission) < double.Epsilon;
            bool isSell = order.Side == OrderSide.Sell || order.Side == OrderSide.SellShort || order.Side == OrderSide.SellPlus;

            // Must get exchange rate here...
            const double exchangeRate = 1d;
            double lastTotalPrice = lastQuantity * lastPrice * exchangeRate;
            if (beleggingsvoorm == AbnAmroBeleggingsvoorm.Beleggingsadvies)
            {
                double averageTotalPrice = cumulativeQuantity * averagePrice * exchangeRate;
                if (isEuronext)
                {
                    // Aandelen, beleggingsfondsen en Turbo’s Euronext Amsterdam én aandelen Euronext Brussel en Parijs.
                    // Obligaties Euronext Amsterdam, Brussel en Parijs.
                    // Vast tarief per order = euro 8.00
                    // Tarief over transactiewaarde t/m euro 10000 = 0.425%, over het meerdere (t/m euro 1000000) += 0.35%
                    // Minimumtarief per order = euro 12.00
                    commission = isNoCommission ? 8d : cumulativeCommission;
                    if (averageTotalPrice <= 10000d)
                    {
                        if (isCompletion)
                        {
                            commission = Math.Max(12d, commission + lastTotalPrice * 0.00425d);
                        }
                        else
                        {
                            commission += lastTotalPrice * 0.00425d;
                        }
                    }
                    else
                    {
                        if (isCompletion)
                        {
                            commission = Math.Max(12d, commission + lastTotalPrice * 0.0035d);
                        }
                        else
                        {
                            commission += lastTotalPrice * 0.0035d;
                        }
                    }

                    commission -= cumulativeCommission;
                    return commission < 0 ? 0 : commission;
                }

                switch (country)
                {
                    // case ExchangeCountry.Greece:
                    // case ExchangeCountry.Ireland:
                    case ExchangeCountry.Germany:
                    case ExchangeCountry.Finland:
                    case ExchangeCountry.Italy:
                    case ExchangeCountry.Luxembourg:
                    case ExchangeCountry.Norway:
                    case ExchangeCountry.Austria:
                    case ExchangeCountry.Portugal:
                    case ExchangeCountry.Spain:
                    case ExchangeCountry.Sweden:
                    case ExchangeCountry.Switzerland:
                    case ExchangeCountry.UnitedKingdom:
                    {
                        // Aandelen buitenland:
                        // Denemarken, Duitsland, Finland, Griekenland, Ierland, Italië, Luxemburg, Noorwegen, Oostenrijk, Portugal, Spanje, Zweden en Zwitserland.
                        // Voor Engeland wordt bij aankoop 0,5% van de effectieve waarde in rekening gebracht in verband met 'stamp duty'.
                        // Vast tarief per order = euro 8.00
                        // Tarief over transactiewaarde t/m euro 10000 = 0.425%, over het meerdere (t/m euro 1000000) += 0.35%
                        // Minimumtarief per order = euro 20.00
                        // Vergoeding kosten correspondent:
                        // Tarief over transactiewaarde= 0.20%
                        // Minimumtarief per order = euro 5.00
                        // Maximumtarief per order = euro 1000.00
                        if (cumulativeCommission >= 1000)
                        {
                            return 0;
                        }

                        commission = isNoCommission ? 8 : cumulativeCommission;
                        if (averageTotalPrice <= 10000)
                        {
                            double coeff = (country == ExchangeCountry.UnitedKingdom && !isSell) ? 0.01125 : 0.00625;
                            if (isCompletion)
                            {
                                commission = Math.Max(25d, commission + lastTotalPrice * coeff);
                            }
                            else
                            {
                                commission += lastTotalPrice * coeff;
                            }
                        }
                        else
                        {
                            double coeff = (country == ExchangeCountry.UnitedKingdom && !isSell) ? 0.0105 : 0.0055;
                            if (isCompletion)
                            {
                                commission = Math.Max(25d, commission + lastTotalPrice * coeff);
                            }
                            else
                            {
                                commission += lastTotalPrice * coeff;
                            }
                        }

                        if (cumulativeCommission > 1000)
                        {
                            cumulativeCommission = 1000;
                        }

                        commission -= cumulativeCommission;
                        if (commission < 0)
                        {
                            commission = 0;
                        }

                        return commission;
                    }

                    // Aandelen buitenland: USA.
                    // Vast tarief per order = euro 8.00
                    // Tarief over transactiewaarde t/m euro 10000 = 0.425%, over het meerdere (t/m euro 1000000) += 0.35%
                    // Minimumtarief per order = euro 20.00
                    // Vergoeding kosten correspondent:
                    // Tarief over transactiewaarde: koers t/m EUR 1.75 = 0.20%, boven = euro 0.03 per aandeel
                    // Minimumtarief per order = euro 5.00
                    // Maximumtarief per order = euro 1000.00
                    case ExchangeCountry.UnitedStates when cumulativeCommission >= 1000:
                        return 0;
                    case ExchangeCountry.UnitedStates:
                    {
                        double extra = (lastPrice * exchangeRate <= 1.75) ? lastTotalPrice * 0.002 : 0.03 * lastQuantity;
                        commission = isNoCommission ? 8 : cumulativeCommission;
                        if (averageTotalPrice <= 10000)
                        {
                            if (isCompletion)
                            {
                                commission = Math.Max(25d, commission + lastTotalPrice * 0.00425d + extra);
                            }
                            else
                            {
                                commission += lastTotalPrice * 0.00425d + extra;
                            }
                        }
                        else
                        {
                            if (isCompletion)
                            {
                                commission = Math.Max(25, commission + lastTotalPrice * 0.0035 + extra);
                            }
                            else
                            {
                                commission += lastTotalPrice * 0.0035 + extra;
                            }
                        }

                        if (cumulativeCommission > 1000)
                        {
                            cumulativeCommission = 1000;
                        }

                        commission -= cumulativeCommission;
                        if (commission < 0)
                        {
                            commission = 0;
                        }

                        return commission;
                    }

                    // Aandelen, beleggingsfondsen en opties buitenland:
                    // Australië, Canada, Hong Kong, Japan, Nieuw Zeeland, Singapore en Zuid-Afrika.
                    // Vast tarief per order = euro 8.00
                    // Tarief over transactiewaarde t/m euro 10000 = 0.425%, over het meerdere (t/m euro 1000000) += 0.35%
                    // Minimumtarief per order = euro 20.00
                    // Vergoeding kosten correspondent:
                    // Tarief over transactiewaarde= 0.20%
                    // Minimumtarief per order = euro 45.00
                    // Maximumtarief per order = euro 1000.00
                    case ExchangeCountry.Canada when cumulativeCommission >= 1000:
                        return 0;
                    case ExchangeCountry.Canada:
                    {
                        commission = isNoCommission ? 8 : cumulativeCommission;
                        if (averageTotalPrice <= 10000)
                        {
                            if (isCompletion)
                            {
                                commission = Math.Max(65d, commission + lastTotalPrice * 0.00625d);
                            }
                            else
                            {
                                commission += lastTotalPrice * 0.00625d;
                            }
                        }
                        else
                        {
                            if (isCompletion)
                            {
                                commission = Math.Max(65d, commission + lastTotalPrice * 0.0055d);
                            }
                            else
                            {
                                commission += lastTotalPrice * 0.0055d;
                            }
                        }

                        if (cumulativeCommission > 1000)
                        {
                            cumulativeCommission = 1000;
                        }

                        commission -= cumulativeCommission;
                        if (commission < 0)
                        {
                            commission = 0;
                        }

                        return commission;
                    }
                }

                // Not supported exchange country.
                commission = lastTotalPrice * 0.1;
                if (isNoCommission)
                {
                    commission += 1000;
                }

                return commission;
            }

            if (isEuronext)
            {
                // Aandelen, obligaties en beursgenoteerde beleggingsfondsen Euronext Amsterdam, Brussel en Parijs
                // Vast tarief per order = euro 8.00
                // Orders t/m euro 1000000 = 0.10%
                // Minimumtarief per order = euro 10.00
                // Maximumtarief per order = euro 150.00
                if (cumulativeCommission >= 150)
                {
                    return 0;
                }

                commission = isNoCommission ? 8d : cumulativeCommission;
                commission = isCompletion
                    ? Math.Min(150d, Math.Max(10d, commission + lastTotalPrice * 0.001d))
                    : Math.Min(150d, commission + lastTotalPrice * 0.001d);
                commission -= cumulativeCommission;
                if (commission < 0)
                {
                    commission = 0;
                }

                return commission;
            }

            switch (country)
            {
                case ExchangeCountry.Germany:
                case ExchangeCountry.Finland:
                case ExchangeCountry.Italy:
                case ExchangeCountry.Luxembourg:
                case ExchangeCountry.Austria:
                case ExchangeCountry.Portugal:
                case ExchangeCountry.Spain:
                case ExchangeCountry.UnitedStates:
                case ExchangeCountry.Canada:
                case ExchangeCountry.UnitedKingdom:
                {
                    // Aandelen, obligaties en beursgenoteerde beleggingsfondsen buitenland:
                    // Duitsland, Finland, Griekenland, Ierland, Italië, Luxemburg, Oostenrijk,
                    // Portugal en Spanje, Engeland, USA en Canada.
                    // Voor Engeland wordt bij aankoop 0,5% van de effectieve waarde in
                    // rekening gebracht in verband met 'stamp duty'.
                    // Vast tarief per order = euro 15.00
                    // Tarief over transactiewaarde = 0.15%
                    if (!isSell && country == ExchangeCountry.UnitedKingdom)
                    {
                        // 0.15% + 0.5%, stamp duty
                        commission = lastTotalPrice * 0.0065;
                    }
                    else
                    {
                        commission = lastTotalPrice * 0.0015;
                    }

                    if (isNoCommission)
                    {
                        commission += 15;
                    }

                    return commission;
                }
            }

            switch (country)
            {
                case ExchangeCountry.Denmark:
                case ExchangeCountry.Norway:
                case ExchangeCountry.Sweden:
                case ExchangeCountry.Switzerland:
                case ExchangeCountry.Luxembourg:
                {
                    // Aandelen, obligaties, beursgenoteerde beleggingsfondsen en
                    // niet-beursgenoteerde beleggingsfondsen (Luxemburg) buitenland:
                    // Denemarken, Noorwegen, Zweden en Zwitserland.
                    // Vast tarief per order = euro 15.00
                    // Tarief over transactiewaarde = 0.30%
                    commission = lastTotalPrice * 0.003;
                    if (isNoCommission)
                    {
                        commission += 15;
                    }

                    return commission;
                }
            }

            // Not supported exchange country.
            commission = lastTotalPrice * 0.1;
            if (isNoCommission)
            {
                commission += 1000;
            }

            return commission;
        }
    }
}
