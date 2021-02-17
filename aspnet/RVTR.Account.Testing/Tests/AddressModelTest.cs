using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RVTR.Account.Domain.Models;
using Xunit;

namespace RVTR.Account.Testing.Tests
{
  public class AddressModelTest
  {
    public static readonly IEnumerable<object[]> Addresses = new List<object[]>
    {
      new object[]
      {
        new AddressModel()
        {
          EntityId = 0,
          City = "City",
          Country = "USA",
          PostalCode = "11111",
          StateProvince = "NC",
          Street = "street",
          AccountId = 0,
          Account = new AccountModel(),
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

    /// <summary>
    /// Tests false if bad country name given
    /// </summary>
    [Theory]
    [MemberData(nameof(Addresses))]
    public void Test_Create_AccountModel_BadCountry()
    {
      AddressModel address = new AddressModel()
      {
        EntityId = 0,
        City = "City",
        Country = "USAD", //Bad country name
        PostalCode = "11111",
        StateProvince = "NC",
        Street = "street",
        AccountId = 0,
        Account = new AccountModel(),
      };

      var validationContext = new ValidationContext(address);
      var actual = Validator.TryValidateObject(address, validationContext, null, true);

      Assert.False(actual);
    }


    /// <summary>
    /// Tests false if bad country zip code given
    /// </summary>
    [Theory]
    [MemberData(nameof(Addresses))]
    public void Test_Create_AccountModel_BadPostCode()
    {
      AddressModel address = new AddressModel()
      {
        EntityId = 0,
        City = "City",
        Country = "USA",
        PostalCode = "abc", //Bad post code
        StateProvince = "NC",
        Street = "street",
        AccountId = 0,
        Account = new AccountModel(),
      };

      var validationContext = new ValidationContext(address);
      var actual = Validator.TryValidateObject(address, validationContext, null, true);

      Assert.False(actual);
    }

    /// <summary>
    /// Tests false if bad state abbreviation given
    /// </summary>
    [Theory]
    [MemberData(nameof(Addresses))]
    public void Test_Create_AccountModel_BadStateAbbr()
    {
      AddressModel address = new AddressModel()
      {
        EntityId = 0,
        City = "City",
        Country = "USA",
        PostalCode = "11111",
        StateProvince = "NCa", //Bad state
        Street = "street",
        AccountId = 0,
        Account = new AccountModel(),
      };

      var validationContext = new ValidationContext(address);
      var actual = Validator.TryValidateObject(address, validationContext, null, true);

      Assert.False(actual);
    }


    [Theory]
    [MemberData(nameof(Addresses))]
    public void Test_Validate_AddressModel(AddressModel address)
    {
      var validationContext = new ValidationContext(address);

      Assert.Empty(address.Validate(validationContext));
    }
  }
}
