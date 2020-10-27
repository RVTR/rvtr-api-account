using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RVTR.Account.ObjectModel.Interfaces;
using RVTR.Account.ObjectModel.Models;
using RVTR.Account.WebApi.ResponseObjects;

namespace RVTR.Account.WebApi.Controllers
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
    /// Delete a user's account via email
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(string email)
    {
      try
      {
        // Instead of directly deleting by passed ID, search for account (& it's ID) from passed email first
        AccountModel accountModel = await _unitOfWork.Account.SelectByEmailAsync(email);

        _logger.LogDebug("Deleting an account by its email...");

        await _unitOfWork.Account.DeleteAsync(accountModel.Id);
        await _unitOfWork.CommitAsync();


        _logger.LogInformation($"Deleted the account with email {email}, ID number {accountModel.Id}.");

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

      return Ok(await _unitOfWork.Account.SelectAsync());

    }

    /// <summary>
    /// Get a user's account by account email
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(AccountModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(string email)
    {
      // Instead of directly retrieving by passed ID, search for account (& it's ID) from passed email first
      AccountModel accountModel = await _unitOfWork.Account.SelectByEmailAsync(email);

      _logger.LogDebug("Getting an account by its email...");

      accountModel = await _unitOfWork.Account.SelectAsync(accountModel.Id);


      if (accountModel is AccountModel theAccount)
      {
        _logger.LogInformation($"Retrieved the account with email {email}, ID: {accountModel.Id}.");

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

      await _unitOfWork.Account.InsertAsync(account);
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

        _unitOfWork.Account.Update(account);
        await _unitOfWork.CommitAsync();

        _logger.LogInformation($"Successfully updated the account {account}.");

        return Accepted(account);
      }

      catch
      {
        _logger.LogWarning($"This account does not exist.");

        return NotFound(new ErrorObject($"Account with ID number {account.Id} does not exist"));
      }

    }

  }
}
