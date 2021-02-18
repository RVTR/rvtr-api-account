using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using RVTR.Account.Context.Repositories;
using RVTR.Account.Domain.Interfaces;
using RVTR.Account.Domain.Models;
using RVTR.Account.Service.Controllers;
using Xunit;

namespace RVTR.Account.Testing.Tests
{
  public class AccountControllerTest
  {
    // private readonly AccountController _controller;
    // private readonly ILogger<AccountController> _logger;
    // private readonly UnitOfWork _unitOfWork;

    // public AccountControllerTest()
    // {
    //   var loggerMock = new Mock<ILogger<AccountController>>();
    //   var repositoryMock = new Mock<AccountRepository>();
    //   var unitOfWorkMock = new Mock<UnitOfWork>();

    //   repositoryMock.Setup(m => m.Select("test@gmail.com")).Throws(new Exception());
    //   repositoryMock.Setup(m => m.SelectAll()).ReturnsAsync((IEnumerable<AccountModel>)null);
    //   unitOfWorkMock.Setup(m => m.Insert(It.IsAny<AccountModel>())).Returns(Task.CompletedTask);
    //   // unitOfWorkMock.Setup(m => m.GetAll<AccountModel>()).ReturnsAsync((IEnumerable<AccountModel>)null);
    //   unitOfWorkMock.Setup(m => m.Get<AccountModel>(0)).Throws(new Exception());
    //   unitOfWorkMock.Setup(m => m.Update(It.IsAny<AccountModel>()));
    //   unitOfWorkMock.Setup(m => m.AccountRepository).Returns(repositoryMock.Object);

    //   _logger = loggerMock.Object;
    //   _unitOfWork = (UnitOfWork)unitOfWorkMock.Object;
    //   _controller = new AccountController(_logger, _unitOfWork);
    // }

    // [Fact]
    // public async void Test_Controller_Delete()
    // {
    //   var resultFail = await _controller.Delete("fake@email.com");
    //   var resultPass = await _controller.Delete("Test@test.com");

    //   Assert.NotNull(resultFail);
    //   Assert.NotNull(resultPass);
    // }

    // [Fact]
    // public async void Test_Controller_Get()
    // {
    //   var resultMany = await _controller.Get();
    //   var resultFail = await _controller.Get("fake@email.com");
    //   var resultOne = await _controller.Get("Test@test.com");

    //   Assert.NotNull(resultMany);
    //   Assert.NotNull(resultFail);
    //   Assert.NotNull(resultOne);
    // }

    // [Fact]
    // public async void Test_Controller_Post()
    // {
    //   var resultPass = await _controller.Post(new AccountModel());

    //   Assert.NotNull(resultPass);
    // }

    // [Fact]
    // public async void Test_Controller_Put()
    // {
    //   var resultPass = await _controller.Put(new AccountModel());

    //   Assert.NotNull(resultPass);
    // }

    // [Fact]
    // public async void Test_404_Response()
    // {
    //   var result = await _controller.Get("fake@email.com");

    //   Assert.IsType<NotFoundObjectResult>(result);
    // }
  }
}
