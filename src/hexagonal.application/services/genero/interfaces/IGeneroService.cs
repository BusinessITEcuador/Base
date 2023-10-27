using hexagonal.application.models.genero;

namespace hexagonal.application.services.genero.interfaces
{
    public interface IGeneroService
    {
        IList<GeneroResponseModel> GetAll();

        void Add(GeneroRequestModel Genero);

        void Update(GeneroRequestModel Genero, Guid id);

        void Delete(Guid id);

        GeneroResponseModel GetById(Guid id);
    }
}