using hexagonal.application.models.usuario;

namespace hexagonal.application.services.usuario.interfaces
{
    public interface IUsuarioService
    {
        IList<UsuarioResponseModel> GetAll();

        void Add(UsuarioRequestModel Usuario, Guid UsuarioId);

        void Update(UsuarioRequestModel Usuario, Guid id, Guid UsuarioId);

        void Delete(Guid id);

        UsuarioResponseModel GetById(Guid id);

        UsuarioResponseModel GetByCorreo(string correoAlternativo);
    }
}