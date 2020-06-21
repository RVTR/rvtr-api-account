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
    /// <summary>
    /// Profile repository overriding generic repository's GetAll() method,
    /// retrieves all profile info including name
    /// </summary>
    /// <returns>Returns IEnumerable of ProfileModeo with names included</returns>
    public override async Task<IEnumerable<ProfileModel>> GetAll()
    {
      return await _context.Profiles
        .Include(x => x.Name)
        .ToListAsync();
    }
    /// <summary>
    /// Profile repository overriding generic repository's Get() method,
    /// retrieves profile info including name
    /// </summary>
    /// <param name="id">ProfileId</param>
    /// <returns>Returns ProfileModel with name included</returns>
    public override async Task<ProfileModel> Get(int id)
    {
      return await _context.Profiles
        .Include(x => x.Name)
        .Where(x => x.Id == id)
        .FirstOrDefaultAsync();
    }
    /// <summary>
    /// Profile Delete() method to remove a profile from the database (delete cascades)
    /// </summary>
    /// <param name="id">ProfileId</param>
    public async override Task Delete(int id)
    {
      var ent = await Get(id);
      _context.Profiles.Remove(ent);
    }
  }
}
