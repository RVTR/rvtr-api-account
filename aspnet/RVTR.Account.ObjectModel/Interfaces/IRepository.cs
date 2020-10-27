using RVTR.Account.ObjectModel.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RVTR.Account.ObjectModel.Interfaces
{
  public interface IRepository<TEntity> where TEntity : class
  {
    Task DeleteAsync(int id);
    Task InsertAsync(TEntity entry);
    Task<IEnumerable<TEntity>> SelectAsync();
    Task<TEntity> SelectAsync(int id);
    Task<TEntity> SelectByEmailAsync(string email); // Implementation of required method to select by email in AccountController.cs
    void Update(TEntity entry);
  }
}
