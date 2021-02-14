using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RVTR.Account.Domain.Models
{
  /// <summary>
  /// Represents the _Profile_ model
  /// </summary>
  public class ProfileModel : AEntity, IValidatableObject
  {

    public bool IsAccountHolder { get; set; }

    [Required(ErrorMessage = "Email address required")]
    [EmailAddress(ErrorMessage = "must be a real email address.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Family name required")]
    [MaxLength(50, ErrorMessage = "Max length of 50 characters")]
    [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$", ErrorMessage = "Name must start with a capital letter and only use letters.")]
    public string FamilyName { get; set; }

    [Required(ErrorMessage = "Given name name required")]
    [MaxLength(50, ErrorMessage = "Max length of 50 characters")]
    [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$", ErrorMessage = "Name must start with a capital letter and only use letters.")]
    public string GivenName { get; set; }

    [Required(ErrorMessage = "Phone number required")]
    [Phone(ErrorMessage = "Must be a phone number")]
    public string Phone { get; set; }

    [Required(ErrorMessage = "Type is required")]
    [MaxLength(50, ErrorMessage = "Type must be fewer than 50 characters.")]
    public string Type { get; set; }

    public int AccountModelId { get; set; }

    public ProfileModel(){}
    public ProfileModel(string firstName, string lastName, string email, bool isAccountHolder)
    {
      GivenName = firstName;
      FamilyName = lastName;
      Email = email;
      IsAccountHolder = isAccountHolder;
    }

    [RegularExpression(@"^(http(s?):\/\/)[^\s]*$", ErrorMessage = "Image URI must be a real image URI.")]
    public string ImageUri { get; set; } = "https://bulma.io/images/placeholders/256x256.png"; //Default is bulma placeholder

    /// <summary>
    /// Represents the _Profile_ `Validate` method
    /// </summary>
    /// <param name="validationContext"></param>
    /// <returns></returns>
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
      if (string.IsNullOrEmpty(Email))
      {
        yield return new ValidationResult("Email cannot be null.");
      }
      if (string.IsNullOrEmpty(FamilyName))
      {
        yield return new ValidationResult("familyName cannot be null.");
      }
      if (string.IsNullOrEmpty(GivenName))
      {
        yield return new ValidationResult("givenName cannot be null.");
      }
      if (string.IsNullOrEmpty(Phone))
      {
        yield return new ValidationResult("Phone cannot be null.");
      }
    }
  }
}
