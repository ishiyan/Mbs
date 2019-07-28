using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using Mbs.Api.ExampleProviders.Trading.Data.Generators.Chirp;
using Mbs.Api.ExampleProviders.Trading.Data.Generators.FractionalBrownianMotion;
using Mbs.Api.ExampleProviders.Trading.Data.Generators.MultiSinusoidal;
using Mbs.Api.ExampleProviders.Trading.Data.Generators.RepetitiveSample;
using Mbs.Api.ExampleProviders.Trading.Data.Generators.Sawtooth;
using Mbs.Api.ExampleProviders.Trading.Data.Generators.Sinusoidal;
using Mbs.Api.ExampleProviders.Trading.Data.Generators.Square;
using Mbs.Api.Extensions.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Mbs.Api.Extensions
{
    /// <summary>
    /// Adds MbsApi Swashbuckle Swagger services to the dependency injection container.
    /// </summary>
    public static class SwaggerServiceCollectionExtensions
    {
        /// <summary>
        /// Adds MbsApi Swashbuckle Swagger services to the specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">A collection of services.</param>
        /// <param name="additionalXmls">An additional XML file names (without extension) for Swagger to search for comments.</param>
        /// <returns>An updated service collection.</returns>
        public static IServiceCollection AddMbsApiSwagger(this IServiceCollection services, params string[] additionalXmls)
        {
            var assembly = Assembly.GetExecutingAssembly();

            services.AddSwaggerGen(options =>
            {
                IncludeDocPerVersion(options, assembly);
                ApplyDocInclusions(options);

                options.DescribeAllEnumsAsStrings();
                options.DescribeStringEnumsInCamelCase();
                options.DescribeAllParametersInCamelCase();
                options.IgnoreObsoleteActions();
                options.IgnoreObsoleteProperties();
                options.UseReferencedDefinitionsForEnums();
                options.ExampleFilters();

                options.OrderActionsBy((apiDesc) => $"{apiDesc.RelativePath}_{apiDesc.HttpMethod}");
                /* options.TagActionsBy(p => new List<string> { p.GroupName }); */

                var basePath = AppContext.BaseDirectory;
                options.IncludeXmlComments(CombinePath(basePath, "Mbs"));
                options.IncludeXmlComments(CombinePath(basePath, "Mbs.Api"));
                foreach (var file in additionalXmls)
                    options.IncludeXmlComments(CombinePath(basePath, file));
            });

            services.AddSwaggerExamplesFromAssemblyOf<MultiSinusoidalOhlcvGeneratorParametersExampleProvider>();
            services.AddSwaggerExamplesFromAssemblyOf<MultiSinusoidalQuoteGeneratorParametersExampleProvider>();
            services.AddSwaggerExamplesFromAssemblyOf<MultiSinusoidalTradeGeneratorParametersExampleProvider>();
            services.AddSwaggerExamplesFromAssemblyOf<MultiSinusoidalScalarGeneratorParametersExampleProvider>();

            services.AddSwaggerExamplesFromAssemblyOf<ChirpOhlcvGeneratorParametersExampleProvider>();
            services.AddSwaggerExamplesFromAssemblyOf<ChirpQuoteGeneratorParametersExampleProvider>();
            services.AddSwaggerExamplesFromAssemblyOf<ChirpTradeGeneratorParametersExampleProvider>();
            services.AddSwaggerExamplesFromAssemblyOf<ChirpScalarGeneratorParametersExampleProvider>();

            services.AddSwaggerExamplesFromAssemblyOf<SquareOhlcvGeneratorParametersExampleProvider>();
            services.AddSwaggerExamplesFromAssemblyOf<SquareQuoteGeneratorParametersExampleProvider>();
            services.AddSwaggerExamplesFromAssemblyOf<SquareTradeGeneratorParametersExampleProvider>();
            services.AddSwaggerExamplesFromAssemblyOf<SquareScalarGeneratorParametersExampleProvider>();

            services.AddSwaggerExamplesFromAssemblyOf<SawtoothOhlcvGeneratorParametersExampleProvider>();
            services.AddSwaggerExamplesFromAssemblyOf<SawtoothQuoteGeneratorParametersExampleProvider>();
            services.AddSwaggerExamplesFromAssemblyOf<SawtoothTradeGeneratorParametersExampleProvider>();
            services.AddSwaggerExamplesFromAssemblyOf<SawtoothScalarGeneratorParametersExampleProvider>();

            services.AddSwaggerExamplesFromAssemblyOf<SinusoidalOhlcvGeneratorParametersExampleProvider>();
            services.AddSwaggerExamplesFromAssemblyOf<SinusoidalQuoteGeneratorParametersExampleProvider>();
            services.AddSwaggerExamplesFromAssemblyOf<SinusoidalTradeGeneratorParametersExampleProvider>();
            services.AddSwaggerExamplesFromAssemblyOf<SinusoidalScalarGeneratorParametersExampleProvider>();

            services.AddSwaggerExamplesFromAssemblyOf<RepetitiveSampleGeneratorParametersExampleProvider>();

            services.AddSwaggerExamplesFromAssemblyOf<FractionalBrownianMotionOhlcvGeneratorParametersExampleProvider>();
            services.AddSwaggerExamplesFromAssemblyOf<FractionalBrownianMotionQuoteGeneratorParametersExampleProvider>();
            services.AddSwaggerExamplesFromAssemblyOf<FractionalBrownianMotionTradeGeneratorParametersExampleProvider>();
            services.AddSwaggerExamplesFromAssemblyOf<FractionalBrownianMotionScalarGeneratorParametersExampleProvider>();

            return services;
        }

        /// <summary>
        /// Configures the Swagger UI options.
        /// </summary>
        /// <param name="options">The Swagger UI options.</param>
        internal static void ConfigureSwaggerUi(SwaggerUIOptions options)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var apiVersions = GetApiVersions(assembly);

            foreach (var apiVersion in apiVersions)
            {
                options.SwaggerEndpoint($"/swagger/v{apiVersion}/swagger.json", $"mbs api v{apiVersion}");
            }

            options.RoutePrefix = "swagger";
        }

        private static void IncludeDocPerVersion(SwaggerGenOptions options, Assembly assembly)
        {
            var apiVersions = GetApiVersions(assembly);
            foreach (var apiVersion in apiVersions)
            {
                options.SwaggerDoc($"v{apiVersion}", new Info
                {
                    Title = "mbs api",
                    Version = $"v{apiVersion}",
                    Description = "an api to access the mbs functionality",
                    TermsOfService = "https://choosealicense.com/no-permission/",
                    License = new License
                    {
                        Name = "no license is granted, only the copyright holder can use the api"
                    }
                });
            }
        }

        private static void ApplyDocInclusions(SwaggerGenOptions options)
        {
            options.DocInclusionPredicate((docName, apiDesc) =>
            {
                if (!apiDesc.TryGetMethodInfo(out MethodInfo methodInfo))
                    return false;

                var versions = methodInfo.DeclaringType
                    .GetCustomAttributes(true)
                    .OfType<ApiVersionAttribute>()
                    .SelectMany(attr => attr.Versions);

                return versions.Any(v => $"v{v.ToString(CultureInfo.InvariantCulture)}" == docName);
            });
        }

        private static IEnumerable<string> GetApiVersions(Assembly webApiAssembly)
        {
            var apiVersion = webApiAssembly.DefinedTypes
                .Where(x => x.IsSubclassOf(typeof(ControllerBase)) && x.GetCustomAttributes<ApiVersionAttribute>().Any())
                .Select(y => y.GetCustomAttribute<ApiVersionAttribute>())
                .SelectMany(v => v.Versions)
                .Distinct()
                .OrderBy(x => x);

            return apiVersion.Select(x => x.ToString(CultureInfo.InvariantCulture));
        }

        private static string CombinePath(string basePath, string xmlFile)
        {
            var filePath = Path.Combine(basePath, xmlFile + ".xml");
            if (!File.Exists(filePath))
                throw new FileNotFoundException("Swagger XML documentation file is not found.", filePath);
            return filePath;
        }
    }
}