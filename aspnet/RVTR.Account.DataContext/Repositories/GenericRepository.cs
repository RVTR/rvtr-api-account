using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using RVTR.Account.ObjectModel.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RVTR.Account.DataContext.Repositories
{
  public abstract class GenericRepository<T>
    : IRepository<T> where T : class
  {
    protected AccountContext _context;
    public GenericRepository(AccountContext context)
    {
      this._context = context;
    }
    public async virtual Task<T> Add(T entity)
    {
      await _context.AddAsync(entity).ConfigureAwait(true);
      return entity;
    }

    public abstract Task Delete(int id);
    

    public async virtual Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate)
    {
      return await _context.Set<T>()
                .AsNoTracking()
                .AsQueryable()
                .Where(predicate)
                .ToListAsync();
    }

    public async virtual Task<T> Get(int id)
    {
      return await _context.FindAsync<T>(id);
    }

    public async virtual Task<IEnumerable<T>> GetAll()
    {
      return await _context.Set<T>()
                .AsNoTracking()
                .ToListAsync();
    }

    public async virtual Task<T> Update(T entity)
    {
      var ent = _context.Update(entity).Entity;
      return ent;
    }
  }
}
