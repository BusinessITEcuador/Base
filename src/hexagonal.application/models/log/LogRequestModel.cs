namespace hexagonal.application.models.log
{
    public class LogRequestModel
    {
        public string Usuario { get; set; }
        public string Estado { get; set; }
        public string? Altitud { get; set; }
        public string? Latitud { get; set; }
        public string? Longitud { get; set; }
        public string? IpPublica { get; set; }
        public string? Mensaje { get; set; }

        public LogRequestModel()
        {
            Usuario = string.Empty;
            Estado = string.Empty;
            Altitud = string.Empty;
            Latitud = string.Empty;
            Longitud = string.Empty;
            IpPublica = string.Empty;
            Mensaje = string.Empty;
        }
    }
}