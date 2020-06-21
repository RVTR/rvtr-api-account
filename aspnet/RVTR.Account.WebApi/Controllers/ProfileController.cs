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
  /// Entry point for any request to profile controller
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
    /// DI of logger/unitOfWork
    /// </summary>
    public ProfileController(ILogger<ProfileController> logger, IUnitOfWork unitOfWork)
    {
      _logger = logger;
      _unitOfWork = unitOfWork;
    }
    /// <summary>
    /// Delete profile by ProfileId
    /// </summary>
    /// <param name="id">ProfileId</param>
    /// <returns>200 OK / 404 Not Found</returns>
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
        _logger.LogError("Issues deleting profile");
        return NotFound();
      }
    }
    /// <summary>
    /// Get all profiles / Get all profiles with AccountId / Get profile with ProfileId
    /// </summary>
    /// <param name="accountId">AccountId</param>
    /// <param name="profileId">ProfileId</param>
    /// <returns>200 OK with IEnumerable(ProfileModel) / 404 Not Found</returns>
    [HttpGet]
    public async Task<IActionResult> Get(int? accountId, int? profileId)
    {
      IEnumerable<ProfileModel> profiles = null;

      if (accountId == null && profileId == null)
      {
        profiles = await _unitOfWork.ProfileRepository.GetAll();
      }
      else if (accountId != null && profileId == null)
      {
        profiles = await _unitOfWork.ProfileRepository.Find(p => p.AccountId == accountId);
      }
      else if(accountId == null && profileId != null)
      {
        return  Ok(await _unitOfWork.ProfileRepository.Get((int)profileId));
      }
      else
      {
        return NotFound();
      }
      return Ok(profiles);
    }

    /// <summary>
    /// Post profile by ProfileModel object
    /// </summary>
    /// <param name="profile">ProfileModel object</param>
    /// <returns>202 Accepted with posted profile</returns>
    [HttpPost]
    public async Task<IActionResult> Post(ProfileModel profile)
    {
      await _unitOfWork.ProfileRepository.Update(profile);
      await _unitOfWork.Complete();

      return Accepted(profile);
    }

    /// <summary>
    /// Put profile by ProfileModel object
    /// </summary>
    /// <param name="profile">ProfileModel object</param>
    /// <returns>202 Accepted with updated profile</returns>
    [HttpPut]
    public async Task<IActionResult> Put(ProfileModel profile)
    {
      await _unitOfWork.ProfileRepository.Update(profile);
      await _unitOfWork.Complete();

      return Accepted(profile);
    }
  }
}
