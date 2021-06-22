using RVTR.Account.Domain.Validators;
using Xunit;

namespace RVTR.Account.Testing.Tests
{
  public class AddressValidatorTest
  {
    [Fact]
    public void Test_NoResults_AddressValidator()
    {
      string address = "alldnalcihuiqic";
      Assert.False(ValidatorSwitch.validate(address,0).Result);
    }

    [Fact]
    public void Test_BadResult_AddressValidator()
    {
      string address = "Texas";
      Assert.False(ValidatorSwitch.validate(address,0).Result);
    }

    [Fact]
    public void Test_GoodResult_AddressValidator()
    {
      string address = "11730 Plaza America Dr. 2nd Floor Reston, VA";
      Assert.True(ValidatorSwitch.validate(address,0).Result);
    }
  }
}
