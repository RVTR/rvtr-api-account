using System.Collections.Generic;
using System.Linq;
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
  /// Entry point for any request to payment controller 
  /// </summary>
  [ApiController]
  [ApiVersion("0.0")]
  [EnableCors("Public")]
  [Route("api/v{version:apiVersion}/[controller]")]
  public class PaymentController : ControllerBase
  {
    private readonly ILogger<PaymentController> _logger;
    private readonly IUnitOfWork _unitOfWork;
    /// <summary>
    /// DI of logger/unitOfWork
    /// </summary>
    public PaymentController(ILogger<PaymentController> logger, IUnitOfWork unitOfWork)
    {
      _logger = logger;
      _unitOfWork = unitOfWork;
    }
    /// <summary>
    /// Delete payment by PaymentId
    /// </summary>
    /// <param name="id">PaymentId</param>
    /// <returns>200 OK / 404 Not Found</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
      try
      {
        await _unitOfWork.PaymentRepository.Delete(id);
        await _unitOfWork.Complete();

        return Ok();
      }
      catch
      {
        _logger.LogError("Issues deleting payment");
        return NotFound();
      }
    }
    /// <summary>
    /// Get all payments / get all payments by AccountId
    /// </summary>
    /// <param name="accountId">AccountId</param>
    /// <returns>200 OK with IEnumerable(Payments) / 404 Not Found</returns>
    [HttpGet]
    public async Task<IActionResult> Get(int? accountId)
    {
        IEnumerable<PaymentModel> payments;
        if (accountId == null)
        {
          payments = await _unitOfWork.PaymentRepository.GetAll();
          if (payments.Count() == 0)
          {
            return NotFound();
          }
        }
        else
        {
          payments = await _unitOfWork.PaymentRepository.Find(p => p.AccountId == accountId);
        }
        if (payments.Count() > 0)
        {
          foreach (var payment in payments)
          {
            payment.CardNumber = Helpers.ObscureCreditCardNum(payment.CardNumber);
          }
        }
        return Ok(payments);
    }
    /// <summary>
    /// Post payment by PaymentModel object
    /// </summary>
    /// <param name="payment">PaymentModel object</param>
    /// <returns>202 Accepted with posted payment</returns>
    [HttpPost]
    public async Task<IActionResult> Post(PaymentModel payment)
    {
      await _unitOfWork.PaymentRepository.Update(payment);
      await _unitOfWork.Complete();

      return Accepted(payment);
    }
  }
}
