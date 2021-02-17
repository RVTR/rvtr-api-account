using RVTR.Account.Context;
using RVTR.Account.Context.Repositories;
using RVTR.Account.Domain.Models;
using Xunit;

namespace RVTR.Account.Testing.Tests
{
  public class UnitOfWorkTest : DataTest
  {
    private readonly PaymentModel _payment = new PaymentModel() { EntityId = 3 };
    private readonly ProfileModel _profile = new ProfileModel() { FamilyName = "FN", GivenName = "GN", EntityId = 3, Email = "anemail@random.com", Phone = "123456789", Type = "" };
    private readonly AddressModel _address = new AddressModel() { EntityId = 3, AccountId = 3 };
    [Theory]
    [MemberData(nameof(_payment))]
    [MemberData(nameof(_profile))]
    [MemberData(nameof(_address))]
    public async void Test_UnitOfWork_CommitAsync()
    {
      using var ctx = new AccountContext(Options);
      var unitOfWork = new UnitOfWork(ctx);
      var actual = await unitOfWork.CommitAsync();

      Assert.NotNull(unitOfWork.Account);
      Assert.NotNull(unitOfWork.Profile);
      Assert.Equal(0, actual);
    }
  }
}
