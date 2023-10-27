using hexagonal.application.external;
using hexagonal.application.models.registroPersona;
using hexagonal.application.services.registroPersona.interfaces;

namespace hexagonal.application.services.registroPersona
{
    // Aplicacion
    public class RegistroPersonaService : IRegistroPersonaService
    {
        private readonly RegistroPersonaClient _registroPersonaClient;

        public RegistroPersonaService(RegistroPersonaClient registroPersonaClient)
        {
            _registroPersonaClient = registroPersonaClient ?? throw new ArgumentNullException(nameof(registroPersonaClient));
        }

        public async Task<ConfiguracionPersonaResponse> ObtenerConfiguracionDePersona()
        {
            return await _registroPersonaClient.ObtenerConfiguracionDePersona();
        }



        public int CalcularEdad(DateTime fechaNacimiento)
        {
            DateTime fechaActual = DateTime.Today;
            int edad = fechaActual.Year - fechaNacimiento.Year;

            if (fechaNacimiento > fechaActual.AddYears(-edad))
                edad--;

            return edad;
        }
    }

}
