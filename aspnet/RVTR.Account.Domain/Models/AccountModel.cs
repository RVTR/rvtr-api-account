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

    [Required(ErrorMessage = "Name required")]
    [MaxLength(50, ErrorMessage = "Name must be fewer than 50 characters.")]
    [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$", ErrorMessage = "Name must start with a capital letter and only use letters.")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Name required")]
    [MaxLength(50, ErrorMessage = "Name must be fewer than 50 characters.")]
    [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$", ErrorMessage = "Name must start with a capital letter and only use letters.")]
    public string LastName { get; set; }

    public List<PaymentModel> Payments { get; set; }

    public List<ProfileModel> Profiles { get; set; }


    /// <summary>
    /// Empty constructor
    /// </summary>
    public AccountModel()
    {
      Payments = new List<PaymentModel>();
      Profiles = new List<ProfileModel>();
    }

    /// <summary>
    /// Constructor that takes a name and an email
    /// </summary>
    /// <param name="email"></param>
    /// <param name="firstName"></param>
    /// <param name="lastName"></param>
    public AccountModel(string firstName, string lastName, string email)
    {
      FirstName = firstName;
      LastName = lastName;
      Email = email;
      Payments = new List<PaymentModel>();
      Profiles = new List<ProfileModel> {
        new ProfileModel(firstName, lastName, email, true)
      };
    }

    /// <summary>
    /// Represents the _Account_ `Validate` method
    /// </summary>
    /// <param name="validationContext"></param>
    /// <returns></returns>
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
      if (string.IsNullOrEmpty(FirstName))
      {
        yield return new ValidationResult("Account name cannot be null.");
      }
      if (string.IsNullOrEmpty(LastName))
      {
        yield return new ValidationResult("Account name cannot be null.");
      }
    }
  }
}
