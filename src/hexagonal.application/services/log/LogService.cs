using AutoMapper;
using hexagonal.application.models.log;
using hexagonal.application.services.log.interfaces;
using hexagonal.domain.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hexagonal.application.services.log
{
    public class LogService : ILogService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public LogService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public void Add(LogRequestModel Log)
        {
            var repository = _unitOfWork.GetLogRepository();
            LogEntity logEntity = _mapper.Map<LogEntity>(Log);
            logEntity.Fecha = DateTime.Now;
            repository.AddSync(logEntity);
            _unitOfWork.SaveSync();
        }

    }
}
