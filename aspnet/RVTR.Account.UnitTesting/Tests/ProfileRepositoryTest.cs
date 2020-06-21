using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using RVTR.Account.DataContext;
using RVTR.Account.DataContext.Repositories;
using RVTR.Account.ObjectModel.Models;
using Xunit;

namespace RVTR.Account.UnitTesting.Tests
{
  public class ProfileRepositoryTest
  {
    private static readonly SqliteConnection _connection = new SqliteConnection("Data Source=:memory:");
    private static readonly DbContextOptions<AccountContext> _options = new DbContextOptionsBuilder<AccountContext>().UseSqlite(_connection).Options;

    public static readonly IEnumerable<object[]> _records = new List<object[]>()
    {
      new object[]
      {
        new ProfileModel() { Id = 1, Email = "email" }
      }
    };

    [Theory]
    [MemberData(nameof(_records))]
    public async void Test_ProfileRepository_DeleteAsync(ProfileModel profile)
    {
      await _connection.OpenAsync();

      try
      {
        using (var ctx = new AccountContext(_options))
        {
          await ctx.Database.EnsureCreatedAsync();
          await ctx.Profiles.AddAsync(profile);
          await ctx.SaveChangesAsync();
        }

        using (var ctx = new AccountContext(_options))
        {
          var profiles = new ProfileRepository(ctx);

          await profiles.Delete(1);
          await ctx.SaveChangesAsync();

          Assert.Empty(await ctx.Profiles.ToListAsync());
        }
      }
      finally
      {
        await _connection.CloseAsync();
      }
    }

    [Fact]
    public async void Test_ProfileRepository_GetAll()
    {
      var options = new DbContextOptionsBuilder<AccountContext>()
        .UseInMemoryDatabase(databaseName: "Test1")
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
        var actual = await profiles.GetAll();

        Assert.Equal(10, actual.First().Id);
      }
    }

    [Fact]
    public async void Test_ProfileRepository_Get()
    {
      var options = new DbContextOptionsBuilder<AccountContext>()
        .UseInMemoryDatabase(databaseName: "Test2")
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
        var actual = await profiles.Get(10);

        Assert.Equal(10, actual.Id);
      }
    }
  }
}
