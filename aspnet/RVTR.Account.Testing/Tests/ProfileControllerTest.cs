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
  public class ProfileControllerTest
  {
    private readonly ProfileController _controller;
    private readonly ILogger<ProfileController> _logger;
    private readonly IUnitOfWork _unitOfWork;
    public static readonly IEnumerable<object[]> Profiles = new List<object[]>
    {
      new object[]
      {
        new ProfileModel()
        {
          EntityId = 0,
          Email = "email@email.com",
          FamilyName = "Family",
          GivenName = "Given",
          Phone = "1234567890",
          Type = "Adult",
          AccountModelId = 0
        }
      }
    };
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
      repositoryMock.Setup(m => m.Update(It.IsAny<ProfileModel>()));
      unitOfWorkMock.Setup(m => m.Profile).Returns(repositoryMock.Object);

      _logger = loggerMock.Object;
      _unitOfWork = unitOfWorkMock.Object;
      _controller = new ProfileController(_logger, _unitOfWork);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    public async void Test_Controller_Delete(int id)
    {
      var resultFail = await _controller.Delete(id);
      var resultPass = await _controller.Delete(id);

      Assert.NotNull(resultFail);
      Assert.NotNull(resultPass);
    }

    [Theory]
    [InlineData(-5)]
    [InlineData(-1)]
    public async void Test_Controller_Get(int id)
    {
      var resultMany = await _controller.Get();
      var resultFail = await _controller.Get(id);
      var resultOne = await _controller.Get(id);

      Assert.NotNull(resultMany);
      Assert.NotNull(resultFail);
      Assert.NotNull(resultOne);
    }

    [Theory]
    [MemberData(nameof(Profiles))]
    public async void Test_Controller_Post(ProfileModel profile)
    {
      var resultPass = await _controller.Post(new ProfileModel());

      Assert.NotNull(resultPass);
    }

    [Theory]
    [MemberData(nameof(Profiles))]
    public async void Test_Controller_Put(ProfileModel profile)
    {
      var resultPass = await _controller.Put(new ProfileModel());

      Assert.NotNull(resultPass);
    }
  }
}
