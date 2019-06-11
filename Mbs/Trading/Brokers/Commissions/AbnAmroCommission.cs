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
        // private readonly string NotSupportedExchangeCountry = "Not supported exchange country: ";
        private readonly AbnAmroBeleggingsvoorm beleggingsvoorm;

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
                return 0d;

            // InstrumentType type = instrument.Type;
            ExchangeCountry country = instrument.Exchange.Country;
            bool isEuronext = instrument.Exchange.IsEuronext && country != ExchangeCountry.Portugal;

            // bool isFirstFill = Math.Abs(cumulativeQuantity - lastQuantity) < double.Epsilon;
            bool isCompletion = Math.Abs(leavesQuantity) < double.Epsilon;
            bool isNoCommission = Math.Abs(cumulativeCommission) < double.Epsilon;
            bool isSell = OrderSide.Sell == order.Side || OrderSide.SellShort == order.Side || OrderSide.SellPlus == order.Side;

            // Must get exchange rate here...
            const double exchangeRate = 1d;
            double lastTotalPrice = lastQuantity * lastPrice * exchangeRate;
            if (AbnAmroBeleggingsvoorm.Beleggingsadvies == beleggingsvoorm)
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
                            commission = Math.Max(12d, commission + lastTotalPrice * 0.00425d);
                        else
                            commission += lastTotalPrice * 0.00425d;
                    }
                    else
                    {
                        if (isCompletion)
                            commission = Math.Max(12d, commission + lastTotalPrice * 0.0035d);
                        else
                            commission += lastTotalPrice * 0.0035d;
                    }

                    commission -= cumulativeCommission;
                    return 0d > commission ? 0d : commission;
                }

                if (ExchangeCountry.Germany == country ||
                    ExchangeCountry.Finland == country ||
                    /* ExchangeCountry.Greece == country || */
                    /* ExchangeCountry.Ireland == country || */
                    ExchangeCountry.Italy == country ||
                    ExchangeCountry.Luxembourg == country ||
                    ExchangeCountry.Norway == country ||
                    ExchangeCountry.Austria == country ||
                    ExchangeCountry.Portugal == country ||
                    ExchangeCountry.Spain == country ||
                    ExchangeCountry.Sweden == country ||
                    ExchangeCountry.Switzerland == country)
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
                    if (1000d <= cumulativeCommission)
                        return 0d;
                    commission = isNoCommission ? 8d : cumulativeCommission;
                    if (averageTotalPrice <= 10000d)
                    {
                        double coeff = (ExchangeCountry.UnitedKingdom == country && !isSell) ? 0.01125d : 0.00625d;
                        if (isCompletion)
                            commission = Math.Max(25d, commission + lastTotalPrice * coeff);
                        else
                            commission += lastTotalPrice * coeff;
                    }
                    else
                    {
                        double coeff = (ExchangeCountry.UnitedKingdom == country && !isSell) ? 0.0105d : 0.0055d;
                        if (isCompletion)
                            commission = Math.Max(25d, commission + lastTotalPrice * coeff);
                        else
                            commission += lastTotalPrice * coeff;
                    }

                    if (1000.0 < cumulativeCommission)
                        cumulativeCommission = 1000d;
                    commission -= cumulativeCommission;
                    if (0d > commission)
                        commission = 0d;
                    return commission;
                }

                if (ExchangeCountry.UnitedStates == country)
                {
                    // Aandelen buitenland: USA.
                    // Vast tarief per order = euro 8.00
                    // Tarief over transactiewaarde t/m euro 10000 = 0.425%, over het meerdere (t/m euro 1000000) += 0.35%
                    // Minimumtarief per order = euro 20.00
                    // Vergoeding kosten correspondent:
                    // Tarief over transactiewaarde: koers t/m EUR 1.75 = 0.20%, boven = euro 0.03 per aandeel
                    // Minimumtarief per order = euro 5.00
                    // Maximumtarief per order = euro 1000.00
                    if (1000d <= cumulativeCommission)
                        return 0d;
                    double extra = (1.75d >= lastPrice * exchangeRate) ? lastTotalPrice * 0.002d : 0.03d * lastQuantity;
                    commission = isNoCommission ? 8d : cumulativeCommission;
                    if (averageTotalPrice <= 10000d)
                    {
                        if (isCompletion)
                            commission = Math.Max(25d, commission + lastTotalPrice * 0.00425d + extra);
                        else
                            commission += lastTotalPrice * 0.00425d + extra;
                    }
                    else
                    {
                        if (isCompletion)
                            commission = Math.Max(25d, commission + lastTotalPrice * 0.0035d + extra);
                        else
                            commission += lastTotalPrice * 0.0035d + extra;
                    }

                    if (1000d < cumulativeCommission)
                        cumulativeCommission = 1000d;
                    commission -= cumulativeCommission;
                    if (0d > commission)
                        commission = 0d;
                    return commission;
                }

                if (/* ExchangeCountry.Australia == country || */
                    /* ExchangeCountry.HongKong == country || */
                    /* ExchangeCountry.Japan == country) || */
                    /* ExchangeCountry.NewZealand == country || */
                    /* ExchangeCountry.Singapore == country || */
                    /* ExchangeCountry.SouthAfrica == country || */
                    ExchangeCountry.Canada == country)
                {
                    // Aandelen, beleggingsfondsen en opties buitenland:
                    // Australië, Canada, Hong Kong, Japan, Nieuw Zeeland, Singapore en Zuid-Afrika.
                    // Vast tarief per order = euro 8.00
                    // Tarief over transactiewaarde t/m euro 10000 = 0.425%, over het meerdere (t/m euro 1000000) += 0.35%
                    // Minimumtarief per order = euro 20.00
                    // Vergoeding kosten correspondent:
                    // Tarief over transactiewaarde= 0.20%
                    // Minimumtarief per order = euro 45.00
                    // Maximumtarief per order = euro 1000.00
                    if (1000d <= cumulativeCommission)
                        return 0d;
                    commission = isNoCommission ? 8d : cumulativeCommission;
                    if (averageTotalPrice <= 10000d)
                    {
                        if (isCompletion)
                            commission = Math.Max(65d, commission + lastTotalPrice * 0.00625d);
                        else
                            commission += lastTotalPrice * 0.00625d;
                    }
                    else
                    {
                        if (isCompletion)
                            commission = Math.Max(65d, commission + lastTotalPrice * 0.0055d);
                        else
                            commission += lastTotalPrice * 0.0055d;
                    }

                    if (1000d < cumulativeCommission)
                        cumulativeCommission = 1000d;
                    commission -= cumulativeCommission;
                    if (0d > commission)
                        commission = 0d;
                    return commission;
                }

                // Not supported exchange country.
                // throw new ArgumentException(string.Concat(NotSupportedExchangeCountry, country.ToString()));
                commission = lastTotalPrice * 0.1d;
                if (isNoCommission)
                    commission += 1000d;
                return commission;
            }

            if (isEuronext)
            {
                // Aandelen, obligaties en beursgenoteerde beleggingsfondsen Euronext Amsterdam, Brussel en Parijs
                // Vast tarief per order = euro 8.00
                // Orders t/m euro 1000000 = 0.10%
                // Minimumtarief per order = euro 10.00
                // Maximumtarief per order = euro 150.00
                if (150d <= cumulativeCommission)
                    return 0d;
                commission = isNoCommission ? 8d : cumulativeCommission;
                commission = isCompletion ?
                    Math.Min(150d, Math.Max(10d, commission + lastTotalPrice * 0.001d)) :
                    Math.Min(150d, commission + lastTotalPrice * 0.001d);
                commission -= cumulativeCommission;
                if (0d > commission)
                    commission = 0d;
                return commission;
            }

            if (ExchangeCountry.Germany == country ||
                ExchangeCountry.Finland == country ||
                /* ExchangeCountry.Greece == country || */
                /* ExchangeCountry.Ireland == country || */
                ExchangeCountry.Italy == country ||
                ExchangeCountry.Luxembourg == country ||
                ExchangeCountry.Austria == country ||
                ExchangeCountry.Portugal == country ||
                ExchangeCountry.Spain == country ||
                ExchangeCountry.UnitedStates == country ||
                ExchangeCountry.Canada == country ||
                ExchangeCountry.UnitedKingdom == country)
            {
                // Aandelen, obligaties en beursgenoteerde beleggingsfondsen buitenland:
                // Duitsland, Finland, Griekenland, Ierland, Italië, Luxemburg, Oostenrijk,
                // Portugal en Spanje, Engeland, USA en Canada.
                // Voor Engeland wordt bij aankoop 0,5% van de effectieve waarde in
                // rekening gebracht in verband met 'stamp duty'.
                // Vast tarief per order = euro 15.00
                // Tarief over transactiewaarde = 0.15%
                if (!isSell && ExchangeCountry.UnitedKingdom == country)
                    commission = lastTotalPrice * 0.0065d; // 0.15% + 0.5%, stamp duty
                else
                    commission = lastTotalPrice * 0.0015d;
                if (isNoCommission)
                    commission += 15d;
                return commission;
            }

            if (ExchangeCountry.Denmark == country ||
                ExchangeCountry.Norway == country ||
                ExchangeCountry.Sweden == country ||
                ExchangeCountry.Switzerland == country ||
                ExchangeCountry.Luxembourg == country)
            {
                // Aandelen, obligaties, beursgenoteerde beleggingsfondsen en
                // niet-beursgenoteerde beleggingsfondsen (Luxemburg) buitenland:
                // Denemarken, Noorwegen, Zweden en Zwitserland.
                // Vast tarief per order = euro 15.00
                // Tarief over transactiewaarde = 0.30%
                commission = lastTotalPrice * 0.003d;
                if (isNoCommission)
                    commission += 15d;
                return commission;
            }

            /*
            if (ExchangeCountry.Australia == country ||
                ExchangeCountry.HongKong == country ||
                ExchangeCountry.Japan == country ||
                ExchangeCountry.NewZealand == country ||
                ExchangeCountry.Singapore == country ||
                ExchangeCountry.SouthAfrica == country)
            {
                // Overige beurzen: Australië, Hong Kong, Japan, Nieuw Zeeland, Singapore en Zuid-Afrika.
                // Alleen mogelijk via telefoon.
                // Vast tarief per order = euro 20.00
                // Tarief over transactiewaarde = 0.40%
                commission = lastTotalPrice * 0.004d;
                if (isNoCommission)
                    commission += 20d;
                return commission;
            }
            */

            // Not supported exchange country.
            // throw new ArgumentException(string.Concat(NotSupportedExchangeCountry, country.ToString()));
            commission = lastTotalPrice * 0.1d;
            if (isNoCommission)
                commission += 1000d;
            return commission;
        }

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
    }
}
