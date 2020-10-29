using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RVTR.Account.ObjectModel.Models;
using Xunit;

namespace RVTR.Account.UnitTesting.Tests
{
  public class ProfileModelTest
  {
    public static readonly IEnumerable<object[]> Profiles = new List<object[]>
    {
      new object[]
      {
        new ProfileModel()
        {
          Id = 0,
          Email = "email@email.com",
          familyName = "Family",
          givenName = "Given",
          Phone = "1234567890",
          Type = "Adult",
          AccountId = 0,
          Account = new AccountModel(),
        }
      }
    };

    [Theory]
    [MemberData(nameof(Profiles))]
    public void Test_Create_ProfileModel(ProfileModel profile)
    {
      var validationContext = new ValidationContext(profile);
      var actual = Validator.TryValidateObject(profile, validationContext, null, true);

      Assert.True(actual);
    }

    [Theory]
    [MemberData(nameof(Profiles))]
    public void Test_Validate_ProfileModel(ProfileModel profile)
    {
      var validationContext = new ValidationContext(profile);

      Assert.Empty(profile.Validate(validationContext));
    }
  }
}
