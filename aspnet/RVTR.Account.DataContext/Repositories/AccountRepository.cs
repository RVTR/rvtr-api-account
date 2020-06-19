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

    public override async Task<IEnumerable<AccountModel>> GetAll()
    {
      return await _context.Accounts
        .Include(x => x.Address)
        .Include(x => x.Payments)
        .Include(x => x.Profiles)
        .ThenInclude(x => x.Name).ToListAsync();
    }
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

    public async override Task Delete(int id)
    {
      var ent = await Get(id);
      _context.Accounts.Remove(ent);
    }
  }
}
