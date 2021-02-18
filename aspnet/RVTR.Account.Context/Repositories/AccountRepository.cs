using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RVTR.Account.Domain.Interfaces;
using RVTR.Account.Domain.Models;

namespace RVTR.Account.Context.Repositories
{
  /// <summary>
  /// Represents the _Repository_ generic
  /// </summary>
  /// <typeparam name="TEntity"></typeparam>
  public class AccountRepository
  {
    private readonly AccountContext _context;
    public AccountRepository(AccountContext context)
    {
      _context = context;
    }

    public async Task<AccountModel> Select(string email)
    {
      return await _context.Accounts
      .Where(x => x.Email == email)
      .Include(x => x.Address)
      .Include(x => x.Profiles)
      .Include(x => x.Payments)
      .FirstOrDefaultAsync();
    }
    public async Task<IEnumerable<AccountModel>> SelectAll()
    {
      return await _context.Accounts
      .Include(x => x.Address)
      .Include(x => x.Profiles)
      .Include(x => x.Payments)
      .ToListAsync();
    }
  }
}
