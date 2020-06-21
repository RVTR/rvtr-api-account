using System.Linq;
using Microsoft.EntityFrameworkCore;
using RVTR.Account.DataContext;
using RVTR.Account.DataContext.Repositories;
using RVTR.Account.ObjectModel.Models;
using Xunit;

namespace RVTR.Account.UnitTesting.Tests
{
  public class GenericRepositoryTest
  {

    [Fact]
    public async void Test_GenericRepository_Find()
    {
      var options = new DbContextOptionsBuilder<AccountContext>()
        .UseInMemoryDatabase(databaseName: "Test1G")
        .Options;

      using (var ctx = new AccountContext(options))
      {
        var profiles = new ProfileRepository(ctx);
        var newProfile = new ProfileModel()
        {
          Id = 10,
          Email = "abc@gmail.com",
          Name = null,
          Phone = "1234567897",
          Age = "Adult",
          AccountId = 1,
          Account = null
        };
        await profiles.Add(newProfile);
        await ctx.SaveChangesAsync();
        var actual = await profiles.Find(profile => profile.AccountId == 1);
        var phone = actual.First().Phone;

        Assert.Equal("1234567897", phone);
      }
    }

    [Fact]
    public async void Test_GenericRepository_Update()
    {
      var options = new DbContextOptionsBuilder<AccountContext>()
        .UseInMemoryDatabase(databaseName: "Test2G")
        .Options;

      using (var ctx = new AccountContext(options))
      {
        var profiles = new ProfileRepository(ctx);
        var payment = new PaymentRepository(ctx);
        var newProfile = new ProfileModel()
        {
          Id = 0,
          Email = "abc@gmail.com",
          Name = null,
          Phone = "1234567897",
          Age = "Adult",
          AccountId = 1,
          Account = null
        };
        var updateProfile = new ProfileModel()
        {
          Id = 1,
          Email = "test@gmail.com",
          Name = null,
          Phone = "2222222222",
          Age = "Child",
          AccountId = 1,
          Account = null
        };
        await profiles.Add(newProfile);
        await ctx.SaveChangesAsync();
        ctx.Entry<ProfileModel>(newProfile).State = EntityState.Detached;
        await profiles.Update(updateProfile);
        await ctx.SaveChangesAsync();
        var actual = await profiles.GetAll();
        var payments = await payment.GetAll();
        var phone = actual.First().Phone;

        Assert.Equal("2222222222", phone);
        Assert.Empty(payments);
      }
    }
  }
}
