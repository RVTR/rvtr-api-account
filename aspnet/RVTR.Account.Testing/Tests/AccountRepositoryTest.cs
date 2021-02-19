using RVTR.Account.Context;
using RVTR.Account.Context.Repositories;
using Xunit;

namespace RVTR.Account.Testing.Tests
{
  public class AccountRepositoryTest : DataTest
  {
    [Fact]
    public async void Test_SelectAll()
    {
      using var ctx = new AccountContext(Options);
      var accounts = new AccountRepository(ctx);
      Assert.NotEmpty(await accounts.SelectAll());
    }

    [Fact]
    public async void Test_Select()
    {
      using var ctx = new AccountContext(Options);
      var accounts = new AccountRepository(ctx);
      var actual = await accounts.Select("ddowd97@gmail.com");
      Assert.NotNull(actual);
    }
  }
}
