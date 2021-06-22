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

    public static readonly IEnumerable<object[]> Payments = new List<object[]>
    {
      new object[]
      {
        new PaymentModel()
        {
          EntityId = 0,
          CardName = "Name",
          CardNumber = "4234123412341234",
          SecurityCode = "111",
          AccountModelId = 0
        }
      }
    };
    public PaymentControllerTest()
    {
      var loggerMock = new Mock<ILogger<PaymentController>>();
      var repositoryMock = new Mock<IRepository<PaymentModel>>();
      var unitOfWorkMock = new Mock<IUnitOfWork>();

      repositoryMock.Setup(m => m.DeleteAsync(0)).Throws(new Exception());
      repositoryMock.Setup(m => m.DeleteAsync(1)).Returns(Task.CompletedTask);
      repositoryMock.Setup(m => m.InsertAsync(It.IsAny<PaymentModel>())).Returns(Task.CompletedTask);
      repositoryMock.Setup(m => m.SelectAsync()).ReturnsAsync((IEnumerable<PaymentModel>)null);
      repositoryMock.Setup(m => m.SelectAsync(e => e.EntityId == 0)).Throws(new Exception());
      repositoryMock.Setup(m => m.SelectAsync(e => e.EntityId == 1)).ReturnsAsync((IEnumerable<PaymentModel>)null);
      repositoryMock.Setup(m => m.Update(It.IsAny<PaymentModel>()));
      unitOfWorkMock.Setup(m => m.Payment).Returns(repositoryMock.Object);

      _logger = loggerMock.Object;
      _unitOfWork = unitOfWorkMock.Object;
      _controller = new PaymentController(_logger, _unitOfWork);
    }

    //Demo purposes

    // [Theory]
    // [InlineData("jsmith@gmail.com", 1)]
    // [InlineData("fake@email.com", 0)]
    // public async void Test_Controller_Delete(string email, int id)
    // {
    //   var resultFail = await _controller.Delete(email, id);
    //   var resultPass = await _controller.Delete(email, id);

    //   Assert.NotNull(resultFail);
    //   Assert.NotNull(resultPass);
    // }

    // [Theory]
    // [InlineData("jsmith@gmail.com")]
    // [InlineData("fake@email.com")]
    // public async void Test_Controller_Get(string email)
    // {
    //   var resultMany = await _controller.Get(email);
    //   var resultFail = await _controller.Get(email);
    //   var resultOne = await _controller.Get(email);

    //   Assert.NotNull(resultMany);
    //   Assert.NotNull(resultFail);
    //   Assert.NotNull(resultOne);
    // }

    [Fact]
    public async void Test_Controller_Post()
    {
      var resultPass = await _controller.Post(new PaymentModel());

      Assert.NotNull(resultPass);
    }

    [Fact]
    public async void Test_Controller_Put()
    {
      var resultPass = await _controller.Put(new PaymentModel());

      Assert.NotNull(resultPass);
    }
  }
}