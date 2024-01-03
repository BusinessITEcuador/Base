using hexagonal.application.models.log;

namespace hexagonal.application.services.log.interfaces
{
    public interface ILogService
    {
        void Add(LogRequestModel Log);
    }
}