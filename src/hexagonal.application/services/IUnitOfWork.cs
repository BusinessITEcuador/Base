using hexagonal.domain.repositories.interfaces;
using hexagonal.domain.repositories.interfaces.generics;

namespace hexagonal.application.services
{
    public interface IUnitOfWork
    {
        IRepository<T> GetRepository<T>() where T : class;

        IUsuarioDomainRepository GetUsuarioRepository();

        ICuentaDomainRepository GetCuentaRepository();

        IGeneroDomainRepository GetGeneroRepository();

        ILogDomainRepository GetLogRepository();

        void SaveSync();

        void CleanTracker();

        void BeginTransaction();

        void CommitTransaction();

        void RollbackTransaction();
    }
}