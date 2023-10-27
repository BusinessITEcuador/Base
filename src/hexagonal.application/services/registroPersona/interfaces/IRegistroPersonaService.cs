using hexagonal.application.models.registroPersona;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hexagonal.application.services.registroPersona.interfaces
{
     public interface IRegistroPersonaService
    {
        Task<ConfiguracionPersonaResponse> ObtenerConfiguracionDePersona();

        int CalcularEdad(DateTime fechaNaciomiento);
    }
}
