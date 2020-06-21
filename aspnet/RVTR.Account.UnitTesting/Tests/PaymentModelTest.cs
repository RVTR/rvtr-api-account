using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RVTR.Account.ObjectModel.Models;
using Xunit;

namespace RVTR.Account.UnitTesting.Tests
{
  public class PaymentModelTest
  {
    public static readonly IEnumerable<Object[]> _payments = new List<Object[]>
    {
      new object[]
      {
        new PaymentModel()
        {
          Id = 0,
          CardExpirationDate=DateTime.Now,
          CardNumber="123456789132456",
          CardName = "name",
          AccountId = 1,
          Account = null
          
        }
      }
    };

    [Theory]
    [MemberData(nameof(_payments))]
    public void Test_Create_PaymentModel(PaymentModel payment)
    {
      var validationContext = new ValidationContext(payment);
      var actual = Validator.TryValidateObject(payment, validationContext, null, true);

      Assert.True(actual);
    }

    [Theory]
    [MemberData(nameof(_payments))]
    public void Test_Validate_PaymentModel(PaymentModel payment)
    {
      var validationContext = new ValidationContext(payment);

      Assert.Empty(payment.Validate(validationContext));
    }
  }
}
