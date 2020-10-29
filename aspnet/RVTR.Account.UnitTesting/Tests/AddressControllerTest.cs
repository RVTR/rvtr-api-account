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
  public class AddressControllerTest
  {
    private readonly AddressController _controller;
    private readonly ILogger<AddressController> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public AddressControllerTest()
    {
      var loggerMock = new Mock<ILogger<AddressController>>();
      var repositoryMock = new Mock<IRepository<AddressModel>>();
      var unitOfWorkMock = new Mock<IUnitOfWork>();

      repositoryMock.Setup(m => m.DeleteAsync(0)).Throws(new Exception());
      repositoryMock.Setup(m => m.DeleteAsync(1)).Returns(Task.CompletedTask);
      repositoryMock.Setup(m => m.InsertAsync(It.IsAny<AddressModel>())).Returns(Task.CompletedTask);
      repositoryMock.Setup(m => m.SelectAsync()).ReturnsAsync((IEnumerable<AddressModel>)null);
      repositoryMock.Setup(m => m.SelectAsync(0)).Throws(new Exception());
      repositoryMock.Setup(m => m.SelectAsync(1)).ReturnsAsync((AddressModel)null);
      repositoryMock.Setup(m => m.SelectAsync(2)).ReturnsAsync(new AddressModel("City", "USA", "11111", "NC", "11 street st.", new AccountModel("Bob")) { Id = 2});
      repositoryMock.Setup(m => m.Update(It.IsAny<AddressModel>()));
      unitOfWorkMock.Setup(m => m.Address).Returns(repositoryMock.Object);

      _logger = loggerMock.Object;
      _unitOfWork = unitOfWorkMock.Object;
      _controller = new AddressController(_logger, _unitOfWork);
    }

    [Fact]
    public async void Test_Controller_Delete()
    {
      var resultFail = await _controller.Delete(0);
      var resultPass = await _controller.Delete(-1);

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
      AddressModel address = new AddressModel("City", "USA", "27516", "NC", "109 Street st.", new AccountModel("Bob"));

      var resultPass = await _controller.Post(address);

      Assert.IsType<AcceptedResult>(resultPass);
    }

    [Fact]
    public async void Test_Controller_Put_OK()
    {
      //Arrange
      AddressModel address = await _unitOfWork.Address.SelectAsync(2);
      address.Street = "204 Street st.";

      //Act
      var resultPass = await _controller.Put(address);

      //Assert
      Assert.IsType<AcceptedResult>(resultPass);
    }

    [Fact]
    public async void Test_Controller_Put_BadRequest()
    {
      var badRequest = await _controller.Put(new AddressModel());

      Assert.IsType<BadRequestObjectResult>(badRequest);
    }

    [Fact]
    public async void Test_Controller_Put_NotFound()
    {
      AddressModel address = new AddressModel("City", "USA", "27516", "NC", "109 Street st.", new AccountModel("Bob"));

      var notFound = await _controller.Put(address); //Can't update a new account model not in the db

      Assert.IsType<NotFoundObjectResult>(notFound);
    }
  }
}
