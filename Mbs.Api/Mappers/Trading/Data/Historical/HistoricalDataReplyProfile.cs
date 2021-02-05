using System.Collections.Generic;
using AutoMapper;
using Mbs.Api.Models.Trading.Data.Historical;
using Mbs.Trading.Data;
using Mbs.Trading.Data.Entities;
using Mbs.Trading.Data.Historical;

namespace Mbs.Api.Mappers.Trading.Data.Historical
{
    /// <summary>
    /// A mapper profile for the historical data reply.
    /// </summary>
    public class HistoricalDataReplyProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HistoricalDataReplyProfile"/> class.
        /// </summary>
        public HistoricalDataReplyProfile()
        {
            CreateMap<(HistoricalDataRequest req, List<Ohlcv> list), HistoricalDataReply<Ohlcv>>()
                .ForMember(d => d.IsDataAdjusted, opt => opt.MapFrom(s => s.req.IsDataAdjusted))
                .ForMember(d => d.Data, opt => opt.MapFrom(s => s.list));

            CreateMap<(HistoricalDataRequest req, List<Trade> list), HistoricalDataReply<Trade>>()
                .ForMember(d => d.IsDataAdjusted, opt => opt.MapFrom(s => s.req.IsDataAdjusted))
                .ForMember(d => d.Data, opt => opt.MapFrom(s => s.list));

            CreateMap<(HistoricalDataRequest req, List<Quote> list), HistoricalDataReply<Quote>>()
                .ForMember(d => d.IsDataAdjusted, opt => opt.MapFrom(s => s.req.IsDataAdjusted))
                .ForMember(d => d.Data, opt => opt.MapFrom(s => s.list));

            CreateMap<(HistoricalDataRequest req, List<Scalar> list), HistoricalDataReply<Scalar>>()
                .ForMember(d => d.IsDataAdjusted, opt => opt.MapFrom(s => s.req.IsDataAdjusted))
                .ForMember(d => d.Data, opt => opt.MapFrom(s => s.list));
        }

        /// <summary>
        /// A generic mapper for the <see cref="HistoricalDataReply{T}"/>.
        /// </summary>
        /// <typeparam name="T">A <see cref="TemporalEntity"/> type.</typeparam>
        /// <param name="autoMapper">The auto-mapper instance.</param>
        /// <param name="historicalDataRequest">A historical data request.</param>
        /// <param name="data">The fetched data.</param>
        /// <returns>The mapped historical data reply.</returns>
        public static HistoricalDataReply<T> Map<T>(IMapper autoMapper, HistoricalDataRequest historicalDataRequest, List<T> data)
            where T : TemporalEntity
        {
            return autoMapper.Map<HistoricalDataReply<T>>((historicalDataRequest, data));
        }
    }
}
