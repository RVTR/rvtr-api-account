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
  public class AccountControllerTest
  {
    private readonly AccountController _controller;
    private readonly ILogger<AccountController> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public AccountControllerTest()
    {
      var loggerMock = new Mock<ILogger<AccountController>>();
      var repositoryMock = new Mock<IRepository<AccountModel>>();
      var unitOfWorkMock = new Mock<IUnitOfWork>();

      repositoryMock.Setup(m => m.DeleteAsync(0)).Throws(new Exception());
      repositoryMock.Setup(m => m.DeleteAsync(1)).Returns(Task.CompletedTask);
      repositoryMock.Setup(m => m.InsertAsync(It.IsAny<AccountModel>())).Returns(Task.CompletedTask);
      repositoryMock.Setup(m => m.SelectAsync()).ReturnsAsync((IEnumerable<AccountModel>)null);
      repositoryMock.Setup(m => m.SelectAsync(0)).Throws(new Exception());
      repositoryMock.Setup(m => m.SelectAsync(1)).ReturnsAsync((AccountModel)null);
      repositoryMock.Setup(m => m.SelectAsync(2)).ReturnsAsync(new AccountModel("Bob") { Id = 2});
      repositoryMock.Setup(m => m.Update(It.IsAny<AccountModel>()));
      unitOfWorkMock.Setup(m => m.Account).Returns(repositoryMock.Object);

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
      var resultFail = await _controller.Get(-5);
      var resultOne = await _controller.Get(-1);

      Assert.NotNull(resultMany);
      Assert.NotNull(resultFail);
      Assert.NotNull(resultOne);
    }

    [Fact]
    public async void Test_Controller_Post_OK()
    {
      //Arrange
      AccountModel accountModel = new AccountModel("Bob");

      //Act
      var resultPass = await _controller.Post(accountModel);

      //Assert
      Assert.IsType<AcceptedResult>(resultPass);
    }

    [Fact]
    public async void Test_Controller_Post_BadRequest()
    {
      //Arrange
      AccountModel accountModel = new AccountModel(); //Empty accountModel is bad input

      //Act
      var badRequest = await _controller.Post(accountModel);

      //Assert
      Assert.IsType<BadRequestObjectResult>(badRequest);
    }

    [Fact]
    public async void Test_Controller_Put_Accepted()
    {
      //Arrange
      AccountModel account = await _unitOfWork.Account.SelectAsync(2);
      account.Name = "Jim";

      //Act
      var resultPass = await _controller.Put(account);

      //Assert
      Assert.IsType<AcceptedResult>(resultPass);
    }

    [Fact]
    public async void Test_Controller_Put_BadRequest()
    {
      var badRequest = await _controller.Put(new AccountModel());

      Assert.IsType<BadRequestObjectResult>(badRequest);
    }

    [Fact]
    public async void Test_Controller_Put_NotFound()
    {
      var notFound = await _controller.Put(new AccountModel("Bob")); //Can't update a new account model not in the db

      Assert.IsType<NotFoundObjectResult>(notFound);
    }

    [Fact]
    public async void Test_404_Response()
    {
      var result = await _controller.Get(-100);

      Assert.IsType<NotFoundObjectResult>(result);
    }
  }
}
