using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RVTR.Account.DataContext;
using RVTR.Account.ObjectModel.Interface;
using RVTR.Account.ObjectModel.Models;

namespace RVTR.Account.WebApi.Controllers
{

  [ApiController]
  [ApiVersion("0.0")]
  [EnableCors("Public")]
  [Route("api/v{version:apiVersion}/[controller]")]
  public class PaymentController : ControllerBase
  {
    private readonly ILogger<ProfileController> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public PaymentController(ILogger<ProfileController> logger, IUnitOfWork unitOfWork)
    {
      _logger = logger;
      _unitOfWork = unitOfWork;
    }

    // GET: api/Payment
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PaymentModel>>> Get()
    {
      return Ok(await _unitOfWork.PaymentRepository.GetAll());
    }

    // GET: api/Payment/5
    [HttpGet("{id}")]
    public async Task<ActionResult<PaymentModel>> Get(int id)
    {
      try
      {
        return Ok(await _unitOfWork.PaymentRepository.Get(id));
      }
      catch
      {
        return NotFound(id);
      }
    }

    // PUT: api/Payment/5
    // To protect from overposting attacks, enable the specific properties you want to bind to, for
    // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
    [HttpPut]
    public async Task<IActionResult> Put(PaymentModel payment)
    {
      await _unitOfWork.PaymentRepository.Update(payment);

      try
      {
        await _unitOfWork.Complete();
      }
      catch (DbUpdateConcurrencyException)
      {
        return NotFound();
      }
      return Accepted(payment);
    }

    // POST: api/Payment
    // To protect from overposting attacks, enable the specific properties you want to bind to, for
    // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
    [HttpPost]
    public async Task<ActionResult<PaymentModel>> Post(PaymentModel payment)
    {
      await _unitOfWork.PaymentRepository.Update(payment);
      await _unitOfWork.Complete();

      return Accepted(payment);
    }
    // DELETE: api/Payment/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<PaymentModel>> Delete(int id)
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
  }
}
