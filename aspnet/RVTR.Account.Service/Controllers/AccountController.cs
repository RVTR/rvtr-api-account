using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RVTR.Account.Context.Repositories;
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
    // private readonly IUnitOfWork _unitOfWork;
    private readonly UnitOfWork _unitOfWork;

    /// <summary>
    ///
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="unitOfWork"></param>
    public AccountController(ILogger<AccountController> logger, UnitOfWork unitOfWork)
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
        _logger.LogDebug("Deleting an account by its email...");

        // Delete By Object
        AccountModel accountModel = await _unitOfWork.AccountRepository.Select(email);

        await _unitOfWork.Delete<AccountModel>(accountModel);
        await _unitOfWork.CommitAsync();


        _logger.LogInformation($"Deleted the account with email {email}.");

        return Ok(MessageObject.Success);
      }
      catch
      {
        _logger.LogWarning($"Account with email {email} does not exist.");

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
      _logger.LogInformation($"Retrieved the accounts.");

      return Ok(await _unitOfWork.AccountRepository.SelectAll());

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
      _logger.LogDebug("Getting an account by its email...");

      AccountModel accountModel = await _unitOfWork.AccountRepository.Select(email);

      if (accountModel is AccountModel theAccount)
      {
        _logger.LogInformation($"Retrieved the account with email {email}.");

        return Ok(theAccount);
      }

      _logger.LogWarning($"Account with email {email} does not exist.");

      return NotFound(new ErrorObject($"Account with email {email} does not exist."));
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

      _logger.LogDebug("Adding an account...");

      await _unitOfWork.Insert<AccountModel>(account);
      await _unitOfWork.CommitAsync();

      _logger.LogInformation($"Successfully added the account {account}.");

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
        _logger.LogDebug("Updating an account...");

        await _unitOfWork.Update<AccountModel>(account);
        await _unitOfWork.CommitAsync();

        _logger.LogInformation($"Successfully updated the account {account}.");

        return Accepted(account);
      }

      catch
      {
        _logger.LogWarning($"This account does not exist.");

        return NotFound(new ErrorObject($"Account with ID number {account.EntityID} does not exist"));
      }

    }

  }
}
