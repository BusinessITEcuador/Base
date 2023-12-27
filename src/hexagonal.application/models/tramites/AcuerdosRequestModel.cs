namespace hexagonal.application.models.tramites
{
    public class AcuerdosRequestModel
    {
        public Datos? datos { get; set; }
    }

    public class Datos
    {
        public Guid? id { get; set; }
        public string? nombres { get; set; }
        public string? primerApellido { get; set; }
        public string? segundoApellido { get; set; }
        public string? correo { get; set; }
        public string? correoAlternativo { get; set; }
        public DateTime? fechaNacimiento { get; set; }
        public string? nacionalidad { get; set; }
        public string? genero { get; set; }
        public string? sexo { get; set; }
        public string? tipoIdentificacion { get; set; }

        public string? numeroIdentificacion { get; set; }
    }
}