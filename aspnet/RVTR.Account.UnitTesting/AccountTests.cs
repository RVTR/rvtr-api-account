using Xunit;
using RVTR.Account.ObjectModel.Models;
using RVTR.Account.ObjectModel.Validation;
using System.ComponentModel.DataAnnotations;

namespace RVTR.Account.UnitTesting
{
  public class AccountTests
  {
    // [Fact]
    // public void ValidateAccountDetails_AccountDetailsMembersAreValid()
    // {
    //   //Arrange
    //   var sut = new AccountDetails() {
    //     AccountDetailsID = "id1",
    //     AccountType = "accounttype1",
    //     AccountRewards = new AccountRewards()
    //   };
    //   //Act
    //   var resultAccountDetailsID = sut.AccountDetailsID.GetType() == typeof(string);
    //   var resultAccountType = sut.AccountType.GetType() == typeof(string);
    //   //AccountRewards resultAccountRewards = sut.AccountRewards.GetType() == typeof(AccountRewards);

    //   //Assert
      
    // }
    [Fact]
    public void ValidateDateOfBirth()
    {
      ValidateDateOfBirth obj = new ValidateDateOfBirth();
      var dob = new System.DateTime(2800, 1, 14); // January 14, 2800
      Assert.Equal(false, obj.IsValid(dob));
      // Assert.IsType(ValidationResult, obj.IsValid(dob));
      // Assert.IsType<ValidationResult>(obj.IsValid(dob));
    }

    [Fact] //Test with InMemory Db
    public void Add_accounts_to_database()
    {
    }
  }
}
