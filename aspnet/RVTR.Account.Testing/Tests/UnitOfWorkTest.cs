using RVTR.Account.Context;
using RVTR.Account.Context.Repositories;
using Xunit;

namespace RVTR.Account.Testing.Tests
{
  public class UnitOfWorkTest : DataTest
  {
    [Fact]
    public async void Test_UnitOfWork_CommitAsync()
    {
      using var ctx = new AccountContext(Options);
      var unitOfWork = new UnitOfWork(ctx);
      var actual = await unitOfWork.CommitAsync();
      Assert.Equal(0, actual);
    }
  }
}
