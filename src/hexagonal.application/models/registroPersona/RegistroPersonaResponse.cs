namespace hexagonal.application.models.registroPersona
{
    public class RegistroPersonaResponse
    {
    }

    public class ConfiguracionPersonaResponse
    {
        public bool ValidarRiesgoPais { get; set; }
        public bool ValidarPuntoDeAcceso { get; set; }
        public bool ValidarNacionalidad { get; set; }
        public string NacionalidadesPermitidas { get; set; }
        public bool ValidarMayoriaEdad { get; set; }
        public int EdadMinima { get; set; }
        public bool HabilitarCaptcha { get; set; }
        public DateTime FechaInicialControl { get; set; }
        public DateTime FechaFinalControl { get; set; }
        public int VigenciaInformacion { get; set; }
        public int DuracionCodigoVerificacion { get; set; }
        public int IntentosPermitidos { get; set; }
        public int DuracionBloqueo { get; set; }
        public bool HabilitarConyuge { get; set; }
        public DateTime FechaMaximaRegistro { get; set; }
        public bool ActivarFase { get; set; }
        public string CategoriasVisasVigentes { get; set; }
        public string PaisesHabilitadosCedula { get; set; }

        public ConfiguracionPersonaResponse()
        {
            ValidarRiesgoPais = false;
            ValidarPuntoDeAcceso = false;
            ValidarNacionalidad = false;
            NacionalidadesPermitidas = string.Empty;
            ValidarMayoriaEdad = false;
            EdadMinima = 0;
            HabilitarCaptcha = false;
            FechaInicialControl = DateTime.Now;
            FechaFinalControl = DateTime.Now;
            VigenciaInformacion = 0;
            DuracionCodigoVerificacion = 0;
            IntentosPermitidos = 0;
            DuracionBloqueo = 0;
            HabilitarConyuge = false;
            FechaMaximaRegistro = DateTime.Now;
            ActivarFase = false;
            CategoriasVisasVigentes = string.Empty;
            PaisesHabilitadosCedula = string.Empty;
        }
    }
}