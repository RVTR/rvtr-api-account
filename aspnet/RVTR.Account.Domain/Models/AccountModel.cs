using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RVTR.Account.Domain.Models
{
  /// <summary>
  /// Represents the _Account_ model
  /// </summary>
  public class AccountModel : AEntity, IValidatableObject
  {
    public AddressModel Address { get; set; }

    [Required(ErrorMessage = "Email address required")]
    [EmailAddress(ErrorMessage = "must be a real email address.")]
    public string Email { get; set; }

    public List<PaymentModel> Payments { get; set; }
    public List<ProfileModel> Profiles { get; set; }

   
    /// <summary>
    /// Represents the _Account_ `Validate` method
    /// </summary>
    /// <param name="validationContext"></param>
    /// <returns></returns>
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
      if (Profiles.Count == 0)
      {
        yield return new ValidationResult("Number of Profiles can't be zero");
      }
    }
  }
}
