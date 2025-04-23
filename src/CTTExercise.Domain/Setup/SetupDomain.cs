namespace CTTExercise.Domain.Setup
{
    using CTTExercise.Domain.Services;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public static class SetupDomain
    {
        public static IServiceCollection ConfigureDomain(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddServices();

            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();

            return services;
        }
    }
}
