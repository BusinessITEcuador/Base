using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace $safeprojectname$.repositories.interfaces.generics
{
  public interface IRepository<T> where T : class
  {
    Task<IList<T>> GetAll(params Expression<Func<T, object>>[] navigationProperties);
    IList<T> GetAllSync(params Expression<Func<T, object>>[] navigationProperties);
    Task<IList<T>> GetAllWhere(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] navigationProperties);
    IList<T> GetAllWhereSync(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] navigationProperties);
    Task<IList<T>> GetAllWhereOrder(Expression<Func<T, bool>> where, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, params Expression<Func<T, object>>[] navigationProperties);
    Task<T> GetSingle(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] navigationProperties);
    void AddRangeSync(ICollection<T> items);
    void AddSync(params T[] items);
    Task UpdateAsync(params T[] items);
    void UpdateSync(params T[] items);
    void UpdateRangeSync(ICollection<T> items);
    Task RemoveAsync(params T[] items);
    void RemoveSync(params T[] items);
    void RemoveRangeSync(ICollection<T> items);
    T FirstOrDefaultSync(Expression<Func<T, bool>> where);
    T FirstOrDefaultSync(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] navigationProperties);
    T SingleSync(Expression<Func<T, bool>> where, string entityMessage, params Expression<Func<T, object>>[] navigationProperties);
    T SingleSync(Expression<Func<T, bool>> where, string entityMessage);
    T SingleSync(Expression<Func<T, bool>> where);
    IList<T> GetAllWhereSync(Expression<Func<T, bool>> where);
    int Count(Expression<Func<T, bool>> predicates);
    void UpdateSync(ICollection<T> items);
  }
}
