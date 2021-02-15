using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RVTR.Account.Domain.Interfaces;
using RVTR.Account.Domain.Models;

namespace RVTR.Account.Context.Repositories
{
  /// <summary>
  /// Represents the _UnitOfWork_ repository
  /// </summary>
  public class UnitOfWork
  {
    private readonly AccountContext _context;
    private AccountRepository _accountRepository;

    public UnitOfWork(AccountContext context)
    {
      _context = context;
    }

    public AccountRepository AccountRepository
    {
      get
      {
        if(_accountRepository == null)
        {
          _accountRepository = new AccountRepository(_context);
        }
        return _accountRepository;
      }
    }

    /// <summary>
    /// Represents the _UnitOfWork_ `Commit` method
    /// </summary>
    /// <returns></returns>
    public async Task<int> CommitAsync() => await _context.SaveChangesAsync();
    public async Task<List<T>> GetAll<T>() where T : class
    {
      return await Task.Run(()=>_context.Set<T>().ToList());
    }
    public async Task<T> Get<T>(string email) where T : class
    {
      return await Task.Run(()=>_context.Set<T>().Find(email));
    }
    public async Task Insert<T>(T obj) where T : class
    {
      await Task.Run(() =>_context.Set<T>().Add(obj));
    }
    public async Task Update<T>(T obj) where T : class
    {
      await Task.Run(() =>_context.Set<T>().Attach(obj));
      _context.Entry(obj).State = EntityState.Modified;
    }

  }
}
