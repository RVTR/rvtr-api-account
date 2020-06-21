using RVTR.Account.ObjectModel.BusinessL;
using Xunit;

namespace RVTR.Account.UnitTesting.Tests
{
  public class HelpersTest
  {
    public static string ccNum = "1234567891234567";

    [Fact]
    public void Test_Helpers_ObscureCreditCardNum()
    {
      // Act
      string obscuredCard = Helpers.ObscureCreditCardNum(ccNum);

      // Assert
      Assert.Equal("************4567",obscuredCard);
    }
  }
}
