namespace MinAPIDemo.Common
{
    public static class EndpointDefinitionExtension
    {
        public static void AddEndpointDefinitions(this IServiceCollection services, params Type[] scanMarkers)
        {
            var endpoitnDefinitions = new List<IEndpointDefinition>();
            foreach(var marker in scanMarkers) 
            {
                endpoitnDefinitions.AddRange(
                    marker.Assembly.ExportedTypes
                        .Where(x => typeof(IEndpointDefinition).IsAssignableFrom(x) && !x.IsAbstract)
                        .Select(Activator.CreateInstance).Cast<IEndpointDefinition>()
                );
            }
            foreach(var endpointDefinition in endpoitnDefinitions)
            {
                endpointDefinition.DefineServices(services);
            }
            services.AddSingleton(endpoitnDefinitions as IReadOnlyCollection<IEndpointDefinition>);
        }

        public static void UseEndpointDefinitions(this WebApplication app)
        {
            var definitions = app.Services.GetRequiredService<IReadOnlyCollection<IEndpointDefinition>>();
            foreach(var endpointDefinition in definitions)
            {
                endpointDefinition.DefineEndpoints(app);
            }
        }
    }
}