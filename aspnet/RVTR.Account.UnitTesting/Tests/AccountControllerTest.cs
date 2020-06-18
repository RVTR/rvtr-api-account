using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using RVTR.Account.DataContext;
using RVTR.Account.DataContext.Repositories;
using RVTR.Account.ObjectModel.Models;
using RVTR.Account.WebApi.Controllers;
using Xunit;

namespace RVTR.Account.UnitTesting.Tests
{
  public class AccountControllerTest
  {
    private static readonly SqliteConnection _connection = new SqliteConnection("Data Source=:memory:");
    private static readonly DbContextOptions<AccountContext> _options = new DbContextOptionsBuilder<AccountContext>().UseSqlite(_connection).Options;
    private readonly AccountController _controller;
    private readonly ILogger<AccountController> _logger;
    private readonly UnitOfWork _unitOfWork;

    public AccountControllerTest()
    {
      var contextMock = new Mock<AccountContext>(_options);
      var loggerMock = new Mock<ILogger<AccountController>>();
      var repositoryMock = new Mock<AccountRepository>(new AccountContext(_options));
      var unitOfWorkMock = new Mock<UnitOfWork>(contextMock.Object);

      repositoryMock.Setup(m => m.Delete(0)).Throws(new Exception());
      repositoryMock.Setup(m => m.Delete(1)).Returns(Task.FromResult(1));
      repositoryMock.Setup(m => m.Update(It.IsAny<AccountModel>())).Returns(Task.FromResult<AccountModel>(null));
      repositoryMock.Setup(m => m.GetAll()).Returns(Task.FromResult<IEnumerable<AccountModel>>(null));
      repositoryMock.Setup(m => m.Get(0)).Throws(new Exception());
      repositoryMock.Setup(m => m.Get(1)).Returns(Task.FromResult<AccountModel>(null));
      repositoryMock.Setup(m => m.Update(It.IsAny<AccountModel>()));
      unitOfWorkMock.Setup(m => m.AccountRepository).Returns(repositoryMock.Object);

      _logger = loggerMock.Object;
      _unitOfWork = unitOfWorkMock.Object;
      _controller = new AccountController(_logger, _unitOfWork);
    }

    [Fact]
    public async void Test_Controller_Delete()
    {
      var resultFail = await _controller.Delete(0);
      var resultPass = await _controller.Delete(1);

      Assert.NotNull(resultFail);
      Assert.NotNull(resultPass);
    }

    [Fact]
    public async void Test_Controller_Get()
    {
      var resultMany = await _controller.Get();
      var resultFail = await _controller.Get(0);
      var resultOne = await _controller.Get(1);

      Assert.NotNull(resultMany);
      Assert.NotNull(resultFail);
      Assert.NotNull(resultOne);
    }

    [Fact]
    public async void Test_Controller_Post()
    {
      var resultPass = await _controller.Post(new AccountModel());

      Assert.NotNull(resultPass);
    }

    [Fact]
    public async void Test_Controller_Put()
    {
      var resultPass = await _controller.Put(new AccountModel());

      Assert.NotNull(resultPass);
    }
  }
}
