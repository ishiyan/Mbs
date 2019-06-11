using System;
using System.Collections.Generic;
using Mbs.Trading.Currencies;
using Mbs.Trading.Instruments;
using Mbs.Trading.Orders;

namespace Mbs.Trading.Brokers.Commissions
{
    /// <summary>
    /// Implements the FXCM commission strategies as described in
    /// <c>http://www.fxcm.com/forex/forex-pricing/</c> and
    /// <c>https://www.fxcm.com/uk/markets/cfds/frequently-asked-questions/</c>.
    /// </summary>
    public class FxcmCommission : ICommission
    {
        private static readonly List<string> GroupCommissionSchedule = new List<string>
        {
            "EURUSD", "GBPUSD", "USDJPY", "USDCHF", "AUDUSD", "EURJPY", "GBPJPY"
        };

        /// <inheritdoc />
        public CurrencyCode Currency => CurrencyCode.Usd;

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
            // From http://www.fxcm.com/forex/forex-pricing/ (on Oct 6th, 2015)
            // Forex: $0.04 per side per 1k lot for EURUSD, GBPUSD, USDJPY, USDCHF, AUDUSD, EURJPY, GBPJPY
            //        $0.06 per side per 1k lot for other instruments
            // From https://www.fxcm.com/uk/markets/cfds/frequently-asked-questions/
            // CFD: no commissions
            Instrument instrument = order.Instrument;
            if (instrument.Type != InstrumentType.Forex)
                return 0;

            string symbol = instrument.Symbol?.ToUpperInvariant();
            if (string.IsNullOrWhiteSpace(symbol))
                return 0;

            double commissionRate = GroupCommissionSchedule.Contains(symbol) ? 0.04 : 0.06;

            return Math.Abs(commissionRate * lastPrice * lastQuantity / 1000);
        }
    }
}
