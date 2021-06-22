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
      var repositoryMock = new Mock<IRepository<AccountModel>>();
      var unitOfWorkMock = new Mock<IUnitOfWork>();

      repositoryMock.Setup(m => m.DeleteAsync(0)).Throws(new Exception());
      repositoryMock.Setup(m => m.DeleteAsync(1)).Returns(Task.CompletedTask);
      repositoryMock.Setup(m => m.InsertAsync(It.IsAny<AccountModel>())).Returns(Task.CompletedTask);
      repositoryMock.Setup(m => m.SelectAsync()).ReturnsAsync((IEnumerable<AccountModel>)null);
      repositoryMock.Setup(m => m.SelectAsync(e => e.EntityId == 0)).Throws(new Exception());
      repositoryMock.Setup(m => m.SelectAsync(e => e.EntityId == 1)).ReturnsAsync((IEnumerable<AccountModel>)null);
      repositoryMock.Setup(m => m.Update(It.IsAny<AccountModel>()));
      unitOfWorkMock.Setup(m => m.Account).Returns(repositoryMock.Object);

      _logger = loggerMock.Object;
      _unitOfWork = unitOfWorkMock.Object;
      _controller = new AccountController(_logger, _unitOfWork);
    }

    [Fact]
    public async void Test_Controller_Delete()
    {
      var resultFail = await _controller.DeleteAccount("fake@email.com");
      var resultPass = await _controller.DeleteAccount("Test@test.com");

      Assert.NotNull(resultFail);
      Assert.NotNull(resultPass);
    }

    [Fact]
    public async void Test_Controller_Get()
    {
      var resultMany = await _controller.GetAccounts();
      var resultFail = await _controller.GetAccountByEmail("fake@email.com");
      var resultOne = await _controller.GetAccountByEmail("Test@test.com");

      Assert.NotNull(resultMany);
      Assert.NotNull(resultFail);
      Assert.NotNull(resultOne);
    }

    [Fact]
    public async void Test_Controller_Post()
    {
      var resultPass = await _controller.AddAccount(new AccountModel());

      Assert.NotNull(resultPass);
    }

    [Fact]
    public async void Test_Controller_Put()
    {
      var resultPass = await _controller.UpdateAccount(new AccountModel());

      Assert.NotNull(resultPass);
    }

    [Fact]
    public async void Test_404_Response()
    {
      var result = await _controller.GetAccountByEmail("fake@email.com");

      Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact] 
    public async void Test_Controller_DeleteProfile()
    {
      var resultFail = await _controller.DeactivateProfile("fake@email.com");
      var resultPass = await _controller.DeactivateProfile("Test@test.com");

      Assert.NotNull(resultFail);
      Assert.NotNull(resultPass);
    }

    [Fact]
    public async void Test_Controller_PutProfile()
    {
      var resultPass = await _controller.UpdateProfile(new ProfileModel());

      Assert.NotNull(resultPass);
    }
  }
}
