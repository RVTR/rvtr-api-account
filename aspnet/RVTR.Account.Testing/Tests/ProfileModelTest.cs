using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RVTR.Account.Domain.Models;
using Xunit;

namespace RVTR.Account.Testing.Tests
{

  public class ProfileModelTest
  {
    private static DateTime adultAge = new DateTime(2003, 1, 30);
    public static readonly IEnumerable<object[]> Profiles = new List<object[]>
    {
      new object[]
      {
        new ProfileModel()
        {
          EntityId = 0,
          Email = "email@email.com",
          FamilyName = "Family",
          GivenName = "Given",
          Phone = "1234567890",
          DateOfBirth = adultAge,
          Type = "Adult",
          AccountModelId = 0
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

    /// <summary>
    /// Tests for phone number with dashes
    /// </summary>
    [Theory]
    [MemberData(nameof(Profiles))]
    public void Test_Create_AccountModel_BadEmail(ProfileModel profile)
    {
      profile = new ProfileModel()
      {
        EntityId = 0,
        Email = "email@email.com",
        FamilyName = "Family",
        GivenName = "Given",
        Phone = "123-456-7890",
        Type = "Adult",
        AccountModelId = 0
      };

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

    /// <summary>
    /// Tests for an invalid name
    /// </summary>
    [Fact]
    public void Test_Profile_BadName()
    {
      AccountModel account = new AccountModel("jim", " ", "abc@gmail.com", adultAge); //bad name given (empty string for last name)

      var validationContext = new ValidationContext(account.Profiles[0]);
      var actual = Validator.TryValidateObject(account.Profiles[0], validationContext, null, true);

      Assert.False(actual);
    }

    [Fact]
    public void Test_Profile_Minor()
    {
      ProfileModel profile = new ProfileModel("", "", "", true, DateTime.Today);
      Assert.False(profile.IsAdult);
    }

    [Fact]
    public void Test_Profile_Adult()
    {
      ProfileModel profile = new ProfileModel("", "", "", true, adultAge);
      Assert.True(profile.IsAdult);
    }
  }
}
