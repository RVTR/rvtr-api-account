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
    /// Get a user's profile with profile email
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    [HttpGet("{email}")]
    [ProducesResponseType(typeof(ProfileModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(string email)
    {
      var profileModel = (await _unitOfWork.Profile.SelectAsync(e => e.Email == email)).FirstOrDefault();

      if (profileModel == null)
      {
        return NotFound(new ErrorObject($"Profile with Email {email} does not exist."));
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
    /// <param name="email"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("Deactivate")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    public async Task<IActionResult> Deactivate(string email)
    {
      try
      {
        var result = (await _unitOfWork.Profile.SelectAsync(p => p.Email == email)).FirstOrDefault();
        result.IsActive = false;

        _unitOfWork.Profile.Update(result);
        await _unitOfWork.CommitAsync();

        return Accepted();
      }
      catch (Exception error)
      {
        _logger.LogError(error, error.Message);

        return NotFound(new ErrorObject($"Profile with Email {email} does not exist."));
      }
    }/// <summary>
     /// Activates a profile from an account
     /// </summary>
     /// <param name="email"></param>
     /// <returns></returns>
    [HttpPost]
    [Route("Activate")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    public async Task<IActionResult> Activate(string email)
    {
      try
      {
        var result = (await _unitOfWork.Profile.SelectAsync(p => p.Email == email)).FirstOrDefault();
        result.IsActive = true;

        _unitOfWork.Profile.Update(result);
        await _unitOfWork.CommitAsync();

        return Accepted();
      }
      catch (Exception error)
      {
        _logger.LogError(error, error.Message);

        return NotFound(new ErrorObject($"Profile with Email {email} does not exist."));
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

        return NotFound(new ErrorObject($"Profile with Email {profile.Email} does not exist."));
      }
    }
  }
}
