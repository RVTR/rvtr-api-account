using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
          EntityId = 0,
          Address = new AddressModel(),
          Payments = new List<PaymentModel>(),
          Profiles = new List<ProfileModel>
            {
              new ProfileModel("John", "Smith", "jsmith@gmail.com", true)
            },
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
    [Theory]
    [InlineData("Jim", "Peters", "abcd")]
    public void Test_Create_AccountModel_BadEmail(string firstName, string lastName,string email)
    {
      AccountModel account = new AccountModel(firstName, lastName, email); //bad email given

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
  }
}
