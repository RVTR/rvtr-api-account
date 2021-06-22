using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using RVTR.Account.Domain.Interfaces;
using RVTR.Account.Domain.Models;
using RVTR.Account.Service.Controllers;
using Xunit;

namespace RVTR.Account.Testing.Tests
{
  public class AddressControllerTest
  {
    private readonly AddressController _controller;
    private readonly ILogger<AddressController> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public static readonly IEnumerable<object[]> Addresses = new List<object[]>
    {
      new object[]
      {
        new AddressModel()
        {
          EntityId = 0,
          City = "City",
          Country = "USA",
          PostalCode = "11111",
          StateProvince = "NC",
          Street = "street",
          AccountId = 0,
          Account = new AccountModel(),
        }
      }
    };
    public AddressControllerTest()
    {
      var loggerMock = new Mock<ILogger<AddressController>>();
      var repositoryMock = new Mock<IRepository<AddressModel>>();
      var unitOfWorkMock = new Mock<IUnitOfWork>();

      repositoryMock.Setup(m => m.DeleteAsync(0)).Throws(new Exception());
      repositoryMock.Setup(m => m.DeleteAsync(1)).Returns(Task.CompletedTask);
      repositoryMock.Setup(m => m.InsertAsync(It.IsAny<AddressModel>())).Returns(Task.CompletedTask);
      repositoryMock.Setup(m => m.SelectAsync()).ReturnsAsync((IEnumerable<AddressModel>)null);
      repositoryMock.Setup(m => m.SelectAsync(e => e.EntityId == 0)).Throws(new Exception());
      repositoryMock.Setup(m => m.SelectAsync(e => e.EntityId == 1)).ReturnsAsync((IEnumerable<AddressModel>)null);
      repositoryMock.Setup(m => m.Update(It.IsAny<AddressModel>()));
      unitOfWorkMock.Setup(m => m.Address).Returns(repositoryMock.Object);

      _logger = loggerMock.Object;
      _unitOfWork = unitOfWorkMock.Object;
      _controller = new AddressController(_logger, _unitOfWork);
    }

    //Demo Purposes
    // [Theory]
    // [InlineData("jsmith@gmail.com")]
    // [InlineData("fake@email.com")]
    // public async void Test_Controller_Delete(string email)
    // {
    //   var resultPass = await _controller.Delete(email);
    //   var resultFail = await _controller.Delete(email);

    //   Assert.NotNull(resultFail);
    //   Assert.NotNull(resultPass);
    // }

    // [Theory]
    // [InlineData("jsmith@gmail.com")]
    // [InlineData("fake@email.com")]
    // public async void Test_Controller_Get(string email)
    // {
    //   var resultMany = await _controller.Get();
    //   var resultOne = await _controller.Get(email);
    //   var resultFail = await _controller.Get(email);

    //   Assert.NotNull(resultMany);
    //   Assert.NotNull(resultFail);
    //   Assert.NotNull(resultOne);
    // }

    [Fact]
    public async void Test_Controller_Post()
    {
      var resultPass = await _controller.Post(new AddressModel());

      Assert.NotNull(resultPass);
    }

    [Fact]
    public async void Test_Controller_Put()
    {
      var resultPass = await _controller.Put(new AddressModel());

      Assert.NotNull(resultPass);
    }
  }
}