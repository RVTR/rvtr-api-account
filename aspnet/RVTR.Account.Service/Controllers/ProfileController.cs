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
  /// Represents the _Profile Controller_ class
  /// </summary>
  [ApiController]
  [ApiVersion("0.0")]
  [EnableCors("Public")]
  [Route("rest/account/{version:apiVersion}/[controller]")]
  public class ProfileController : ControllerBase
  {
    private readonly ILogger<ProfileController> _logger;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// The _Profile Controller_ constructor
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="unitOfWork"></param>
    public ProfileController(ILogger<ProfileController> logger, IUnitOfWork unitOfWork)
    {
      _logger = logger;
      _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Delete a user's profile
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
        await _unitOfWork.Profile.DeleteAsync(id);
        await _unitOfWork.CommitAsync();

        return Ok(MessageObject.Success);
      }
      catch (Exception error)
      {
        _logger.LogError(error, error.Message);

        return NotFound(new ErrorObject($"Profile with ID number {id} does not exist."));
      }
    }

    /// <summary>
    /// Get all profiles
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ProfileModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get()
    {
      return Ok(await _unitOfWork.Profile.SelectAsync(e => e.IsActive == true));
    }

    /// <summary>
    /// Get a user's profile with profile ID number
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ProfileModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(int id)
    {
      var profileModel = (await _unitOfWork.Profile.SelectAsync(e => e.EntityId == id)).FirstOrDefault();

      if (profileModel == null)
      {
        return NotFound(new ErrorObject($"Profile with ID number {id} does not exist."));
      }

      return Ok(profileModel);
    }

    /// <summary>
    /// Add a profile to an account
    /// </summary>
    /// <param name="profile"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    public async Task<IActionResult> Post(ProfileModel profile)
    {
      await _unitOfWork.Profile.InsertAsync(profile);
      await _unitOfWork.CommitAsync();

      return Accepted(profile);
    }


    /// <summary>
    /// Deactivate a profile from an account
    /// </summary>
    /// <param name="ID"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("Deactivate")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    public async Task<IActionResult> Deactivate(long ID)
    {
      try
      {
        var result = (await _unitOfWork.Profile.SelectAsync(p => p.EntityId == ID)).FirstOrDefault();
        result.IsActive = false;

        _unitOfWork.Profile.Update(result);
        await _unitOfWork.CommitAsync();

        return Accepted();
      }
      catch (Exception error)
      {
        _logger.LogError(error, error.Message);

        return NotFound(new ErrorObject($"Profile with ID number {ID} does not exist."));
      }
    }

    /// <summary>
    /// Update a user's profile
    /// </summary>
    /// <param name="profile"></param>
    /// <returns></returns>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Put(ProfileModel profile)
    {
      try
      {
        _unitOfWork.Profile.Update(profile);

        await _unitOfWork.CommitAsync();

        return Accepted(profile);
      }
      catch (Exception error)
      {
        _logger.LogError(error, error.Message);

        return NotFound(new ErrorObject($"Profile with ID number {profile.EntityId} does not exist."));
      }
    }
  }
}
