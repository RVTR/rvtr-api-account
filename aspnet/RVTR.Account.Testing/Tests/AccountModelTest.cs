using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using RVTR.Account.Domain.Models;
using Xunit;

namespace RVTR.Account.Testing.Tests
{
  public class AccountModelTest
  {
    public static readonly IEnumerable<object[]> Accounts = new List<object[]>
    {
      new object[]
      {
        new AccountModel()
        {
          EntityID = 0,
          Address = new AddressModel(),
          FirstName = "FirstName",
          LastName = "LastName",
          Payments = new List<PaymentModel>(),
          Profiles = new List<ProfileModel>(),
          Email = "test@gmail.com"
        }
      }
    };

    [Theory]
    [MemberData(nameof(Accounts))]
    public void Test_Create_AccountModel(AccountModel account)
    {
      var validationContext = new ValidationContext(account);
      var actual = Validator.TryValidateObject(account, validationContext, null, true);

      Assert.True(actual);
    }


    /// <summary>
    /// Tests for an invalid email
    /// </summary>
    /// <param name="account"></param>
    [Fact]
    public void Test_Create_AccountModel_BadEmail()
    {
      AccountModel account = new AccountModel("Jim","jefferys", "abcd"); //bad email given

      var validationContext = new ValidationContext(account);
      var actual = Validator.TryValidateObject(account, validationContext, null, true);

      Assert.False(actual);
    }

    /// <summary>
    /// Tests for an invalid email
    /// </summary>
    /// <param name="account"></param>
    [Fact]
    public void Test_Create_AccountModel_BadName()
    {
      AccountModel account = new AccountModel("jim","jimmy", "abcd@gmail.com"); //bad name given (lower case first lettter)

      var validationContext = new ValidationContext(account);
      var actual = Validator.TryValidateObject(account, validationContext, null, true);

      Assert.False(actual);
    }

    [Theory]
    [MemberData(nameof(Accounts))]
    public void Test_Validate_AccountModel(AccountModel account)
    {
      var validationContext = new ValidationContext(account);

      Assert.Empty(account.Validate(validationContext));
    }

    [Fact]
    public void Test_Create_Account_Profile_Creation()
    {
      AccountModel account = new AccountModel("Jim","Jimmy", "abcd@gmail.com");
      var profile = account.Profiles.ToList().Last();

      Assert.IsType<ProfileModel>(profile);
      Assert.True(profile.IsAccountHolder);
    }

  }
}
