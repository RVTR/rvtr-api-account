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
  /// Represents the _Payment Controller_
  /// </summary>
  [ApiController]
  [ApiVersion("0.0")]
  [EnableCors("Public")]
  [Route("rest/account/{version:apiVersion}/[controller]")]
  public class PaymentController : ControllerBase
  {
    private readonly ILogger<PaymentController> _logger;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// The _Payment Controller_ constructor
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="unitOfWork"></param>
    public PaymentController(ILogger<PaymentController> logger, IUnitOfWork unitOfWork)
    {
      _logger = logger;
      _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Deletes a user's payment information
    /// </summary>
    /// <param name="email"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{email}/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(string email, int id)
    {
      var accountModel = (await _unitOfWork.Account.SelectAsync(e => e.Email == email)).FirstOrDefault();
      if (accountModel == null)
      {
        return NotFound(new ErrorObject($"Account with email: {email} does not exist"));
      }
      foreach (var item in accountModel.Payments)
      {
        if (item.EntityId == id)
        {
          try
          {
            await _unitOfWork.Payment.DeleteAsync(id);
            await _unitOfWork.CommitAsync();

            return Ok(MessageObject.Success);
          }
          catch (Exception error)
          {
            _logger.LogError(error, error.Message);

            return NotFound(new ErrorObject($"Payment with ID number {id} does not exist"));
          }
        }
      }
      return NotFound(new ErrorObject($"Payment with ID number {id} does not exist"));
    }

    /// <summary>
    /// Retrieves all payments
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<PaymentModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get()
    {
      return Ok(await _unitOfWork.Payment.SelectAsync());
    }

    /// <summary>
    /// Retrieves a payment by user's email
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    [HttpGet("{email}")]
    [ProducesResponseType(typeof(PaymentModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(string email)
    {
      var accountModel = (await _unitOfWork.Account.SelectAsync(e => e.Email == email)).FirstOrDefault();

      if (accountModel.Payments == null)
      {
        return NotFound(new ErrorObject($"Account with email: {email} does not exist."));
      }

      return Ok(accountModel.Payments);
    }

    /// <summary>
    /// Adds a payment to an account
    /// </summary>
    /// <param name="payment"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    public async Task<IActionResult> Post(PaymentModel payment)
    {
      await _unitOfWork.Payment.InsertAsync(payment);
      await _unitOfWork.CommitAsync();

      return Accepted(payment);
    }

    /// <summary>
    /// Updates a payment
    /// </summary>
    /// <param name="payment"></param>
    /// <returns></returns>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Put(PaymentModel payment)
    {
      try
      {
        _unitOfWork.Payment.Update(payment);

        await _unitOfWork.CommitAsync();

        return Accepted(payment);
      }
      catch (Exception error)
      {
        _logger.LogError(error, error.Message);

        return NotFound(new ErrorObject($"Payment with ID number {payment.EntityId} does not exist"));
      }
    }
  }
}