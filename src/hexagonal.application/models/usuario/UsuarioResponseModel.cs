namespace hexagonal.application.models.usuario
{
    public class UsuarioResponseModel
    {
        public Guid Id { get; set; }
        public string Nombres { get; set; }
        public string PrimerApellido { get; set; }
        public string? SegundoApellido { get; set; }
        public string Correo { get; set; }
        public string CorreoAlternativo { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public Guid IdNacionalidad { get; set; }
        public Guid IdSexo { get; set; }
        public Guid IdGenero { get; set; }
        public Guid IdTipoIdentificacion { get; set; }
        public string NumeroIdentificacion { get; set; }

        public string IpPublica { get; set; }
        public string Longitud { get; set; }
        public string Latitud { get; set; }
        public string Altitud { get; set; }
    }
}