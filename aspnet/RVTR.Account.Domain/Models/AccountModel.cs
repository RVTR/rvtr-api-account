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
    /// Empty constructor
    /// </summary>
    public AccountModel()
    {
      Profiles = new List<ProfileModel>();
      Payments  = new List<PaymentModel>();
    }

    /// <summary>
    /// Constructor that takes a first name, last name, and an email
    /// </summary>
    /// <param name="firstName"></param>
    /// <param name="lastName"></param>
    /// <param name="email"></param>
    public AccountModel(string firstName, string lastName, string email)
    {
      Email = email;
      Profiles = new List<ProfileModel> {
        new ProfileModel(firstName, lastName, email, true)
      };
      Payments  = new List<PaymentModel>();
    }

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