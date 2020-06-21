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
  public class AccountRepositoryTest
  {
    private static readonly SqliteConnection _connection = new SqliteConnection("Data Source=:memory:");
    private static readonly DbContextOptions<AccountContext> _options = new DbContextOptionsBuilder<AccountContext>().UseSqlite(_connection).Options;

    public static readonly IEnumerable<object[]> _records = new List<object[]>()
    {
      new object[]
      {
        new AccountModel() { Id = 1 }
      }
    };

    [Theory]
    [MemberData(nameof(_records))]
    public async void Test_AccountRepository_DeleteAsync(AccountModel account)
    {
      await _connection.OpenAsync();

      try
      {
        using (var ctx = new AccountContext(_options))
        {
          await ctx.Database.EnsureCreatedAsync();
          await ctx.Accounts.AddAsync(account);
          await ctx.SaveChangesAsync();
        }

        using (var ctx = new AccountContext(_options))
        {
          var accounts = new AccountRepository(ctx);

          await accounts.Delete(1);
          await ctx.SaveChangesAsync();

          Assert.Empty(await ctx.Accounts.ToListAsync());
        }
      }
      finally
      {
        await _connection.CloseAsync();
      }
    }

    [Fact]
    public async void Test_AccountRepository_GetAll()
    {
      var options = new DbContextOptionsBuilder<AccountContext>()
        .UseInMemoryDatabase(databaseName: "Test1A")
        .Options;

      using (var x = new AccountContext())
      {
        Assert.True(true);
      }

      using (var ctx = new AccountContext(options))
      {
        var accounts = new AccountRepository(ctx);
        var newAccount = new AccountModel()
        {
          Id = 0,
          Name = "Lucy Coupling"
        };
        await accounts.Add(newAccount);
        await ctx.SaveChangesAsync();
        var actual = await accounts.GetAll();

        Assert.Equal("Lucy Coupling", actual.First().Name);
      }
    }

    [Fact]
    public async void Test_ProfileRepository_Get()
    {
      var options = new DbContextOptionsBuilder<AccountContext>()
        .UseInMemoryDatabase(databaseName: "Test2A")
        .Options;

      using (var ctx = new AccountContext(options))
      {
        var accounts = new AccountRepository(ctx);
        var newAccount = new AccountModel()
        {
          Id = 0,
          Name = "Lucy Coupling"
        };
        await accounts.Add(newAccount);
        await ctx.SaveChangesAsync();
        var actual = await accounts.Get(1);

        Assert.Equal("Lucy Coupling", actual.Name);

      }
    }

  }
}
