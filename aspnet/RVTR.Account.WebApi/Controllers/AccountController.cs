using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RVTR.Account.DataContext.Repositories;
using RVTR.Account.ObjectModel.BusinessL;
using RVTR.Account.ObjectModel.Interface;
using RVTR.Account.ObjectModel.Models;

namespace RVTR.Account.WebApi.Controllers
{
  /// <summary>
  ///
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
    ///
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
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
        return NotFound(id);
      }
    }

    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
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
    ///
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
      try
      {
        var account = await _unitOfWork.AccountRepository.Get(id);
        foreach (var payment in account.Payments)
        {
          payment.CardNumber = Helpers.ObscureCreditCardNum(payment.CardNumber);
        }

        return Ok(new List<AccountModel> { account });
      }
      catch
      {
        return NotFound(id);
      }
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="account"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Post(AccountModel account)
    {
      await _unitOfWork.AccountRepository.Update(account);
      await _unitOfWork.Complete();

      return Accepted(account);
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="account"></param>
    /// <returns></returns>
    [HttpPut]
    public async Task<IActionResult> Put(AccountModel account)
    {
      await _unitOfWork.AccountRepository.Update(account);
      await _unitOfWork.Complete();

      return Accepted(account);
    }
  }
}
