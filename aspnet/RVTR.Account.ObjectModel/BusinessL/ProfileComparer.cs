/*using RVTR.Account.ObjectModel.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RVTR.Account.ObjectModel.BusinessL
{
  public class ProfileComparer : IEqualityComparer<ProfileModel>
  {
    public bool Equals(ProfileModel x, ProfileModel y)
    {
      //Check whether the compared objects reference the same data.
      if (Object.ReferenceEquals(x, y)) return true;

      //Check whether any of the compared objects is null.
      if (x is null || y is null)
        return false;

      //Check whether the products' properties are equal.
      return x.Id == y.Id;
    }

    public int GetHashCode(ProfileModel obj)
    {
      //Check whether the object is null
      if (obj is null) return 0;

      //Get hash code for the Id field if it is not null.
      return obj.Id == 0 ? 0 : obj.Id.GetHashCode();
    }
  }
}
*/
