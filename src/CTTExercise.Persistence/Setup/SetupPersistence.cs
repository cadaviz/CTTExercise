namespace CTTExercise.Persistence.Setup
{
    using CTTExercise.Domain.Repositories;
    using CTTExercise.Persistence.Repositories;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public static class SetupPersistence
    {
        public static IServiceCollection ConfigurePersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureDatabase(configuration);

            services.AddRepositories();

            return services;
        }

        private static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var dbSettings = configuration.GetRequiredSection("DatabaseSettings").Get<DatabaseSettings>();
            ArgumentNullException.ThrowIfNull(dbSettings);

            services.AddSingleton(dbSettings);

            MongoDBSetup.Setup(services, dbSettings);
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();

            return services;
        }
    }
}
