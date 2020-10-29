using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
      try
      {
        _logger.LogDebug("Deleting a payment by its ID number...");

        await _unitOfWork.Payment.DeleteAsync(id);
        await _unitOfWork.CommitAsync();

        _logger.LogInformation($"Deleted the payment with ID number {id}.");

        return Ok(MessageObject.Success);
      }
      catch
      {
        _logger.LogWarning($"Payment with ID number {id} does not exist.");

        return NotFound(new ErrorObject($"Payment with ID number {id} does not exist"));
      }

    }

    /// <summary>
    /// Retrieves all payments
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<PaymentModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get()
    {
      _logger.LogInformation($"Retrieved the payments.");

      return Ok(await _unitOfWork.Payment.SelectAsync());
    }

    /// <summary>
    /// Retrieves a payment by payment ID number
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(PaymentModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(int id)
    {
      PaymentModel paymentModel;

      _logger.LogDebug("Getting a payment by its ID number...");

      paymentModel = await _unitOfWork.Payment.SelectAsync(id);


      if (paymentModel is PaymentModel thePayment)
      {
        _logger.LogInformation($"Retrieved the payment with ID: {id}.");

        return Ok(thePayment);
      }

      _logger.LogWarning($"Payment with ID number {id} does not exist.");

      return NotFound(new ErrorObject($"Payment with ID number {id} does not exist."));
    }

    /// <summary>
    /// Adds a payment to an account
    /// </summary>
    /// <param name="payment"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post(PaymentModel payment)
    {
      _logger.LogDebug("Adding a payment...");

      //Checks to see if the payment model is valid
      //Sends a 400BadRequest response since the payment isn't valid
      var context = new ValidationContext(payment);
      if (!Validator.TryValidateObject(payment, context, null, true))
      {
        _logger.LogDebug("Payment model state is invalid.");

        return BadRequest(payment); //Return 400
      }
      else
      {
        await _unitOfWork.Payment.InsertAsync(payment); //Add payment to db
        await _unitOfWork.CommitAsync();                //Save changes

        _logger.LogInformation($"Successfully added the payment {payment}.");

        return Accepted(payment); //Return 202
      }
    }

    /// <summary>
    /// Updates a payment
    /// </summary>
    /// <param name="payment"></param>
    /// <returns></returns>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Put(PaymentModel payment)
    {
      _logger.LogDebug("Updating a payment...");

      //Checks to see if the payment model is valid
      //Sends a 400BadRequest response since the payment isn't valid
      var context = new ValidationContext(payment);
      if (!Validator.TryValidateObject(payment, context, null, true))
      {
        _logger.LogDebug("Payment model state is invalid.");

        return BadRequest(payment); //Return 400
      }
      else //If the model state is valid...
      {
        try
        {
          PaymentModel foundPayment = await _unitOfWork.Payment.SelectAsync(payment.Id); //Find the payment to update

          _unitOfWork.Payment.Update(payment);  //Update payement in db
          await _unitOfWork.CommitAsync();      //Save changes


          _logger.LogInformation($"Successfully updated the payment {payment}.");

          return Accepted(payment); //Return 202 OK
        }
        catch
        {
          _logger.LogWarning($"This payment does not exist.");

          return NotFound(new ErrorObject($"Payment with ID number {payment.Id} does not exist"));  //Return not found
        }
      }
    }
  }
}
