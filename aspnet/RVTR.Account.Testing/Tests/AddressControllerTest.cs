using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using RVTR.Account.Context;
using RVTR.Account.Context.Repositories;
using RVTR.Account.Domain.Interfaces;
using RVTR.Account.Domain.Models;
using RVTR.Account.Service.Controllers;
using Xunit;

namespace RVTR.Account.Testing.Tests
{
  public class AddressControllerTest
  {
    // private readonly AddressController _controller;
    // private readonly ILogger<AddressController> _logger;
    // private readonly UnitOfWork _unitOfWork;
    // private readonly AccountContext _context;

    // public AddressControllerTest()
    // {
      // var loggerMock = new Mock<ILogger<AddressController>>();
      // var unitOfWorkMock = new Mock<UnitOfWork>(_context);

      // unitOfWorkMock.Setup(m => m.Delete(0)).Throws(new Exception());
      // unitOfWorkMock.Setup(m => m.Delete(1)).Returns(Task.CompletedTask);
      // unitOfWorkMock.Setup(m => m.Insert(It.IsAny<AddressModel>())).Returns(Task.CompletedTask);
      // unitOfWorkMock.Setup(m => m.Select()).ReturnsAsync((IEnumerable<AddressModel>)null);
      // unitOfWorkMock.Setup(m => m.Select(0)).Throws(new Exception());
      // unitOfWorkMock.Setup(m => m.SelectAll(1)).ReturnsAsync((AddressModel)null);
      // unitOfWorkMock.Setup(m => m.Update(It.IsAny<AddressModel>()));
      // unitOfWorkMock.Setup(m => m.Address).Returns(repositoryMock.Object);

    //   _logger = loggerMock.Object;
    //   _unitOfWork = unitOfWorkMock.Object;
    //   _controller = new AddressController(_logger, _unitOfWork);
    // }

    // [Fact]
    // public async void Test_Controller_Delete()
    // {
    //   var resultFail = await _controller.Delete(0);
    //   var resultPass = await _controller.Delete(-1);

    //   Assert.NotNull(resultFail);
    //   Assert.NotNull(resultPass);
    // }

    // [Fact]
    // public async void Test_Controller_Get()
    // {
    //   var resultMany = await _controller.Get();
    //   var resultFail = await _controller.Get(-5);
    //   var resultOne = await _controller.Get(-1);

    //   Assert.NotNull(resultMany);
    //   Assert.NotNull(resultFail);
    //   Assert.NotNull(resultOne);
    // }

    // [Fact]
    // public async void Test_Controller_Post()
    // {
    //   var resultPass = await _controller.Post(new AddressModel());

    //   Assert.NotNull(resultPass);
    // }

    // [Fact]
    // public async void Test_Controller_Put()
    // {
    //   var resultPass = await _controller.Put(new AddressModel());

    //   Assert.NotNull(resultPass);
    // }
  }
}
