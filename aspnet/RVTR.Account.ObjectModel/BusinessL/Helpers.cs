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
      if (ccNum.Length != 16)
        return null;

      return $"***********${ccNum.Substring(11, 16)}";
    }

    public static bool UpdateAccount(AccountModel account)
    {
      //Compare profiles

      //Compare payments

      return true;
    }
  }
}
