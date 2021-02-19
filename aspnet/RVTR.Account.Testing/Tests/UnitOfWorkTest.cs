using System;
using Microsoft.EntityFrameworkCore;
using RVTR.Account.Context;
using RVTR.Account.Context.Repositories;
using RVTR.Account.Domain.Models;
using Xunit;

namespace RVTR.Account.Testing.Tests
{
  public class UnitOfWorkTest : DataTest
  {
    [Fact]
    public void Test_AccountInstance()
    {
      using (var ctx = new AccountContext(Options))
      {
        UnitOfWork _unit = new UnitOfWork(ctx);
        var account = _unit.AccountRepository;
        Assert.IsType<AccountRepository>(account);
      }
    }
  }
}
