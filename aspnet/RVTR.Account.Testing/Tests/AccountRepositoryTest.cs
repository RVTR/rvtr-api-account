using RVTR.Account.Context;
using RVTR.Account.Context.Repositories;
using RVTR.Account.Domain.Models;
using Xunit;

namespace RVTR.Account.Testing.Tests
{
  public class AccountRepositoryTest : DataTest
  {
    private readonly AccountModel _account = new AccountModel() { EntityId = 3 };
    [Theory]
    [MemberData(nameof(_account))]
    public async void Test_Repository_SelectAsync_ById()
    {
      using var ctx = new AccountContext(Options);

      var accounts = new AccountRepository(ctx);

      var actual = await accounts.SelectAsync(1);

      Assert.NotNull(actual);
    }

    [Theory]
    [MemberData(nameof(_account))]
    public async void Test_Repository_SelectByEmailAsync()
    {
      using var ctx = new AccountContext(Options);

      var accounts = new AccountRepository(ctx);

      var actual = await accounts.SelectByEmailAsync("ddowd97@gmail.com");

      Assert.NotNull(actual);
    }
  }
}
