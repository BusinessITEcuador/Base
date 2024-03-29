﻿using AutoMapper;
using hexagonal.application.models.usuario;
using hexagonal.application.services.usuario.interfaces;
using hexagonal.domain.entities;
using hexagonal.domain.repositories.interfaces;
using Microsoft.IdentityModel.Tokens;

namespace hexagonal.application.services.Usuario
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UsuarioService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public void Add(UsuarioRequestModel Usuario, Guid UsuarioId)
        {
            var repository = _unitOfWork.GetUsuarioRepository();
            UsuarioEntity UsuarioEntity = _mapper.Map<UsuarioEntity>(Usuario);
            UsuarioEntity.Id = Guid.NewGuid();
            UsuarioEntity.CreatedBy = UsuarioId;
            UsuarioEntity.Created = DateTime.Now;
            repository.AddSync(UsuarioEntity);

            //crear cuenta
            var cuentaRepository = _unitOfWork.GetCuentaRepository();
            var cuenta = cuentaRepository.FirstOrDefaultSync(p => p.Id == Usuario.Id);
            if (cuenta == null)
            {
                CuentaEntity cuentaEntity = new CuentaEntity();
                cuentaEntity.Id = Usuario.Id;
                cuentaEntity.Correo = Usuario.Correo;
                cuentaEntity.UsuarioId = UsuarioEntity.Id;
                cuentaEntity.CreatedBy = UsuarioId;
                cuentaEntity.Created = DateTime.Now;
                cuentaRepository.AddSync(cuentaEntity);
            }
            _unitOfWork.SaveSync();
        }

        public void Update(UsuarioRequestModel Usuario, Guid id, Guid UsuarioId)
        {
            var repository = _unitOfWork.GetUsuarioRepository();

            UsuarioEntity UsuarioEntity = repository.FirstOrDefaultSync(p => p.Id == id);
            UsuarioEntity UsuarioToEdit = _mapper.Map(Usuario, UsuarioEntity);
            UsuarioToEdit.LastModifiedBy = UsuarioId;
            UsuarioToEdit.LastModified = DateTime.Now;
            repository.UpdateSync(UsuarioToEdit);
            _unitOfWork.SaveSync();
        }

        public void Delete(Guid id)
        {
            var repository = _unitOfWork.GetUsuarioRepository();
            UsuarioEntity Usuario = repository.FirstOrDefaultSync(p => p.Id == id);
            repository.RemoveSync(Usuario);
            _unitOfWork.SaveSync();
        }

        IList<UsuarioResponseModel> IUsuarioService.GetAll()
        {
            IUsuarioDomainRepository repository = _unitOfWork.GetUsuarioRepository();
            return this._mapper.Map<IList<UsuarioResponseModel>>(repository.GetAllSync());
        }

        public UsuarioResponseModel GetById(Guid id)
        {
            IUsuarioDomainRepository repository = _unitOfWork.GetUsuarioRepository();
            return this._mapper.Map<UsuarioResponseModel>(repository.FirstOrDefaultSync(p => p.Id == id));
        }

        public UsuarioResponseModel GetByCorreo(string correo)
        {
            IUsuarioDomainRepository repository = _unitOfWork.GetUsuarioRepository();
            return this._mapper.Map<UsuarioResponseModel>(repository.FirstOrDefaultSync(
            p => p.Correo == correo));
        }

        public UsuarioResponseModel GetByCorreoAlternativo(string correoAlternativo)
        {
            if (!correoAlternativo.IsNullOrEmpty())
            {
                IUsuarioDomainRepository repository = _unitOfWork.GetUsuarioRepository();
                return this._mapper.Map<UsuarioResponseModel>(repository.FirstOrDefaultSync(
                p => p.Correo == correoAlternativo || p.CorreoAlternativo == correoAlternativo
                ));
            }
            else
            {
                return null;
            }
        }

        public UsuarioResponseModel ObtenerUsuarioPorCuentaId(Guid cuentaId)
        {
            IUsuarioDomainRepository repository = _unitOfWork.GetUsuarioRepository();
            ICuentaDomainRepository cuentaDomainRepository = _unitOfWork.GetCuentaRepository();
            var cuenta = cuentaDomainRepository.FirstOrDefaultSync(p => p.Id == cuentaId);
            if (cuenta == null)
            {
                return null;
            }
            return this._mapper.Map<UsuarioResponseModel>(repository.FirstOrDefaultSync(p => p.Id == cuenta.UsuarioId));
        }
    }
}