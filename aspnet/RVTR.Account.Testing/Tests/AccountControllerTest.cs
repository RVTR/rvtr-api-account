using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using RVTR.Account.Domain.Interfaces;
using RVTR.Account.Domain.Models;
using RVTR.Account.Service.Controllers;
using Xunit;

namespace RVTR.Account.Testing.Tests
{
  public class AccountControllerTest
  {
    private readonly AccountController _controller;
    private readonly ILogger<AccountController> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public AccountControllerTest()
    {
      var loggerMock = new Mock<ILogger<AccountController>>();
      var repositoryMock = new Mock<IAccountRepository>();
      var unitOfWorkMock = new Mock<IUnitOfWork>();

      repositoryMock.Setup(m => m.DeleteAsync(0)).Throws(new Exception());
      repositoryMock.Setup(m => m.DeleteAsync(1)).Returns(Task.CompletedTask);
      repositoryMock.Setup(m => m.InsertAsync(It.IsAny<AccountModel>())).Returns(Task.CompletedTask);
      repositoryMock.Setup(m => m.SelectAsync()).ReturnsAsync((IEnumerable<AccountModel>)null);
      repositoryMock.Setup(m => m.SelectAsync(0)).Throws(new Exception());
      repositoryMock.Setup(m => m.SelectAsync(1)).ReturnsAsync((AccountModel)null);
      repositoryMock.Setup(m => m.Update(It.IsAny<AccountModel>()));
      unitOfWorkMock.Setup(m => m.Account).Returns(repositoryMock.Object);

      _logger = loggerMock.Object;
      _unitOfWork = unitOfWorkMock.Object;
      _controller = new AccountController(_logger, _unitOfWork);
    }

    [Theory]
    [MemberData(nameof(AccountControllerTest))]
    public async void Test_Controller_Delete()
    {
      var resultFail = await _controller.Delete("fake@email.com");
      var resultPass = await _controller.Delete("Test@test.com");

      Assert.NotNull(resultFail);
      Assert.NotNull(resultPass);
    }

    [Theory]
    [MemberData(nameof(AccountControllerTest))]
    public async void Test_Controller_Get()
    {
      var resultMany = await _controller.Get();
      var resultFail = await _controller.Get("fake@email.com");
      var resultOne = await _controller.Get("Test@test.com");

      Assert.NotNull(resultMany);
      Assert.NotNull(resultFail);
      Assert.NotNull(resultOne);
    }

    [Theory]
    public async void Test_Controller_Post()
    {
      var resultPass = await _controller.Post(new AccountModel());

      Assert.NotNull(resultPass);
    }

    [Theory]
    public async void Test_Controller_Put()
    {
      var resultPass = await _controller.Put(new AccountModel());

      Assert.NotNull(resultPass);
    }
  }
}
