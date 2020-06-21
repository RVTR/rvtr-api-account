using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using RVTR.Account.DataContext;
using RVTR.Account.DataContext.Repositories;
using RVTR.Account.ObjectModel.Models;
using Xunit;

namespace RVTR.Account.UnitTesting.Tests
{
  public class PaymentRepositoryTest
  {
    private static readonly SqliteConnection _connection = new SqliteConnection("Data Source=:memory:");
    private static readonly DbContextOptions<AccountContext> _options = new DbContextOptionsBuilder<AccountContext>().UseSqlite(_connection).Options;

    public static readonly IEnumerable<object[]> _records = new List<object[]>()
    {
      new object[]
      {
        new PaymentModel() { Id = 1 }
      }
    };

    [Theory]
    [MemberData(nameof(_records))]
    public async void Test_PaymentRepository_DeleteAsync(PaymentModel payment)
    {
      await _connection.OpenAsync();

      try
      {
        using (var ctx = new AccountContext(_options))
        {
          await ctx.Database.EnsureCreatedAsync();
          await ctx.Payments.AddAsync(payment);
          await ctx.SaveChangesAsync();
        }

        using (var ctx = new AccountContext(_options))
        {
          var payments = new PaymentRepository(ctx);

          await payments.Delete(1);
          await ctx.SaveChangesAsync();

          Assert.Empty(await ctx.Payments.ToListAsync());
        }
      }
      finally
      {
        await _connection.CloseAsync();
      }
    }
  }
}
