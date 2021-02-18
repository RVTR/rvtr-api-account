using System;
using Microsoft.EntityFrameworkCore;
using RVTR.Account.Context;
using RVTR.Account.Context.Repositories;
using RVTR.Account.Domain.Models;
using Xunit;

namespace RVTR.Account.Testing.Tests
{
  public class UnitOfWorkTest
  {
    protected readonly DbContextOptions<AccountContext> Options;
    protected UnitOfWork unitOfWork;
    public UnitOfWorkTest()
    {
      unitOfWork = new UnitOfWork(new AccountContext(Options));
    }

    [Fact]
    public void Test_AccountRepo()
    {
      var actual = unitOfWork.AccountRepository;
      Assert.IsType<AccountRepository>(actual);
    }

    // [Fact]
    // public async void Test_CommitAsync()
    // {
    //   var actual = await unitOfWork.CommitAsync();
    //   Assert.Equal(0, actual);
    // }

    // [Fact]
    // public async void Test_GetAll()
    // {
    //   var actual = await unitOfWork.GetAll<T>();
    //   Assert.Collection<T>(actual);
    // }

  }
}
