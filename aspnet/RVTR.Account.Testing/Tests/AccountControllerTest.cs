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
    private readonly ILogger<AccountController> _logger;
    private readonly UnitOfWork _unitOfWork;
    private AccountController _controller;

    public AccountControllerTest()
    {
      var unitOfWorkMock = new Mock<UnitOfWork>();
      _controller = new Mock<AccountController>(unitOfWorkMock.Object).Object;
    }

    [Fact]
    public async void Test_Controller_Delete()
    {
      var resultFail = await _controller.Delete("fake@email.com");
      var resultPass = await _controller.Delete("Test@test.com");

      Assert.NotNull(resultFail);
      Assert.NotNull(resultPass);
    }

    [Fact]
    public async void Test_Controller_Get()
    {
      var resultMany = await _controller.Get();
      var resultFail = await _controller.Get("fake@email.com");
      var resultOne = await _controller.Get("Test@test.com");

      Assert.NotNull(resultMany);
      Assert.NotNull(resultFail);
      Assert.NotNull(resultOne);
    }
  }

  //   [Fact]
  //   public async void Test_Controller_Post()
  //   {
  //     var resultPass = await _controller.Post(new AccountModel());

  //     Assert.NotNull(resultPass);
  //   }

  //   [Fact]
  //   public async void Test_Controller_Put()
  //   {
  //     var resultPass = await _controller.Put(new AccountModel());

  //     Assert.NotNull(resultPass);
  //   }

  //   [Fact]
  //   public async void Test_404_Response()
  //   {
  //     var result = await _controller.Get("fake@email.com");

  //     Assert.IsType<NotFoundObjectResult>(result);
  //   }
  // }
}
