using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RVTR.Account.DataContext.Repositories;
using RVTR.Account.ObjectModel.BusinessL;
using RVTR.Account.ObjectModel.Interface;
using RVTR.Account.ObjectModel.Models;
using SQLitePCL;

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
      await _unitOfWork.AccountRepository.Add(account);
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
      await UpdateAccount(account);

      return Accepted(account);
    }

    private async Task UpdateAccount(AccountModel account)
    {
      //Compare profiles
      await UpdateProfiles(account.Profiles, account.Id);

      //Compare payments
      await UpdatePayments(account.Payments, account.Id);

      
      await _unitOfWork.AccountRepository.Update(account);
      await _unitOfWork.Complete();
    }
    private async Task UpdateProfiles(IEnumerable<ProfileModel> profiles, int accountId)
    {
      var profileComparer = new ProfileComparer();
      var dbProfiles =  await _unitOfWork.ProfileRepository.Find(p => p.AccountId == accountId);

      foreach (var profile in dbProfiles)
      {
        //if the new list does not contain this item, remove it from the db
        if (!profiles.Contains(profile, profileComparer))
        {
          await _unitOfWork.ProfileRepository.Delete(profile.Id);
        }
      }

      foreach (var profile in profiles)
      {
        //if the db list does not contain this item, add it to the db
        if (!dbProfiles.Contains(profile, profileComparer))
        {
          await _unitOfWork.ProfileRepository.Add(profile);
        }
      }
    }
    private async Task UpdatePayments(IEnumerable<PaymentModel> payments, int accountId)
    {
      var paymentComparer = new PaymentComparer();
      var dbPayments =  await _unitOfWork.PaymentRepository.Find(p => p.AccountId == accountId);

      foreach (var payment in dbPayments)
      {
        //if the new list does not contain this item, remove it from the db
        if (!payments.Contains(payment, paymentComparer))
        {
          await _unitOfWork.PaymentRepository.Delete(payment.Id);
        }
      }

      foreach (var payment in payments)
      {
        //if the db list does not contain this item, add it to the db
        if (!dbPayments.Contains(payment, paymentComparer))
        {
          await _unitOfWork.PaymentRepository.Add(payment);
        }
      }
    }
  }
}
