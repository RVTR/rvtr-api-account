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

    [Required(ErrorMessage = "First name required")]
    [MaxLength(50, ErrorMessage = "First name must be fewer than 50 characters.")]
    [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$", ErrorMessage = "First name must start with a capital letter and only use letters.")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Last name required")]
    [MaxLength(50, ErrorMessage = "Last name must be fewer than 50 characters.")]
    [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$", ErrorMessage = "Last name must start with a capital letter and only use letters.")]
    public string LastName { get; set; }
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
    /// Constructor that takes a name and an email
    /// </summary>
    /// <param name="name"></param>
    /// <param name="email"></param>
    public AccountModel(string firstName, string lastName, string email)
    {
      FirstName = firstName;
      LastName = lastName;
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
      if (string.IsNullOrEmpty(FirstName) && string.IsNullOrEmpty(LastName))
      {
        yield return new ValidationResult("Account name cannot be null.");
      }
    }
  }
}
