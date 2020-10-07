using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RVTR.Account.ObjectModel.Models;
namespace RVTR.Account.DataContext.Repositories
{
  /// <summary>
  /// Represents the _Repository_ generic
  /// </summary>
  /// <typeparam name="TEntity"></typeparam>
  public class AccountRepository : Repository<AccountModel>
  {

    public AccountRepository(AccountContext context) : base(context) { }

    public async Task<AccountModel> SelectAsync(string email) => await _db
      .Where(x => x.Email == email)
      .Include(x => x.Address)
      .Include(x => x.Profiles)
      .Include(x => x.Payments)
      .FirstOrDefaultAsync();

    public override async Task<IEnumerable<AccountModel>> SelectAsync() => await _db
      .Include(x => x.Address)
      .Include(x => x.Profiles)
      .Include(x => x.Payments)
      .ToListAsync();
  }
}
