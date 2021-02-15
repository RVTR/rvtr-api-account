using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using RVTR.Account.Domain.Interfaces;
using RVTR.Account.Domain.Models;
using RVTR.Account.Service.Controllers;
using Xunit;

namespace RVTR.Account.UnitTesting.Tests
{
  public class PaymentControllerTest
  {
    private readonly PaymentController _controller;
    private readonly ILogger<PaymentController> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public PaymentControllerTest()
    {
      var loggerMock = new Mock<ILogger<PaymentController>>();
      var repositoryMock = new Mock<IRepository<PaymentModel>>();
      var unitOfWorkMock = new Mock<IUnitOfWork>();

      repositoryMock.Setup(m => m.DeleteAsync(0)).Throws(new Exception());
      repositoryMock.Setup(m => m.DeleteAsync(1)).Returns(Task.CompletedTask);
      repositoryMock.Setup(m => m.InsertAsync(It.IsAny<PaymentModel>())).Returns(Task.CompletedTask);
      repositoryMock.Setup(m => m.SelectAsync()).ReturnsAsync((IEnumerable<PaymentModel>)null);
      repositoryMock.Setup(m => m.SelectAsync(0)).Throws(new Exception());
      repositoryMock.Setup(m => m.SelectAsync(1)).ReturnsAsync((PaymentModel)null);
      repositoryMock.Setup(m => m.Update(It.IsAny<PaymentModel>()));
      unitOfWorkMock.Setup(m => m.Payment).Returns(repositoryMock.Object);

      _logger = loggerMock.Object;
      _unitOfWork = unitOfWorkMock.Object;
      _controller = new PaymentController(_logger, _unitOfWork);
    }

    [Theory]
    public async void Test_Controller_Delete()
    {
      var resultFail = await _controller.Delete(0);
      var resultPass = await _controller.Delete(1);

      Assert.NotNull(resultFail);
      Assert.NotNull(resultPass);
    }

    [Theory]
    public async void Test_Controller_Get()
    {
      var resultMany = await _controller.Get();
      var resultFail = await _controller.Get(-5);
      var resultOne = await _controller.Get(-1);

      Assert.NotNull(resultMany);
      Assert.NotNull(resultFail);
      Assert.NotNull(resultOne);
    }

    [Theory]
    public async void Test_Controller_Post()
    {
      var resultPass = await _controller.Post(new PaymentModel());

      Assert.NotNull(resultPass);
    }

    [Theory]
    public async void Test_Controller_Put()
    {
      var resultPass = await _controller.Put(new PaymentModel());

      Assert.NotNull(resultPass);
    }
  }
}
