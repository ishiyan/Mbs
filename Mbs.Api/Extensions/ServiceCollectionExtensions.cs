using System;
using AutoMapper;
using Mbs.Api.Services.Trading.Data.Historical;
using Mbs.Api.Services.Trading.Instruments;
using Microsoft.Extensions.DependencyInjection;

namespace Mbs.Api.Extensions
{
    /// <summary>
    /// Adds MbsApi services to the dependency injection container.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds MbsApi services to the specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">A collection of services.</param>
        /// <returns>An updated service collection.</returns>
        public static IServiceCollection AddMbsApi(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddSingleton<IInstrumentListDataService, InstrumentListDataService>();
            services.AddSingleton<IEuronextHistoricalDataService, EuronextHistoricalDataService>();

            return services;
        }
    }
}