using AutoMapper;
using hexagonal.application.models.genero;
using hexagonal.application.services.genero.interfaces;
using hexagonal.domain.entities;
using hexagonal.domain.repositories.interfaces;

namespace hexagonal.application.services.Genero
{
    public class GeneroService : IGeneroService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GeneroService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public void Add(GeneroRequestModel Genero)
        {
            var repository = _unitOfWork.GetGeneroRepository();
            GeneroEntity generoEntity = _mapper.Map<GeneroEntity>(Genero);
            generoEntity.CreatedBy = default(Guid);
            generoEntity.Created = DateTime.Now;
            repository.AddSync(generoEntity);
            _unitOfWork.SaveSync();
        }

        public void Update(GeneroRequestModel Genero, Guid id)
        {
            var repository = _unitOfWork.GetGeneroRepository();

            GeneroEntity generoEntity = repository.FirstOrDefaultSync(p => p.Id == id);
            GeneroEntity generoToEdit = _mapper.Map(Genero, generoEntity);
            generoToEdit.LastModifiedBy = default(Guid);
            generoToEdit.LastModified = DateTime.Now;
            repository.UpdateSync(generoToEdit);
            _unitOfWork.SaveSync();
        }

        public void Delete(Guid id)
        {
            var repository = _unitOfWork.GetGeneroRepository();
            GeneroEntity Genero = repository.FirstOrDefaultSync(p => p.Id == id);
            repository.RemoveSync(Genero);
            _unitOfWork.SaveSync();
        }

        IList<GeneroResponseModel> IGeneroService.GetAll()
        {
            IGeneroDomainRepository repository = _unitOfWork.GetGeneroRepository();
            return this._mapper.Map<IList<GeneroResponseModel>>(repository.GetAllSync());
        }

        public GeneroResponseModel GetById(Guid id)
        {
            IGeneroDomainRepository repository = _unitOfWork.GetGeneroRepository();
            return this._mapper.Map<GeneroResponseModel>(repository.FirstOrDefaultSync(p => p.Id == id));
        }
    }
}