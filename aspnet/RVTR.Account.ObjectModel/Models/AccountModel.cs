using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RVTR.Account.ObjectModel.Models
{
  /// <summary>
  /// Represents the _Account_ model
  /// </summary>
  public class AccountModel : IValidatableObject
  {
    public int Id { get; set; }

    public AddressModel Address { get; set; }

    [Required]
    [MinLength(1, ErrorMessage="Name must be at least one character.")]
    [MaxLength(50, ErrorMessage="Name must be fewer than 50 characters.")]
    [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$", ErrorMessage = "Name must start with a capital letter and only use letters.")]
    public string Name { get; set; }

    public IEnumerable<PaymentModel> Payments { get; set; } = new List<PaymentModel>();

    public IEnumerable<ProfileModel> Profiles { get; set; } = new List<ProfileModel>();

    /// <summary>
    /// Empty constructor
    /// </summary>
    public AccountModel() { }

    /// <summary>
    /// Constructor that takes a name
    /// </summary>
    /// <param name="name"></param>
    public AccountModel(string name)
    {
      Name = name;
    }


    /// <summary>
    /// Represents the _Account_ `Validate` method
    /// </summary>
    /// <param name="validationContext"></param>
    /// <returns></returns>
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
      if (string.IsNullOrEmpty(Name))
      {
        yield return new ValidationResult("Account name cannot be null.");
      }
    }
  }
}
