using RVTR.Account.Context;
using RVTR.Account.Context.Repositories;
using Xunit;

namespace RVTR.Account.Testing.Tests
{
  public class AccountRepositoryTest : DataTest
  {
    [Fact]
    public async void Test_Repository_SelectAsync()
    {
      using var ctx = new AccountContext(Options);

      var accounts = new AccountRepository(ctx);

      var actual = await accounts.SelectAsync();

      Assert.NotEmpty(actual);
    }

    [Fact]
    public async void Test_Repository_SelectAsync_ById()
    {
      using var ctx = new AccountContext(Options);

      var accounts = new AccountRepository(ctx);

      var actual = await accounts.SelectAsync(1);

      Assert.NotNull(actual);
    }

    [Fact]
    public async void Test_Repository_SelectByEmailAsync()
    {
      using var ctx = new AccountContext(Options);

      var accounts = new AccountRepository(ctx);

      var actual = await accounts.SelectByEmailAsync("j@gmail.com");

      Assert.NotNull(actual);
    }
  }
}
