using hexagonal.application.models.log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hexagonal.application.services.log.interfaces
{
    public interface ILogService
    {
        void Add(LogRequestModel Log);
    }
}
