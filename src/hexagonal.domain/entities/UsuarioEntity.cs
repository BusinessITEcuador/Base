namespace hexagonal.domain.entities
{
    public class UsuarioEntity : BaseEntity
    {
        public Guid Id { get; set; }
        public string Nombres { get; set; }
        public string PrimerApellido { get; set; }
        public string? SegundoApellido { get; set; }
        public string Correo { get; set; }
        public string? CorreoAlternativo { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public Guid IdNacionalidad { get; set; }
        public string Nacionalidad { get; set; }
        public Guid IdSexo { get; set; }
        public string Sexo { get; set; }
        public Guid IdGenero { get; set; }
        public string Genero { get; set; }
        public Guid IdTipoIdentificacion { get; set; }
        public string TipoIdentificacion { get; set; }
        public string NumeroIdentificacion { get; set; }

        public string IpPublica { get; set; }

        public string Altitud { get; set; }
        public string Longitud { get; set; }

        public string Latitud { get; set; }
    }
}