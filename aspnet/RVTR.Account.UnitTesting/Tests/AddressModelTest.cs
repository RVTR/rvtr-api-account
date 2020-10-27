using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RVTR.Account.ObjectModel.Models;
using Xunit;

namespace RVTR.Account.UnitTesting.Tests
{
  public class AddressModelTest
  {
    public static readonly IEnumerable<object[]> Addresses = new List<object[]>
    {
      new object[]
      {
        new AddressModel()
        {
          Id = 0,
          City = "city",
          Country = "USA",
          PostalCode = "postalcode",
          StateProvince = "stateprovince",
          Street = "street",
          AccountId = 0,
          Account = null,
        }
      }
    };

    [Theory]
    [MemberData(nameof(Addresses))]
    public void Test_Create_AddressModel(AddressModel address)
    {
      var validationContext = new ValidationContext(address);
      var actual = Validator.TryValidateObject(address, validationContext, null, true);

      Assert.True(actual);
    }

    [Theory]
    [MemberData(nameof(Addresses))]
    public void Test_Validate_AddressModel(AddressModel address)
    {
      var validationContext = new ValidationContext(address);

      var x = address.Validate(validationContext);

      Assert.Empty(x);
    }
  }
}
