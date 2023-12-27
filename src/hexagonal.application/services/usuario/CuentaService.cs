using AutoMapper;
using hexagonal.application.models.usuario;
using hexagonal.application.services.usuario.interfaces;
using hexagonal.domain.entities;
using hexagonal.domain.repositories.interfaces;

namespace hexagonal.application.services.Cuenta
{
    public class CuentaService : ICuentaService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CuentaService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public void Add(UsuarioRequestModel Usuario, Guid UsuarioId)
        {
            var repository = _unitOfWork.GetCuentaRepository();
            CuentaEntity CuentaEntity = _mapper.Map<CuentaEntity>(Usuario);
            CuentaEntity.CreatedBy = UsuarioId;
            CuentaEntity.Created = DateTime.Now;
            repository.AddSync(CuentaEntity);
            _unitOfWork.SaveSync();
        }

        public void AgregarCuenta(Guid id, string correo, Guid UsuarioId)
        {
            var repository = _unitOfWork.GetCuentaRepository();
            CuentaEntity CuentaEntity = new CuentaEntity();
            CuentaEntity.Id = id;
            CuentaEntity.Correo = correo;
            CuentaEntity.UsuarioId = UsuarioId;
            CuentaEntity.CreatedBy = id;
            CuentaEntity.Created = DateTime.Now;
            repository.AddSync(CuentaEntity);
            _unitOfWork.SaveSync();
        }

        public CuentaResponseModel GetById(Guid id)
        {
            ICuentaDomainRepository repository = _unitOfWork.GetCuentaRepository();
            return this._mapper.Map<CuentaResponseModel>(repository.FirstOrDefaultSync(p => p.Id == id));
        }
    }
}