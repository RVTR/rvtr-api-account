using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using RVTR.Account.DataContext;
using RVTR.Account.DataContext.Repositories;
using RVTR.Account.ObjectModel.Models;
using RVTR.Account.WebApi.Controllers;
using Xunit;

namespace RVTR.Account.UnitTesting.Tests
{
  public class ProfileControllerTest
  {
    private class Mocks
    {
      public Mock<AccountContext> _accountContext;
      public Mock<ILogger<ProfileController>> _logger;
      public Mock<ProfileRepository> _repository;
      public Mock<UnitOfWork> _unitOfWork;

      public Mocks()
      {
        SqliteConnection _connection = new SqliteConnection("Data Source=:memory:");
        DbContextOptions<AccountContext> _options = new DbContextOptionsBuilder<AccountContext>().UseSqlite(_connection).Options;

        this._accountContext = new Mock<AccountContext>(_options);
        this._logger = new Mock<ILogger<ProfileController>>();
        this._repository = new Mock<ProfileRepository>(this._accountContext.Object);
        this._unitOfWork = new Mock<UnitOfWork>(_accountContext.Object);
        this._unitOfWork.Setup(m => m.ProfileRepository).Returns(this._repository.Object);
      }
    }
    private ProfileController NewProfileController(Mocks mocks)
    {
      return new ProfileController(mocks._logger.Object, mocks._unitOfWork.Object);
    }

    [Fact]
    public async void Delete_ById()
    {
      var mocks = new Mocks();
      mocks._repository.Setup(m => m.Delete(1)).Returns(Task.FromResult(new ProfileModel()));

      var _controller = NewProfileController(mocks);
      var result = await _controller.Delete(1);
      Assert.IsType<OkResult>(result);
    }

    [Fact]
    public async void Delete_ByIdError()
    {
      var mocks = new Mocks();
      mocks._repository.Setup(m => m.Delete(0)).Throws(new ArgumentNullException());

      var _controller = NewProfileController(mocks);
      var result = await _controller.Delete(0);
      Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async void Get_ProfileNoAccountIdNoProfileId()
    {
      var mocks = new Mocks();
      IEnumerable<ProfileModel> newProfile = new List<ProfileModel>();
      mocks._repository.Setup(m => m.GetAll()).Returns(Task.FromResult(newProfile));

      var _controller = NewProfileController(mocks);
      var result = await _controller.Get(null,null);
      Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async void Get_ProfileYesAccountIdNoProfileId()
    {
      var mocks = new Mocks();
      var accountId = 1;
      IEnumerable<ProfileModel> newProfile = new List<ProfileModel>() { new ProfileModel() { AccountId = 1 } };
      mocks._repository.Setup(m => m.Find(p => p.AccountId == accountId)).Returns(Task.FromResult(newProfile));

      var _controller = NewProfileController(mocks);
      var result = await _controller.Get(accountId, null);
      Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async void Get_ProfileNoAccountIdYesProfileId()
    {
      var mocks = new Mocks();
      int profileId = 1;
      ProfileModel newProfile = new ProfileModel() { AccountId = 1 };
      mocks._repository.Setup(m => m.Get(profileId)).Returns(Task.FromResult(newProfile));

      var _controller = NewProfileController(mocks);
      var result = await _controller.Get(null, profileId);
      Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async void Get_ProfileYesAccountIdYesProfileId()
    {
      var mocks = new Mocks();
      int profileId = 1;
      int accountId = 1;

      var _controller = NewProfileController(mocks);
      var result = await _controller.Get(accountId, profileId);
      Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async void Post_ByProfile()
    {
      var mocks = new Mocks();
      var newProfile = new ProfileModel();
      mocks._repository.Setup(m => m.Add(newProfile)).Returns(Task.FromResult(newProfile));

      var _controller = NewProfileController(mocks);
      var result = await _controller.Post(newProfile);
      Assert.IsType<AcceptedResult>(result);
    }

    [Fact]
    public async void Put_ByProfile()
    {
      var mocks = new Mocks();
      var newProfile = new ProfileModel();
      mocks._repository.Setup(m => m.Update(newProfile)).Returns(Task.FromResult(newProfile));

      var _controller = NewProfileController(mocks);
      var result = await _controller.Put(newProfile);
      Assert.IsType<AcceptedResult>(result);
    }
  }
}


