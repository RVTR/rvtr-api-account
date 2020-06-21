using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RVTR.Account.ObjectModel.Models;

namespace RVTR.Account.DataContext.Repositories
{
  public class AccountRepository : GenericRepository<AccountModel>
  {
    public AccountRepository(AccountContext context):base(context)
    {
    }
    /// <summary>
    /// Account repository overriding generic repository's GetAll() method,
    /// retrieves all account info including address/payment/profiles/name
    /// </summary>
    /// <returns>Returns IEnumerable of AccountModel with address/payments/profiles/names included</returns>
    public override async Task<IEnumerable<AccountModel>> GetAll()
    {
      return await _context.Accounts
        .Include(x => x.Address)
        .Include(x => x.Payments)
        .Include(x => x.Profiles)
        .ThenInclude(x => x.Name).ToListAsync();
    }
    /// <summary>
    /// Account repository overriding generic repository's Get() method,
    /// retrieves account info including address/payment/profiles/name by accountId
    /// </summary>
    /// <param name="id">AccoundId</param>
    /// <returns>Returns AccountModel with address/payments/profiles/name included</returns>
    public override async Task<AccountModel> Get(int id)
    {
      return await _context.Accounts
        .Include(x => x.Address)
        .Include(x => x.Payments)
        .Include(x => x.Profiles)
        .ThenInclude(x => x.Name)
        .Where(x => x.Id == id)
        .FirstOrDefaultAsync();
    }
    /// <summary>
    /// Account Delete() method to remove an account from the database (delete cascades)
    /// </summary>
    /// <param name="id">AccoundId</param>
    public async override Task Delete(int id)
    {
      var ent = await Get(id);
      _context.Accounts.Remove(ent);
    }
  }
}
