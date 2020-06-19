using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
  public class ProfileController : ControllerBase
  {
    private readonly ILogger<ProfileController> _logger;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    ///
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="unitOfWork"></param>
    public ProfileController(ILogger<ProfileController> logger, IUnitOfWork unitOfWork)
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
        await _unitOfWork.ProfileRepository.Delete(id);
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
    /// <param name="accountId"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> Get(int? accountId)
    {
      IEnumerable<ProfileModel> profiles;

      if (accountId == null)
        profiles = await _unitOfWork.ProfileRepository.GetAll();
      else
        profiles = await _unitOfWork.ProfileRepository.Find(p => p.AccountId == accountId);

      return Ok(profiles);
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
        return Ok(await _unitOfWork.ProfileRepository.Get(id));
      }
      catch
      {
        return NotFound(id);
      }
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="profile"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Post(ProfileModel profile)
    {
      await _unitOfWork.ProfileRepository.Update(profile);
      await _unitOfWork.Complete();

      return Accepted(profile);
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="profile"></param>
    /// <returns></returns>
    [HttpPut]
    public async Task<IActionResult> Put(ProfileModel profile)
    {
      await _unitOfWork.ProfileRepository.Update(profile);
      await _unitOfWork.Complete();

      return Accepted(profile);
    }
  }
}
