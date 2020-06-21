using RVTR.Account.ObjectModel.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RVTR.Account.ObjectModel.BusinessL
{
  public static class Helpers
  {

    public static string ObscureCreditCardNum(string ccNum)
    {
      int left = ccNum.Length - 4;
      string sub = ccNum.Substring(left, 4);
      return $"************{sub}";
    }
  }
}
