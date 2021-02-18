using RVTR.Account.Context;
using RVTR.Account.Context.Repositories;
using RVTR.Account.Domain.Models;
using Xunit;

namespace RVTR.Account.Testing.Tests
{
  public class AccountRepositoryTest : DataTest
  {
    [Theory]
    [InlineData(1)]
    public async void Test_Repository_SelectAsync_ById(int id)
    {
      using var ctx = new AccountContext(Options);

      var accounts = new AccountRepository(ctx);

      var actual = await accounts.SelectAsync(id);

      Assert.NotNull(actual);
    }

    [Theory]
    [InlineData("jsmith@gmail.com")]
    public async void Test_Repository_SelectByEmailAsync(string email)
    {
      using var ctx = new AccountContext(Options);

      var accounts = new AccountRepository(ctx);

      var actual = await accounts.SelectByEmailAsync(email);

      Assert.NotNull(actual);
    }
  }
}
