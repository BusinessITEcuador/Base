using hexagonal.application.models.registroPersona;

namespace hexagonal.application.services.registroPersona.interfaces
{
    public interface IRegistroPersonaService
    {
        Task<ConfiguracionPersonaResponse> ObtenerConfiguracionDePersona();

        int CalcularEdad(DateTime fechaNaciomiento);
    }
}