using Microsoft.EntityFrameworkCore;
using RVTR.Account.ObjectModel.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RVTR.Account.DataContext.Repositories
{
  public abstract class GenericRepository<T>
    : IRepository<T> where T : class
  {
    protected AccountContext _context;
    protected GenericRepository(AccountContext context)
    {
      this._context = context;
    }
    /// <summary>
    /// Generic Add() method to add new data into the database
    /// </summary>
    /// <param name="entity">Object entity using the generic Add()</param>
    /// <returns>Returns the object entity added to the database</returns>
    public async virtual Task<T> Add(T entity)
    {
      await _context.AddAsync(entity).ConfigureAwait(true);
      return entity;
    }
    /// <summary>
    /// Abstract delete method for Account/Payment/ProfileRepository to override
    /// </summary>
    /// <param name="id">id of the entity being deleted</param>
    public abstract Task Delete(int id);
    /// <summary>
    /// Generic Find() method to retrieve specific information through expression from the database
    /// </summary>
    /// <param name="predicate">Ex: p => p.AccountId == 1</param>
    /// <returns>Returns the object entity called to the database</returns>
    public async virtual Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate)
    {
      return await _context.Set<T>()
                .AsNoTracking()
                .AsQueryable()
                .Where(predicate)
                .ToListAsync();
    }
    /// <summary>
    /// Generic Get() method to retrieve information through id from the database
    /// </summary>
    /// <param name="id">id of the entity being called</param>
    /// <returns>Returns the object entity called to the database</returns>
    public async virtual Task<T> Get(int id)
    {
      return await _context.FindAsync<T>(id);
    }
    /// <summary>
    /// Generic GetAll() method to retrieve informations from the database
    /// </summary>
    /// <returns>Return IEnumerable object called to the database</returns>
    public async virtual Task<IEnumerable<T>> GetAll()
    {
      return await _context.Set<T>()
                .AsNoTracking()
                .ToListAsync();
    }
    /// <summary>
    /// Generic Update() method to update information in the database
    /// </summary>
    /// <param name="entity">Entity object being updated</param>
    /// <returns>Return Object entity updated</returns>
    public async virtual Task<T> Update(T entity)
    {
      var ent = _context.Update(entity).Entity;
      return ent;
    }
  }
}
