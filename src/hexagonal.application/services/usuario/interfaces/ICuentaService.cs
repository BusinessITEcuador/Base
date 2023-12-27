using hexagonal.application.models.usuario;

namespace hexagonal.application.services.usuario.interfaces
{
    public interface ICuentaService
    {
        void Add(UsuarioRequestModel Usuario, Guid UsuarioId);

        void AgregarCuenta(Guid id, string correo, Guid UsuarioId);

        CuentaResponseModel GetById(Guid id);
    }
}