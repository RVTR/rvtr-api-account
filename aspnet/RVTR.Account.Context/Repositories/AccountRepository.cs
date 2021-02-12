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
  public class AccountRepository : Repository<AccountModel>, IAccountRepository
  {
    public AccountRepository(AccountContext context) : base(context) { }

    public override async Task<AccountModel> SelectAsync(int id) => await Db
      .Where(x => x.EntityId == id)
      .Include(x => x.Address)
      .Include(x => x.Profiles)
      .Include(x => x.Payments)
      .FirstOrDefaultAsync();

    public override async Task<IEnumerable<AccountModel>> SelectAsync() => await Db
      .Include(x => x.Address)
      .Include(x => x.Profiles)
      .Include(x => x.Payments)
      .ToListAsync();

    // Select an account by email instead of by ID, as is the case with SelectAsync(id)
    public virtual async Task<AccountModel> SelectByEmailAsync(string email) => await Db
      .Include(x => x.Address)
      .Include(x => x.Profiles)
      .Include(x => x.Payments)
      .FirstOrDefaultAsync(x => x.Email == email);
  }
}
