using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RVTR.Account.ObjectModel.Interfaces;

namespace RVTR.Account.DataContext.Repositories
{
  /// <summary>
  /// Represents the _Repository_ generic
  /// </summary>
  /// <typeparam name="TEntity"></typeparam>
  public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
  {
    protected readonly DbSet<TEntity> Db;

    public Repository(AccountContext context)
    {
      Db = context.Set<TEntity>();
    }

    // Generic 'Get' method to be overridden by AccountRepository.cs for use in AccountController.cs
    public virtual async Task<TEntity> SelectByEmailAsync(string email) => await SelectAsync(0);

    public virtual async Task DeleteAsync(int id) => Db.Remove(await SelectAsync(id));

    public virtual async Task InsertAsync(TEntity entry) => await Db.AddAsync(entry).ConfigureAwait(true);

    public virtual async Task<IEnumerable<TEntity>> SelectAsync() => await Db.ToListAsync();

    public virtual async Task<TEntity> SelectAsync(int id) => await Db.FindAsync(id).ConfigureAwait(true);

    public virtual void Update(TEntity entry) => Db.Update(entry);
  }
}
