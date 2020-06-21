using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using RVTR.Account.DataContext;
using RVTR.Account.DataContext.Repositories;
using RVTR.Account.ObjectModel.BusinessL;
using RVTR.Account.ObjectModel.Models;
using RVTR.Account.WebApi.Controllers;
using Xunit;

namespace RVTR.Account.UnitTesting.Tests
{
  public class AccountControllerTest
  {
    private class Mocks
    {
      public Mock<AccountContext> _accountContext;
      public Mock<ILogger<AccountController>> _logger;
      public Mock<AccountRepository> _repository;
      public Mock<UnitOfWork> _unitOfWork;

      public Mocks()
      {
        SqliteConnection _connection = new SqliteConnection("Data Source=:memory:");
        DbContextOptions<AccountContext> _options = new DbContextOptionsBuilder<AccountContext>().UseSqlite(_connection).Options;

        this._accountContext = new Mock<AccountContext>(_options);
        this._logger = new Mock<ILogger<AccountController>>();
        this._repository = new Mock<AccountRepository>(this._accountContext.Object);
        this._unitOfWork = new Mock<UnitOfWork>(_accountContext.Object);
        this._unitOfWork.Setup(m => m.AccountRepository).Returns(this._repository.Object);
      }
    }
    private AccountController NewAccountController(Mocks mocks)
    {
      return new AccountController(mocks._logger.Object, mocks._unitOfWork.Object);
    }

    [Fact]
    public async void Delete_ById()
    {
      var mocks = new Mocks();
      mocks._repository.Setup(m => m.Delete(1)).Returns(Task.FromResult(new AccountModel()));

      var _controller = NewAccountController(mocks);
      var result = await _controller.Delete(1);
      Assert.IsType<OkResult>(result);
    }

    [Fact]
    public async void Delete_ByIdError()
    {
      var mocks = new Mocks();
      mocks._repository.Setup(m => m.Delete(0)).Throws(new Exception());

      var _controller = NewAccountController(mocks);
      var result = await _controller.Delete(0);
      Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async void Get_All()
    {
      var mocks = new Mocks();
      IEnumerable<AccountModel> newAccount = new List<AccountModel>() { new AccountModel() { Payments = new List<PaymentModel>() { new PaymentModel() { CardNumber = "1234567891234567" } } } };
      mocks._repository.Setup(m => m.GetAll()).Returns(Task.FromResult(newAccount));

      var _controller = NewAccountController(mocks);
      var result = await _controller.Get();
      Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async void Get_ById()
    {
      var mocks = new Mocks();
      AccountModel newAccount = new AccountModel() { Payments = new List<PaymentModel>() { new PaymentModel() { CardNumber = "1234567891234567" } } };
      mocks._repository.Setup(m => m.Get(1)).Returns(Task.FromResult(newAccount));

      var _controller = NewAccountController(mocks);
      var result = await _controller.Get(1);
      Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async void Get_ByIdNullError()
    {
      var mocks = new Mocks();
      AccountModel newAccount = null;
      mocks._repository.Setup(m => m.Get(1)).Returns(Task.FromResult(newAccount));

      var _controller = NewAccountController(mocks);
      var result = await _controller.Get(1);
      Assert.IsType<NotFoundObjectResult>(result);
    }


    [Fact]
    public async void Get_ByIdError()
    {
      var mocks = new Mocks();
      mocks._repository.Setup(m => m.Get(1)).Returns(Task.FromResult(new AccountModel()));

      var _controller = NewAccountController(mocks);
      var result = await _controller.Get(1);
      Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async void Post_ByAccount()
    {
      var mocks = new Mocks();
      var newAccount = new AccountModel();
      mocks._repository.Setup(m => m.Add(newAccount)).Returns(Task.FromResult(newAccount));

      var _controller = NewAccountController(mocks);
      var result = await _controller.Post(newAccount);
      Assert.IsType<AcceptedResult>(result);
    }

    [Fact]
    public async void Put_ByAccount()
    {
      var mocks = new Mocks();
      var newAccount = new AccountModel();
      mocks._repository.Setup(m => m.Update(newAccount)).Returns(Task.FromResult(newAccount));

      var _controller = NewAccountController(mocks);
      var result = await _controller.Put(newAccount);
      Assert.IsType<AcceptedResult>(result);
    }
  }
}


