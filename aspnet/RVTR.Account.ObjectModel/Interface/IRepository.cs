using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RVTR.Account.ObjectModel.Interface
{
  /// <summary>
  /// Methods that needs to be defined in the Repository Class
  /// </summary>
  public interface IRepository<TEntity>
  {
    Task<TEntity> Add(TEntity entity);
    Task<TEntity> Update(TEntity entity);
    Task<TEntity> Get(int id);
    Task<IEnumerable<TEntity>> GetAll();
    Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate);
    Task Delete(int id);
  }
}
