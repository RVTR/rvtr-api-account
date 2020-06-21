
namespace RVTR.Account.ObjectModel.BusinessL
{
  public static class Helpers
  {
    /// <summary>
    /// Obscures all the credit card number returned to the caller
    /// </summary>
    /// <param name="ccNum">String of Credit Card Number</param>
    /// <returns>Returns Obscured Credit Card Number</returns>
    public static string ObscureCreditCardNum(string ccNum)
    {
      int left = ccNum.Length - 4;
      string sub = ccNum.Substring(left, 4);

      return $"************{sub}";
    }
  }
}
