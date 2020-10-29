using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using RVTR.Account.ObjectModel.Interfaces;
using RVTR.Account.ObjectModel.Models;
using RVTR.Account.WebApi.Controllers;
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
      repositoryMock.Setup(m => m.SelectAsync(2)).ReturnsAsync(new PaymentModel(DateTime.Now, "4123-4123-4123-4123", "111", "Alex", new AccountModel("Alex")) { Id = 2 });
      repositoryMock.Setup(m => m.Update(It.IsAny<PaymentModel>()));
      unitOfWorkMock.Setup(m => m.Payment).Returns(repositoryMock.Object);

      _logger = loggerMock.Object;
      _unitOfWork = unitOfWorkMock.Object;
      _controller = new PaymentController(_logger, _unitOfWork);
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
      var resultFail = await _controller.Get(-5);
      var resultOne = await _controller.Get(-1);

      Assert.NotNull(resultMany);
      Assert.NotNull(resultFail);
      Assert.NotNull(resultOne);
    }

    [Fact]
    public async void Test_Controller_Post_OK()
    {
      PaymentModel payment = new PaymentModel()
      {
        Id = 0,
        CardExpirationDate = DateTime.Now,
        CardName = "Name",
        CardNumber = "4234-1234-1234-1234",
        SecurityCode = "111",
        AccountId = 0,
        Account = new AccountModel(),
      };

      var resultPass = await _controller.Post(payment);

      Assert.IsType<AcceptedResult>(resultPass);
    }

    [Fact]
    public async void Test_Controller_Post_BadRequest()
    {
      var resultPass = await _controller.Post(new PaymentModel());

      Assert.IsType<BadRequestObjectResult>(resultPass);
    }

    [Fact]
    public async void Test_Controller_Put_OK()
    {
      //Arrange
      PaymentModel payment = await _unitOfWork.Payment.SelectAsync(2);
      payment.CardName = "Name";

      //Act
      var resultPass = await _controller.Put(payment);

      //Assert
      Assert.IsType<AcceptedResult>(resultPass);
    }

    [Fact]
    public async void Test_Controller_Put_BadRequest()
    {
      //Arrange
      PaymentModel payment = new PaymentModel(DateTime.Now, "4123-4123-4123-4123asdfasdfasdf", "111", "Alex", new AccountModel("Alex")) { Id = 2 };

      //Act
      var resultPass = await _controller.Put(payment);

      //Assert
      Assert.IsType<BadRequestObjectResult>(resultPass);
    }

    [Fact]
    public async void Test_Controller_Put_NotFound()
    {
      //Arrange
      PaymentModel payment = new PaymentModel(DateTime.Now, "4123-4123-4123-4123", "111", "Alex", new AccountModel("Alex"));


      //Act
      var resultPass = await _controller.Put(payment);

      //Assert
      Assert.IsType<NotFoundObjectResult>(resultPass);
    }
  }
}
