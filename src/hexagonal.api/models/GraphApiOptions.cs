namespace hexagonal.infrastructure.api.models
{
    public class GraphApiOptions
    {
        public string Scopes { get; set; }
        public string TenantId { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string ExtensionId { get; set; }
    }
}
