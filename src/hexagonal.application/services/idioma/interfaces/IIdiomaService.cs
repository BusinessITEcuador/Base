using hexagonal.application.models.idioma;
using System;
using System.Collections.Generic;

namespace hexagonal.application.services.idioma.interfaces
{
    public interface IIdiomaService
    {
        IList<IdiomaResponseModel> GetAll();

        void Add(IdiomaRequestModel idioma, Guid usuarioId);

        void Update(IdiomaRequestModel idioma, Guid id, Guid usuarioId);

        void Delete(Guid id);

        IdiomaResponseModel GetById(Guid id);
    }
}
