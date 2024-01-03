namespace hexagonal.application.models.usuario
{
    public class CuentaResponseModel
    {
        public Guid Id { get; set; }
        public string Correo { get; set; }

        public CuentaResponseModel()
        {
            Id = Guid.Empty;
            Correo = string.Empty;
        }
    }
}