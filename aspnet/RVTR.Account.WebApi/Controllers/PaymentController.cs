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
  /// 
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
    /// 
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="unitOfWork"></param>
    public PaymentController(ILogger<PaymentController> logger, IUnitOfWork unitOfWork)
    {
      _logger = logger;
      _unitOfWork = unitOfWork;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    // DELETE: api/Payment/5
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
        return NotFound(id);
      }
    }

    // GET: api/Payment
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

    // GET: api/Payment/5
/*    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
      try
      {
        var payment = await _unitOfWork.PaymentRepository.Get(id);

        if (payment == null)
          return NotFound(id);

        payment.CardNumber = Helpers.ObscureCreditCardNum(payment.CardNumber);
        return Ok(payment);
      }
      catch
      {
        return NotFound(id);
      }
    }*/

    // PUT: api/Payment/5
    // To protect from overposting attacks, enable the specific properties you want to bind to, for
    // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
    //[HttpPut]
    //public async Task<IActionResult> Put(PaymentModel payment)
    //{
    //  await _unitOfWork.PaymentRepository.Update(payment);

    //  try
    //  {
    //    await _unitOfWork.Complete();
    //  }
    //  catch (DbUpdateConcurrencyException)
    //  {
    //    return NotFound();
    //  }
    //  return Accepted(payment);
    //}

    // POST: api/Payment
    // To protect from overposting attacks, enable the specific properties you want to bind to, for
    // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
    [HttpPost]
    public async Task<IActionResult> Post(PaymentModel payment)
    {
      await _unitOfWork.PaymentRepository.Update(payment);
      await _unitOfWork.Complete();

      return Accepted(payment);
    }
  }
}
