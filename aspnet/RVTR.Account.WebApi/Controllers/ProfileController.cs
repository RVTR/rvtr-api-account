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
        _logger.LogDebug("Deleting a profile by its ID number...");

        await _unitOfWork.Profile.DeleteAsync(id);
        await _unitOfWork.CommitAsync();

        _logger.LogInformation($"Deleted the profile with ID number {id}.");

        return Ok(MessageObject.Success);
      }
      catch
      {
        _logger.LogWarning($"Profile with ID number {id} does not exist.");

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
      _logger.LogInformation($"Retrieved the profiles.");

      return Ok(await _unitOfWork.Profile.SelectAsync());
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
      ProfileModel profileModel;

      _logger.LogDebug("Getting a profile by its ID number...");

      profileModel = await _unitOfWork.Profile.SelectAsync(id);


      if (profileModel is ProfileModel theProfile)
      {
        _logger.LogInformation($"Retrieved the profile with ID: {id}.");

        return Ok(theProfile);
      }

      _logger.LogWarning($"Profile with ID number {id} does not exist.");

      return NotFound(new ErrorObject($"Profile with ID number {id} does not exist."));
    }

    /// <summary>
    /// Add a profile to an account
    /// </summary>
    /// <param name="profile"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post(ProfileModel profile)
    {
      _logger.LogDebug("Adding a profile...");

      //Checks to see if the profile model is valid
      //Throws a 400BadRequest response since the profile isn't valid
      var context = new ValidationContext(profile);
      if (!Validator.TryValidateObject(profile, context, null, true))
      {
        _logger.LogDebug("Profile model state is invalid.");

        return BadRequest(profile); //Return 400
      }
      else //If model state is valid...
      {
        await _unitOfWork.Profile.InsertAsync(profile); //Insert new profile into db
        await _unitOfWork.CommitAsync();                //Save db changes

        _logger.LogInformation($"Successfully added the profile {profile}.");

        return Accepted(profile); //return 202
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
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Put(ProfileModel profile)
    {
      //Checks to see if the profile model is valid
      //Throws a 400BadRequest response since the profile isn't valid
      var context = new ValidationContext(profile);
      if (!Validator.TryValidateObject(profile, context, null, true))
      {
        _logger.LogDebug("Profile model state is invalid.");

        return BadRequest(profile); //Return 400
      }
      else //If model state is valid...
      {
        try
        {
          ProfileModel foundAccount = await _unitOfWork.Profile.SelectAsync(profile.Id); //Try to find the profile to update

          _logger.LogDebug("Updating a profile...");

          _unitOfWork.Profile.Update(profile);  //Update rpfile in db
          await _unitOfWork.CommitAsync();      //Save changes in db

          _logger.LogInformation($"Successfully updated the profile {profile}.");

          return Accepted(profile); //Return 202
        }
        catch
        {
          _logger.LogWarning($"This profile does not exist.");

          return NotFound(new ErrorObject($"Profile with ID number {profile.Id} does not exist.")); //Return 404
        }
      }
    }
  }
}
