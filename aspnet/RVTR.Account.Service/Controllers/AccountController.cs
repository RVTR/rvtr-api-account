using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RVTR.Account.Domain.Interfaces;
using RVTR.Account.Domain.Models;
using RVTR.Account.Service.ResponseObjects;

namespace RVTR.Account.Service.Controllers
{
  /// <summary>
  ///
  /// </summary>
  [ApiController]
  [ApiVersion("0.0")]
  [EnableCors("Public")]
  [Route("rest/account/{version:apiVersion}/[controller]")]
  public class AccountController : ControllerBase
  {
    private readonly ILogger<AccountController> _logger;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    ///
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="unitOfWork"></param>
    public AccountController(ILogger<AccountController> logger, IUnitOfWork unitOfWork)
    {
      _logger = logger;
      _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Delete a user's account by email
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    [HttpDelete]
    [Route("DeleteAccount/{email}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAccount(string email)
    {
      try
      {
        var accountModel = (await _unitOfWork.Account.SelectAsync(e => e.Email == email)).FirstOrDefault();

        await _unitOfWork.Account.DeleteAsync(accountModel.EntityId);
        await _unitOfWork.CommitAsync();

        return Ok(MessageObject.Success);
      }
      catch (Exception error)
      {
        _logger.LogError(error, error.Message);

        return NotFound(new ErrorObject($"Account with email {email} does not exist"));
      }
    }

    /// <summary>
    /// Get all user accounts available
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("GetAccounts")]
    [ProducesResponseType(typeof(IEnumerable<AccountModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAccounts()
    {
      return Ok(await _unitOfWork.Account.SelectAsync());
    }

    /// <summary>
    /// Get a user's account via email
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("GetAccountByEmail/{email}")]
    [ProducesResponseType(typeof(AccountModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAccountByEmail(string email)
    {
      var accountModel = (await _unitOfWork.Account.SelectAsync(e => e.Email == email)).FirstOrDefault();

      if (accountModel == null)
      {
        return NotFound(new ErrorObject($"Account with email {email} does not exist."));
      }

      return Ok(accountModel);
    }

    /// <summary>
    /// Add an account
    /// </summary>
    /// <param name="account"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("AddAccount")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    public async Task<IActionResult> AddAccount([FromBody] AccountModel account)
    {
      await _unitOfWork.Account.InsertAsync(account);
      await _unitOfWork.CommitAsync();

      return Accepted(account);
    }

    /// <summary>
    /// Update an existing account
    /// </summary>
    /// <param name="account"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("UpdateAccount")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateAccount([FromBody] AccountModel account)
    {
      try
      {
        _unitOfWork.Account.Update(account);

        await _unitOfWork.CommitAsync();

        return Accepted(account);
      }
      catch (Exception error)
      {
        _logger.LogError(error, error.Message);

        return NotFound(new ErrorObject($"Account with ID number {account.EntityId} does not exist"));
      }
    }

    /// <summary>
    /// Get all profiles
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("GetAllProfile")]
    [ProducesResponseType(typeof(IEnumerable<ProfileModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllProfile()
    {
      return Ok(await _unitOfWork.Profile.SelectAsync(e => e.IsActive == true));
    }

    /// <summary>
    /// Get a user's profile with profile email
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("GetProfileByEmail/{email}")]
    [ProducesResponseType(typeof(ProfileModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetProfileByEmail(string email)
    {
      var profileModel = (await _unitOfWork.Profile.SelectAsync(e => e.Email == email)).FirstOrDefault();

      if (profileModel == null)
      {
        return NotFound(new ErrorObject($"Profile with Email {email} does not exist."));
      }

      return Ok(profileModel);
    }

    /// <summary>
    /// Add a profile to an account
    /// </summary>
    /// <param name="profile"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("AddProfile")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    public async Task<IActionResult> AddProfile(ProfileModel profile)
    {
      await _unitOfWork.Profile.InsertAsync(profile);
      await _unitOfWork.CommitAsync();

      return Accepted(profile);
    }


    /// <summary>
    /// Deactivate a profile from an account
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("Deactivate")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    public async Task<IActionResult> DeactivateProfile(string email)
    {
      try
      {
        var result = (await _unitOfWork.Profile.SelectAsync(p => p.Email == email)).FirstOrDefault();
        result.IsActive = false;

        _unitOfWork.Profile.Update(result);
        await _unitOfWork.CommitAsync();

        return Accepted();
      }
      catch (Exception error)
      {
        _logger.LogError(error, error.Message);

        return NotFound(new ErrorObject($"Profile with Email {email} does not exist."));
      }
    }

    /// <summary>
    /// Update a user's profile
    /// </summary>
    /// <param name="profile"></param>
    /// <returns></returns>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Route("UpdateProfile")]
    public async Task<IActionResult> UpdateProfile(ProfileModel profile)
    {
      try
      {
        _unitOfWork.Profile.Update(profile);

        await _unitOfWork.CommitAsync();

        return Accepted(profile);
      }
      catch (Exception error)
      {
        _logger.LogError(error, error.Message);

        return NotFound(new ErrorObject($"Profile with Email {profile.Email} does not exist."));
      }
    }
  }
}
