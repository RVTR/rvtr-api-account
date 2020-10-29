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
  public class ProfileControllerTest
  {
    private readonly ProfileController _controller;
    private readonly ILogger<ProfileController> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public ProfileControllerTest()
    {
      var loggerMock = new Mock<ILogger<ProfileController>>();
      var repositoryMock = new Mock<IRepository<ProfileModel>>();
      var unitOfWorkMock = new Mock<IUnitOfWork>();

      repositoryMock.Setup(m => m.DeleteAsync(0)).Throws(new Exception());
      repositoryMock.Setup(m => m.DeleteAsync(1)).Returns(Task.CompletedTask);
      repositoryMock.Setup(m => m.InsertAsync(It.IsAny<ProfileModel>())).Returns(Task.CompletedTask);
      repositoryMock.Setup(m => m.SelectAsync()).ReturnsAsync((IEnumerable<ProfileModel>)null);
      repositoryMock.Setup(m => m.SelectAsync(0)).Throws(new Exception());
      repositoryMock.Setup(m => m.SelectAsync(1)).ReturnsAsync((ProfileModel)null);
      repositoryMock.Setup(m => m.SelectAsync(2)).ReturnsAsync(new ProfileModel("test@gmail.com", "Name", "Name", "1234567899", "Type", new AccountModel()) { Id = 2 });
      repositoryMock.Setup(m => m.Update(It.IsAny<ProfileModel>()));
      unitOfWorkMock.Setup(m => m.Profile).Returns(repositoryMock.Object);

      _logger = loggerMock.Object;
      _unitOfWork = unitOfWorkMock.Object;
      _controller = new ProfileController(_logger, _unitOfWork);
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
      ProfileModel profile = new ProfileModel("test@gmail.com", "Name", "Name", "1234567899", "Type", new AccountModel());

      //Act
      var resultPass = await _controller.Post(profile);

      //Assert
      Assert.IsType<AcceptedResult>(resultPass);
    }

    [Fact]
    public async void Test_Controller_Post_BadRequest()
    {
      //Arrange
      ProfileModel profile = new ProfileModel("", "Name", "Name", "1234567899", "Type", new AccountModel()); //Bad email

      //Act
      var result = await _controller.Post(profile);

      //Assert
      Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async void Test_Controller_Put_OK()
    {
      //Arrange
      ProfileModel profile = await _unitOfWork.Profile.SelectAsync(2);
      profile.familyName = "Name";

      //Act
      var resultPass = await _controller.Put(profile);

      //Assert
      Assert.IsType<AcceptedResult>(resultPass);
    }

    [Fact]
    public async void Test_Controller_Put_BadRequest()
    {
      //Arrange
      ProfileModel profile = await _unitOfWork.Profile.SelectAsync(2);
      profile.familyName = "123"; //Bad name

      //Act
      var result = await _controller.Post(profile);

      //Assert
      Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async void Test_Controller_Put_NotFound()
    {
      //Arrange
      ProfileModel profile = new ProfileModel("test@gmail.com", "Name", "Name", "1234567899", "Type", new AccountModel());

      //Act
      var result = await _controller.Put(profile);

      //Assert
      Assert.IsType<NotFoundObjectResult>(result);
    }
  }
}
