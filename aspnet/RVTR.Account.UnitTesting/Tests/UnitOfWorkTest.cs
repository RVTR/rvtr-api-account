using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using RVTR.Account.DataContext;
using RVTR.Account.DataContext.Repositories;
using Xunit;

namespace RVTR.Account.UnitTesting.Tests
{
  public class UnitOfWorkTest
  {
    private static readonly SqliteConnection _connection = new SqliteConnection("Data Source=:memory:");
    private static readonly DbContextOptions<AccountContext> _options = new DbContextOptionsBuilder<AccountContext>().UseSqlite(_connection).Options;

    [Fact]
    public async void Test_UnitOfWork_CommitAsync()
    {
      await _connection.OpenAsync();

      try
      {
        using (var ctx = new AccountContext(_options))
        {
          await ctx.Database.EnsureCreatedAsync();
        }

        using (var ctx = new AccountContext(_options))
        {
          var unitOfWork = new UnitOfWork(ctx);
          await unitOfWork.Complete();

          Assert.NotNull(unitOfWork.AccountRepository);
          Assert.NotNull(unitOfWork.ProfileRepository);
          //Assert.Equal(0, actual);
        }
      }
      finally
      {
        await _connection.CloseAsync();
      }
    }
  }
}
