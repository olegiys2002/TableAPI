using Nest;


namespace BookingTables.API.ServicesConfiguration
{
    public static  class ElasticSearchExtensions
    {
        public static void ConfigureElasticsearch(
            this IServiceCollection services, IConfiguration configuration)
        {
            var url = configuration["ELKConfiguration:uri"];
            var defaultIndex = configuration["ELKConfiguration:index"];

            var settings = new ConnectionSettings(new Uri(url))
                .PrettyJson()
                .DefaultIndex(defaultIndex);
            settings.EnableApiVersioningHeader();

            AddDefaultMappings(settings);

            var client = new ElasticClient(settings);

            services.AddSingleton<IElasticClient>(client);

            CreateIndex(client, defaultIndex);
        }

        private static void AddDefaultMappings(ConnectionSettings settings)
        {
           
        }

        private static void CreateIndex(IElasticClient client, string indexName)
        {

        }
    }
}
