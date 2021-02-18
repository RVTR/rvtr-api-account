using RVTR.Account.Context;
using RVTR.Account.Context.Repositories;
using Xunit;

namespace RVTR.Account.Testing.Tests
{
  public class AccountRepositoryTest : DataTest
  {
    [Fact]
    public void Test_Repository_Select()
    {
      using var ctx = new AccountContext(Options);

      var accounts = new AccountRepository(ctx);

      Assert.NotNull(accounts.Select("ddowd97@gmail.com"));
    }

    [Fact]
    public async void Test_Repository_SelectAll()
    {
      using var ctx = new AccountContext(Options);

      var accounts = new AccountRepository(ctx);

      var actual = await accounts.SelectAll();

      Assert.NotNull(actual);
    }
  }
}
