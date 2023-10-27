using AutoMapper;
using hexagonal.application.models.idioma;
using hexagonal.application.services.idioma.interfaces;
using hexagonal.domain.entities;
using hexagonal.domain.repositories.interfaces;
using System;
using System.Collections.Generic;

namespace hexagonal.application.services.idioma
{
    public class IdiomaService : IIdiomaService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public IdiomaService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public void Add(IdiomaRequestModel idioma, Guid usuarioId)
        {
            var repository = _unitOfWork.GetIdiomaRepository();
            IdiomaEntity idiomaEntity = _mapper.Map<IdiomaEntity>(idioma);
            idiomaEntity.CreatedBy = usuarioId;
            idiomaEntity.Created = DateTime.Now;
            repository.AddSync(idiomaEntity);
            _unitOfWork.SaveSync();
        }

        public void Update(IdiomaRequestModel idioma, Guid id, Guid usuarioId)
        {
            var repository = _unitOfWork.GetIdiomaRepository();

            IdiomaEntity idiomaEntity = repository.FirstOrDefaultSync(p => p.Id == id);
            if (idiomaEntity != null)
            {
                IdiomaEntity idiomaToEdit = _mapper.Map(idioma, idiomaEntity);
                idiomaToEdit.LastModifiedBy = usuarioId;
                idiomaToEdit.LastModified = DateTime.Now;
                repository.UpdateSync(idiomaToEdit);
                _unitOfWork.SaveSync();
            }
            else
            {
                // Manejo de caso en el que el idioma no se encuentra.
            }
        }

        public void Delete(Guid id)
        {
            var repository = _unitOfWork.GetIdiomaRepository();
            IdiomaEntity idioma = repository.FirstOrDefaultSync(p => p.Id == id);
            if (idioma != null)
            {
                repository.RemoveSync(idioma);
                _unitOfWork.SaveSync();
            }
            else
            {
                // Manejo de caso en el que el idioma no se encuentra.
            }
        }

        public IList<IdiomaResponseModel> GetAll()
        {
            IIdiomaDomainRepository repository = _unitOfWork.GetIdiomaRepository();
            return _mapper.Map<IList<IdiomaResponseModel>>(repository.GetAllSync());
        }

        public IdiomaResponseModel GetById(Guid id)
        {
            IIdiomaDomainRepository repository = _unitOfWork.GetIdiomaRepository();
            return _mapper.Map<IdiomaResponseModel>(repository.FirstOrDefaultSync(p => p.Id == id));
        }
    }
}
