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
    [HttpDelete("{email}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(string email)
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
    [ProducesResponseType(typeof(IEnumerable<AccountModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get()
    {
      return Ok(await _unitOfWork.Account.SelectAsync());
    }

    /// <summary>
    /// Get a user's account via email
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    [HttpGet("{email}")]
    [ProducesResponseType(typeof(AccountModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(string email)
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
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    public async Task<IActionResult> Post([FromBody] AccountModel account)
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
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Put([FromBody] AccountModel account)
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
  }
}
