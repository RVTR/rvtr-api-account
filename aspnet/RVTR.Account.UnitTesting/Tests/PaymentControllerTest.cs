using System;
using System.Collections.Generic;
using System.Linq;
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
  public class PaymentControllerTest
  {
    private class Mocks
    {
      public Mock<AccountContext> _accountContext;
      public Mock<ILogger<PaymentController>> _logger;
      public Mock<PaymentRepository> _repository;
      public Mock<UnitOfWork> _unitOfWork;

      public Mocks()
      {
        SqliteConnection _connection = new SqliteConnection("Data Source=:memory:");
        DbContextOptions<AccountContext> _options = new DbContextOptionsBuilder<AccountContext>().UseSqlite(_connection).Options;

        this._accountContext = new Mock<AccountContext>(_options);
        this._logger = new Mock<ILogger<PaymentController>>();
        this._repository = new Mock<PaymentRepository>(this._accountContext.Object);
        this._unitOfWork = new Mock<UnitOfWork>(_accountContext.Object);
        this._unitOfWork.Setup(m => m.PaymentRepository).Returns(this._repository.Object);
      }
    }
    private PaymentController NewPaymentController(Mocks mocks)
    {
      return new PaymentController(mocks._logger.Object, mocks._unitOfWork.Object);
    }

    [Fact]
    public async void Delete_ById()
    {
      var mocks = new Mocks();
      mocks._repository.Setup(m => m.Delete(1)).Returns(Task.FromResult(new PaymentModel()));

      var _controller = NewPaymentController(mocks);
      var result = await _controller.Delete(1);
      Assert.IsType<OkResult>(result);
    }

    [Fact]
    public async void Delete_ByIdError()
    {
      var mocks = new Mocks();
      mocks._repository.Setup(m => m.Delete(0)).Throws(new Exception());

      var _controller = NewPaymentController(mocks);
      var result = await _controller.Delete(0);
      Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async void Get_PaymentNoIdWithExistingPayment()
    {
      var mocks = new Mocks();
      IEnumerable<PaymentModel> newPayment = new List<PaymentModel>() { new PaymentModel() { CardNumber = "1234567891234567" } };
      mocks._repository.Setup(m => m.GetAll()).Returns(Task.FromResult(newPayment));

      var _controller = NewPaymentController(mocks);
      var result = await _controller.Get(null);
      Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async void Get_PaymentNoIdNoPayments()
    {
      var mocks = new Mocks();
      IEnumerable<PaymentModel> newPayment = new List<PaymentModel>();
      mocks._repository.Setup(m => m.GetAll()).Returns(Task.FromResult(newPayment));

      var _controller = NewPaymentController(mocks);
      var result = await _controller.Get(null);
      Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async void Get_PaymentAccountIdWithExistingPayments()
    {
      var mocks = new Mocks();
      var accountId = 1;
      IEnumerable<PaymentModel> newPayment = new List<PaymentModel>() { new PaymentModel() { CardNumber = "1234567891234567" } };
      mocks._repository.Setup(m => m.Find(p => p.AccountId == accountId)).Returns(Task.FromResult(newPayment));

      var _controller = NewPaymentController(mocks);
      var result = await _controller.Get(accountId);
      Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async void Post_ByPayment()
    {
      var mocks = new Mocks();
      var newPayment = new PaymentModel();
      mocks._repository.Setup(m => m.Add(newPayment)).Returns(Task.FromResult(newPayment));

      var _controller = NewPaymentController(mocks);
      var result = await _controller.Post(newPayment);
      Assert.IsType<AcceptedResult>(result);
    }
  }
}
