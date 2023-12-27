using hexagonal.application.models.registroPersona;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace hexagonal.application.external
{
    public class RegistroPersonaClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public RegistroPersonaClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _configuration = configuration;
        }

        public async Task<ConfiguracionPersonaResponse> ObtenerConfiguracionDePersona()
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{_configuration.GetSection("Externos:RegistroPersona:BaseUrl").Value}/api/RegistroPersona/personaConfiguracion?fase=F1");

            if (response.IsSuccessStatusCode)
            {
                var responseStream = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ConfiguracionPersonaResponse>(responseStream);
            }

            throw new Exception();
        }
    }
}