using RVTR.Account.ObjectModel.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RVTR.Account.DataContext.Repositories
{
  public class ProfileRepository : GenericRepository<ProfileModel>
  {
    public ProfileRepository(AccountContext context) : base(context)
    {
    }
  }
}
