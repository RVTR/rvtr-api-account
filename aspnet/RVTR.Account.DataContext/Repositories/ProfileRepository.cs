using Microsoft.EntityFrameworkCore;
using RVTR.Account.ObjectModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RVTR.Account.DataContext.Repositories
{
  public class ProfileRepository : GenericRepository<ProfileModel>
  {
    public ProfileRepository(AccountContext context) : base(context)
    {
    }
    public override async Task<IEnumerable<ProfileModel>> GetAll()
    {
      return await _context.Profiles
        .Include(x => x.Name)
        .ToListAsync();
    }
    public override async Task<ProfileModel> Get(int id)
    {
      return await _context.Profiles
        .Include(x => x.Name)
        .Where(x => x.Id == id)
        .FirstOrDefaultAsync();
    }
  }
}
