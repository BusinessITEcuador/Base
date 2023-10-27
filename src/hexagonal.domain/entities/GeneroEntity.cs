namespace hexagonal.domain.entities
{
    public class GeneroEntity : BaseEntity
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }

        public GeneroEntity()
        {
            Id = Guid.NewGuid();
            Nombre = string.Empty;
        }
    }
}