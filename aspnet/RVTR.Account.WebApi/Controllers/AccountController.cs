using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RVTR.Account.ObjectModel.BusinessL;
using RVTR.Account.ObjectModel.Interface;
using RVTR.Account.ObjectModel.Models;


namespace RVTR.Account.WebApi.Controllers
{
  /// <summary>
  /// Entry point for any request to account controller
  /// </summary>
  [ApiController]
  [ApiVersion("0.0")]
  [EnableCors("Public")]
  [Route("api/v{version:apiVersion}/[controller]")]
  public class AccountController : ControllerBase
  {
    private readonly ILogger<AccountController> _logger;
    private readonly IUnitOfWork _unitOfWork;
    /// <summary>
    /// DI of logger/unitOfWork
    /// </summary>
    public AccountController(ILogger<AccountController> logger, IUnitOfWork unitOfWork)
    {
      _logger = logger;
      _unitOfWork = unitOfWork;
    }
    /// <summary>
    /// Delete account by AccountId
    /// </summary>
    /// <param name="id">AccountId</param>
    /// <returns>200 OK / 404 Not Found</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
      try
      {
        await _unitOfWork.AccountRepository.Delete(id);
        await _unitOfWork.Complete();

        return Ok();
      }
      catch
      {
        _logger.LogError("Issues deleting account");
        return NotFound();
      }
    }
    /// <summary>
    /// Get all accounts
    /// </summary>
    /// <returns>200 OK with IEnumerable(AccountModel)</returns>
    [HttpGet]
    public async Task<IActionResult> Get()
    {
      var accounts = await _unitOfWork.AccountRepository.GetAll();
      foreach (var account in accounts)
      {
        foreach (var payment in account.Payments)
        {
          payment.CardNumber = Helpers.ObscureCreditCardNum(payment.CardNumber);
        }
      }
      return Ok(accounts);
    }
    /// <summary>
    /// Get account by AccountId
    /// </summary>
    /// <param name="id">AccountId</param>
    /// <returns>200 OK with List(AccountModel) / 404 Not Found</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
      try
      {
        var account = await _unitOfWork.AccountRepository.Get(id);
        if (account != null)
        {
          foreach (var payment in account.Payments)
          {
            payment.CardNumber = Helpers.ObscureCreditCardNum(payment.CardNumber);
          }

          return Ok(new List<AccountModel> { account });
        }
        throw new Exception();
      }
      catch
      {
        return NotFound();
      }
    }
    /// <summary>
    /// Post account by AccountModel object
    /// </summary>
    /// <param name="account">AccountModel object</param>
    /// <returns>202 Accepted with posted account</returns>
    [HttpPost]
    public async Task<IActionResult> Post(AccountModel account)
    {
      await _unitOfWork.AccountRepository.Add(account);
      await _unitOfWork.Complete();

      return Accepted(account);
    }

    /// <summary>
    /// Put account by AccountModel object
    /// </summary>
    /// <param name="account">AccountModel object</param>
    /// <returns>202 Accepted with updated account</returns>
    [HttpPut]
    public async Task<IActionResult> Put(AccountModel account)
    {
      await _unitOfWork.AccountRepository.Update(account);
      await _unitOfWork.Complete();

      return Accepted(account);
    }
  }
}
