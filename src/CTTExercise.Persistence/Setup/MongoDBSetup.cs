namespace CTTExercise.Persistence.Setup
{
    using CTTExercise.Domain.Entities;
    using Microsoft.Extensions.DependencyInjection;
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization;
    using MongoDB.Bson.Serialization.Serializers;
    using MongoDB.Driver;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    internal static class MongoDBSetup
    {
        public static void Setup(IServiceCollection services, DatabaseSettings databaseSettings)
        {
            services.AddSingleton<IMongoClient>(sp =>
            {
                return new MongoClient(databaseSettings.ConnectionString);
            });

            RegisterSerializer();

            RegisterClassMap();
        }

        private static void RegisterClassMap()
        {
            BsonClassMap.TryRegisterClassMap<Product>(cm =>
            {
                cm.AutoMap();
            });
        }

        private static void RegisterSerializer()
        {
            BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
        }
    }
}
