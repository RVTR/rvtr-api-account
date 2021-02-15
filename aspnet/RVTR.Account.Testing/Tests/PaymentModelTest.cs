using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RVTR.Account.Domain.Models;
using Xunit;

namespace RVTR.Account.Testing.Tests
{
  public class PaymentModelTest
  {
    public static readonly IEnumerable<object[]> Payments = new List<object[]>
    {
      new object[]
      {
        new PaymentModel()
        {
          EntityId = 0,
          CardName = "Name",
          CardNumber = "4234123412341234",
          SecurityCode = "111",
          AccountModelId = 0
        }
      }
    };

    [Theory]
    [MemberData(nameof(Payments))]
    public void Test_Create_PaymentModel(PaymentModel payment)
    {
      var validationContext = new ValidationContext(payment);
      var actual = Validator.TryValidateObject(payment, validationContext, null, true);

      Assert.True(actual);
    }

    /// <summary>
    /// Tests for bad card number
    /// </summary>
    [Theory]
    public void Test_Create_AccountModel_BadEmail()
    {
      PaymentModel payment = new PaymentModel()
      {
        EntityId = 0,
        CardName = "Name",
        CardNumber = "abc", //bad card number given
        SecurityCode = "111",
        AccountModelId = 0
      };

      var validationContext = new ValidationContext(payment);
      var actual = Validator.TryValidateObject(payment, validationContext, null, true);

      Assert.False(actual);
    }


    [Theory]
    [MemberData(nameof(Payments))]
    public void Test_Validate_PaymentModel(PaymentModel payment)
    {
      var validationContext = new ValidationContext(payment);

      Assert.Empty(payment.Validate(validationContext));
    }
  }
}
