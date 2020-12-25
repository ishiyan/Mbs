using System;
using Mbs.Trading.Currencies;
using Mbs.Trading.Instruments;
using Mbs.Trading.Orders;

namespace Mbs.Trading.Brokers.Commissions
{
    /// <summary>
    /// Implements the Interactive Brokers commission strategy.
    /// </summary>
    public class InteractiveBrokersCommission : ICommission
    {
        private readonly double commissionRate;
        private readonly double minimumPerOrder;

        /// <summary>
        /// Initializes a new instance of the <see cref="InteractiveBrokersCommission"/> class.
        /// </summary>
        /// <param name="monthlyTradeAmountInUsDollars">The monthly dollar volume traded.</param>
        public InteractiveBrokersCommission(double monthlyTradeAmountInUsDollars = 0)
        {
            const double basisPoint = 0.0001;
            if (monthlyTradeAmountInUsDollars <= 1000000000)
            {
                // <= 1 billion.
                commissionRate = 0.2 * basisPoint;
                minimumPerOrder = 2.0;
            }
            else if (monthlyTradeAmountInUsDollars <= 2000000000)
            {
                // <= 2 billion.
                commissionRate = 0.15 * basisPoint;
                minimumPerOrder = 1.50;
            }
            else if (monthlyTradeAmountInUsDollars <= 5000000000)
            {
                // <= 5 billion.
                commissionRate = 0.10 * basisPoint;
                minimumPerOrder = 1.25;
            }
            else
            {
                commissionRate = 0.08 * basisPoint;
                minimumPerOrder = 1.00;
            }
        }

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
            Instrument instrument = order.Instrument;
            if (instrument.Type == InstrumentType.Forex)
            {
                // Conversion order.Instrument.Currency to USD???
                double commission = cumulativeQuantity * averagePrice * commissionRate;
                if (Math.Abs(leavesQuantity) < double.Epsilon && commission < minimumPerOrder)
                {
                    commission = minimumPerOrder;
                }

                commission -= cumulativeCommission;
                return commission < 0 ? 0 : commission;
            }

            if (instrument.Type == InstrumentType.Stock)
            {
                // Per share fees.
                double commission = 0.005 * cumulativeQuantity;

                // Maximum per order 0.5%, minimum per order $1.0.
                // Conversion order.Instrument.Currency to USD???
                var maximumPerOrder = 0.005 * cumulativeQuantity * averagePrice;
                if (commission < 1)
                {
                    commission = 1;
                }
                else if (commission > maximumPerOrder)
                {
                    commission = maximumPerOrder;
                }

                commission -= cumulativeCommission;
                return commission < 0 ? 0 : commission;
            }

            return 0;
        }
    }
}
