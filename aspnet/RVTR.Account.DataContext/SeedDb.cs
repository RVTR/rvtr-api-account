using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RVTR.Account.ObjectModel.Models;

namespace RVTR.Account.DataContext
{
  /// <summary>
  /// Represents the _Seed Database_
  /// </summary>
  public static class SeedDb
  {
    public static async Task Seed(AccountContext ctx)
    {
      if (ctx.Accounts.Count() > 0)
      {
        ctx.Accounts.RemoveRange(ctx.Accounts);
        await ctx.SaveChangesAsync();
      }
      await ctx.Accounts.AddAsync(new AccountModel
      {
        Id = -1,
        Email = "Test@test.com",
        Address = new AddressModel()
        {
          Id = -1,
          City = "City",
          Country = "Country",
          PostalCode = "21345",
          StateProvince = "Somewhere",
          Street = "123 elm street",
          AccountId = -1,
        },
        Name = "Name",
        Payments = new List<PaymentModel>()
        {
            new PaymentModel()
            {
                Id = -1,
                cardExpirationDate = new DateTime(),
                cardNumber = "xxxx-1234",
                cardName = "Visa",
                securityCode = "123"
            }
        },
        Profiles = new List<ProfileModel>()
        {
            new ProfileModel()
            {
                Id = -1,
                Email = "Test@test.com",
                familyName = "Jones",
                givenName = "Tom",
                Phone = "1234567891",
                Type = "Adult",
                AccountId = -1
            }
        }
      });
      await ctx.SaveChangesAsync();
    }
  }
}
