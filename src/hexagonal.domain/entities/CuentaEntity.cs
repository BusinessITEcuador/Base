namespace hexagonal.domain.entities
{
    public class CuentaEntity : BaseEntity
    {
        public Guid Id { get; set; }

        public string Correo { get; set; }

        public Guid UsuarioId { get; set; }
    }
}