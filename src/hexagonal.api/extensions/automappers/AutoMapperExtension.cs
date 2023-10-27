namespace hexagonal.api.extensions.automappers
{
    public static class AutoMapperExtension
    {
        public static void ConfigureAutoMappersServices(IServiceCollection service)
        {
            service.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }
    }
}