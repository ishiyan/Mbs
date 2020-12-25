using System.Collections.Generic;
using System.Linq;
using Mbs.Trading.Time;

// ReSharper disable once CheckNamespace
namespace Mbs.Trading.Data.Historical
{
    /// <summary>
    /// Encapsulates information about CSV files related to the same instrument.
    /// </summary>
    public class InstrumentCsvInfo
    {
        /// <summary>
        /// A collection of CSV files related to the instrument.
        /// </summary>
        private readonly List<CsvInfo> csvInfoList = new List<CsvInfo>();

        /// <summary>
        /// Gets a value indicating whether trade data is available.
        /// </summary>
        public bool HasTradeData { get; private set; }

        /// <summary>
        /// Gets a value indicating whether quote data is available.
        /// </summary>
        public bool HasQuoteData { get; private set; }

        /// <summary>
        /// Gets a value indicating whether scalar data is available.
        /// </summary>
        public bool HasScalarData { get; private set; }

        /// <summary>
        /// Gets a value indicating whether ohlcv data is available.
        /// </summary>
        public bool HasOhlcvData { get; private set; }

        /// <summary>
        /// Adds csv info.
        /// </summary>
        /// <param name="info">A csv info to add.</param>
        public void Add(CsvInfo info)
        {
            csvInfoList.Insert(0, info);
            switch (info.DataType)
            {
                case CsvDataType.Ohlcv:
                    HasOhlcvData = true;
                    break;
                case CsvDataType.Scalar:
                    HasScalarData = true;
                    break;
                case CsvDataType.Trade:
                    HasTradeData = true;
                    break;
                case CsvDataType.Quote:
                    HasQuoteData = true;
                    break;
            }
        }

        /// <summary>
        /// Given a data type, gets the first matching csv info.
        /// </summary>
        /// <param name="dataType">The data type.</param>
        /// <returns>The first matching csv info or null.</returns>
        public CsvInfo GetFirstData(CsvDataType dataType)
        {
            return csvInfoList.FirstOrDefault(c => c.DataType == dataType);
        }

        /// <summary>
        /// Gets a csv info which directly matches the given time granularity.
        /// </summary>
        /// <param name="dataType">The data type in question.</param>
        /// <param name="timeGranularity">The time granularity in question.</param>
        /// <returns>The first matching csv info or null.</returns>
        public CsvInfo GetMatchingData(CsvDataType dataType, TimeGranularity timeGranularity)
        {
            return csvInfoList.FirstOrDefault(
                csv => csv.DataType == dataType &&
                csv.TimeGranularity == timeGranularity);
        }

        /// <summary>
        /// Gets an adjusted csv info which directly matches the given time granularity.
        /// </summary>
        /// <param name="dataType">The data type in question.</param>
        /// <param name="timeGranularity">The time granularity in question.</param>
        /// <returns>The first matching csv info or null.</returns>
        public CsvInfo GetMatchingAdjustedData(CsvDataType dataType, TimeGranularity timeGranularity)
        {
            return csvInfoList.FirstOrDefault(
                csv => csv.DataType == dataType &&
                csv.TimeGranularity == timeGranularity &&
                csv.IsAdjustedData.HasValue &&
                csv.IsAdjustedData.Value);
        }

        /// <summary>
        /// Gets a csv info if an available ohlcv data with shorter time granularity can be aggregated into the desired one.
        /// </summary>
        /// <param name="desiredGranularity">The time granularity in question.</param>
        /// <returns>The first matching csv info or null.</returns>
        public CsvInfo GetAggregateOhlcvData(TimeGranularity desiredGranularity)
        {
            foreach (var csv in csvInfoList)
            {
                if (csv.DataType == CsvDataType.Ohlcv)
                {
                    TimeGranularity timeGranularity = csv.TimeGranularity;
                    if (timeGranularity == desiredGranularity)
                    {
                        return csv;
                    }

                    if (timeGranularity < desiredGranularity && AggregatingConverter.CanAggregate(timeGranularity, desiredGranularity))
                    {
                        return csv;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Gets an adjusted csv info if an available ohlcv data with shorter time granularity can be aggregated into the desired one.
        /// </summary>
        /// <param name="desiredGranularity">The time granularity in question.</param>
        /// <returns>The first matching csv info or null.</returns>
        public CsvInfo GetAggregateAdjustedOhlcvData(TimeGranularity desiredGranularity)
        {
            foreach (var csv in csvInfoList)
            {
                if (csv.DataType == CsvDataType.Ohlcv && csv.IsAdjustedData.HasValue && csv.IsAdjustedData.Value)
                {
                    TimeGranularity timeGranularity = csv.TimeGranularity;
                    if (timeGranularity == desiredGranularity)
                    {
                        return csv;
                    }

                    if (timeGranularity < desiredGranularity && AggregatingConverter.CanAggregate(timeGranularity, desiredGranularity))
                    {
                        return csv;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Gets a csv info if an available scalar data with shorter time granularity can be aggregated into the desired one.
        /// </summary>
        /// <param name="desiredGranularity">The time granularity in question.</param>
        /// <returns>The first matching csv info or null.</returns>
        public CsvInfo GetAggregateScalarData(TimeGranularity desiredGranularity)
        {
            foreach (var csv in csvInfoList)
            {
                if (csv.DataType == CsvDataType.Scalar)
                {
                    TimeGranularity timeGranularity = csv.TimeGranularity;
                    if (timeGranularity == desiredGranularity)
                    {
                        return csv;
                    }

                    if (timeGranularity < desiredGranularity && AggregatingConverter.CanAggregate(timeGranularity, desiredGranularity))
                    {
                        return csv;
                    }
                }
            }

            return null;
        }
    }
}
